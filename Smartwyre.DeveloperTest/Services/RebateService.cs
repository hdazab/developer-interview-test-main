using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;
using Smartwyre.DeveloperTest.Services.Calculators;

namespace Smartwyre.DeveloperTest.Services
{
    /// <summary>
    /// Service responsible for calculating rebates.
    /// </summary>
    public class RebateService : IRebateService
    {
        private readonly IRebateDataStore _rebateDataStore;
        private readonly IProductDataStore _productDataStore;
        private readonly IRebateCalculatorFactory _rebateCalculatorFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="RebateService"/> class.
        /// </summary>
        /// <param name="rebateDataStore">The data store for accessing rebate information.</param>
        /// <param name="productDataStore">The data store for accessing product information.</param>
        /// <param name="rebateCalculatorFactory">The factory for obtaining the correct rebate calculator.</param>
        public RebateService(IRebateDataStore rebateDataStore, IProductDataStore productDataStore, IRebateCalculatorFactory rebateCalculatorFactory)
        {
            _rebateDataStore = rebateDataStore;
            _productDataStore = productDataStore;
            _rebateCalculatorFactory = rebateCalculatorFactory;
        }

        /// <summary>
        /// Calculates a rebate based on the provided request.
        /// </summary>
        /// <param name="request">The request containing details about the rebate calculation.</param>
        /// <returns>A <see cref="CalculateRebateResult"/> containing the result of the calculation.</returns>
        public CalculateRebateResult Calculate(CalculateRebateRequest request)
        {
            // Retrieve the rebate and product information
            var rebate = _rebateDataStore.GetRebate(request.RebateIdentifier);
            var product = _productDataStore.GetProduct(request.ProductIdentifier);

            // Validate rebate and product
            if (rebate == null || product == null)
            {
                return new CalculateRebateResult
                {
                    Success = false,
                    ErrorMessage = "Invalid rebate or product."
                };
            }

            // Get the appropriate rebate calculator
            var calculator = _rebateCalculatorFactory.GetCalculator(rebate.Incentive);

            // Calculate the rebate
            var result = calculator.Calculate(rebate, product, request);

            // Store the rebate calculation if successful
            if (result.Success)
            {
                _rebateDataStore.StoreCalculationResult(new RebateCalculation
                {
                    RebateIdentifier = request.RebateIdentifier,
                    ProductIdentifier = request.ProductIdentifier,
                    RebateAmount = result.RebateAmount
                });
            }

            return result;
        }
    }
}
