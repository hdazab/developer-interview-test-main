using Moq;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Services.Calculators;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests.Services
{
    /// <summary>
    /// Unit tests for the RebateService class.
    /// Verifies that the RebateService calculates rebates correctly and handles different rebate scenarios.
    /// </summary>
    public class RebateServiceTests
    {
        private readonly Mock<IRebateDataStore> _rebateDataStoreMock;
        private readonly Mock<IProductDataStore> _productDataStoreMock;
        private readonly Mock<IRebateCalculatorFactory> _calculatorFactoryMock;
        private readonly RebateService _rebateService;

        /// <summary>
        /// Initializes a new instance of the RebateServiceTests class
        /// and sets up the mocks and the RebateService instance.
        /// </summary>
        public RebateServiceTests()
        {
            _rebateDataStoreMock = new Mock<IRebateDataStore>();
            _productDataStoreMock = new Mock<IProductDataStore>();
            _calculatorFactoryMock = new Mock<IRebateCalculatorFactory>();
            _rebateService = new RebateService(_rebateDataStoreMock.Object, _productDataStoreMock.Object, _calculatorFactoryMock.Object);
        }

        /// <summary>
        /// Tests that Calculate returns success when the rebate and product are valid
        /// and the calculator calculates the rebate correctly.
        /// </summary>
        [Fact]
        public void Calculate_Returns_Success_If_Calculator_Succeeds()
        {
            // Arrange
            var rebate = new Rebate { Incentive = IncentiveType.FixedCashAmount };
            var product = new Product { ProductIdentifier = "Prod001", SupportedIncentives = SupportedIncentiveType.FixedCashAmount };
            var request = new CalculateRebateRequest { RebateIdentifier = "Reb001", ProductIdentifier = "Prod001", Volume = 10 };

            _rebateDataStoreMock.Setup(r => r.GetRebate(request.RebateIdentifier)).Returns(rebate);
            _productDataStoreMock.Setup(p => p.GetProduct(request.ProductIdentifier)).Returns(product);

            var calculatorMock = new Mock<IRebateCalculator>();
            calculatorMock.Setup(c => c.Calculate(rebate, product, request)).Returns(new CalculateRebateResult { Success = true, RebateAmount = 50m });
            _calculatorFactoryMock.Setup(f => f.GetCalculator(rebate.Incentive)).Returns(calculatorMock.Object);

            // Act
            var result = _rebateService.Calculate(request);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(50m, result.RebateAmount);
        }

        /// <summary>
        /// Tests that Calculate fails when the rebate is null.
        /// </summary>
        [Fact]
        public void Calculate_Fails_If_Rebate_Is_Null()
        {
            // Arrange
            var request = new CalculateRebateRequest { RebateIdentifier = "Reb001", ProductIdentifier = "Prod001", Volume = 10 };
            _rebateDataStoreMock.Setup(r => r.GetRebate(request.RebateIdentifier)).Returns((Rebate)null);

            // Act
            var result = _rebateService.Calculate(request);

            // Assert
            Assert.False(result.Success);
        }

        /// <summary>
        /// Tests that Calculate fails when the product is null.
        /// </summary>
        [Fact]
        public void Calculate_Fails_If_Product_Is_Null()
        {
            // Arrange
            var rebate = new Rebate { Incentive = IncentiveType.FixedCashAmount };
            var request = new CalculateRebateRequest { RebateIdentifier = "Reb001", ProductIdentifier = "Prod001", Volume = 10 };

            _rebateDataStoreMock.Setup(r => r.GetRebate(request.RebateIdentifier)).Returns(rebate);
            _productDataStoreMock.Setup(p => p.GetProduct(request.ProductIdentifier)).Returns((Product)null);

            // Act
            var result = _rebateService.Calculate(request);

            // Assert
            Assert.False(result.Success);
        }

        /// <summary>
        /// Tests that Calculate fails when the calculator returns a failed result.
        /// </summary>
        [Fact]
        public void Calculate_Fails_If_Calculator_Fails()
        {
            // Arrange
            var rebate = new Rebate { Incentive = IncentiveType.FixedCashAmount };
            var product = new Product { ProductIdentifier = "Prod001", SupportedIncentives = SupportedIncentiveType.FixedCashAmount };
            var request = new CalculateRebateRequest { RebateIdentifier = "Reb001", ProductIdentifier = "Prod001", Volume = 10 };

            _rebateDataStoreMock.Setup(r => r.GetRebate(request.RebateIdentifier)).Returns(rebate);
            _productDataStoreMock.Setup(p => p.GetProduct(request.ProductIdentifier)).Returns(product);

            var calculatorMock = new Mock<IRebateCalculator>();
            calculatorMock.Setup(c => c.Calculate(rebate, product, request)).Returns(new CalculateRebateResult { Success = false });
            _calculatorFactoryMock.Setup(f => f.GetCalculator(rebate.Incentive)).Returns(calculatorMock.Object);

            // Act
            var result = _rebateService.Calculate(request);

            // Assert
            Assert.False(result.Success);
        }
    }
}
