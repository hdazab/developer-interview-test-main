using Smartwyre.DeveloperTest.Types;
using Smartwyre.DeveloperTest.Services.Calculators;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests
{
    public class RebateCalculatorTests
    {
        private readonly RebateCalculator _calculator = new RebateCalculator();

        [Fact]
        public void Calculate_FixedCashAmount_ReturnsExpectedRebate()
        {
            // Arrange
            var rebate = new Rebate
            {
                Incentive = IncentiveType.FixedCashAmount,
                Amount = 100m
            };
            var product = new Product
            {
                SupportedIncentives = SupportedIncentiveType.FixedCashAmount,
                Price = 200m
            };
            var request = new CalculateRebateRequest
            {
                Volume = 5
            };

            // Act
            var result = _calculator.Calculate(rebate, product, request);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(100m, result.RebateAmount); // Expected rebate amount for Fixed Cash Amount
        }

        [Fact]
        public void Calculate_FixedRateRebate_ReturnsExpectedRebate()
        {
            // Arrange
            var rebate = new Rebate
            {
                Incentive = IncentiveType.FixedRateRebate,
                Percentage = 0.1m // 10%
            };
            var product = new Product
            {
                SupportedIncentives = SupportedIncentiveType.FixedRateRebate,
                Price = 200m
            };
            var request = new CalculateRebateRequest
            {
                Volume = 5
            };

            // Act
            var result = _calculator.Calculate(rebate, product, request);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(100m, result.RebateAmount); // 200 * 0.1 * 5 = 100m
        }

        [Fact]
        public void Calculate_AmountPerUom_ReturnsExpectedRebate()
        {
            // Arrange
            var rebate = new Rebate
            {
                Incentive = IncentiveType.AmountPerUom,
                Amount = 10m
            };
            var product = new Product
            {
                SupportedIncentives = SupportedIncentiveType.AmountPerUom,
                Price = 200m
            };
            var request = new CalculateRebateRequest
            {
                Volume = 5
            };

            // Act
            var result = _calculator.Calculate(rebate, product, request);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(50m, result.RebateAmount); // 10 * 5 = 50m
        }

        [Fact]
        public void Calculate_Fails_When_IncentiveType_Is_Not_Supported()
        {
            // Arrange
            var rebate = new Rebate
            {
                Incentive = IncentiveType.FixedCashAmount,
                Amount = 100m
            };
            var product = new Product
            {
                SupportedIncentives = SupportedIncentiveType.FixedRateRebate, // Doesn't support FixedCashAmount
                Price = 200m
            };
            var request = new CalculateRebateRequest
            {
                Volume = 5
            };

            // Act
            var result = _calculator.Calculate(rebate, product, request);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(0m, result.RebateAmount);
        }

        [Fact]
        public void Calculate_Fails_When_Rebate_Is_Null()
        {
            // Arrange
            Rebate rebate = null;
            var product = new Product
            {
                SupportedIncentives = SupportedIncentiveType.FixedCashAmount,
                Price = 200m
            };
            var request = new CalculateRebateRequest
            {
                Volume = 5
            };

            // Act
            var result = _calculator.Calculate(rebate, product, request);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public void Calculate_Fails_When_Product_Is_Null()
        {
            // Arrange
            var rebate = new Rebate
            {
                Incentive = IncentiveType.FixedCashAmount,
                Amount = 100m
            };
            Product product = null;
            var request = new CalculateRebateRequest
            {
                Volume = 5
            };

            // Act
            var result = _calculator.Calculate(rebate, product, request);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public void Calculate_Fails_When_Request_Is_Null()
        {
            // Arrange
            var rebate = new Rebate
            {
                Incentive = IncentiveType.FixedCashAmount,
                Amount = 100m
            };
            var product = new Product
            {
                SupportedIncentives = SupportedIncentiveType.FixedCashAmount,
                Price = 200m
            };
            CalculateRebateRequest request = null;

            // Act
            var result = _calculator.Calculate(rebate, product, request);

            // Assert
            Assert.False(result.Success);
        }
    }
}
