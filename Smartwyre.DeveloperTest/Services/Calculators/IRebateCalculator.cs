using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services.Calculators
{
    /// <summary>
    /// Interface that defines a contract for rebate calculators.
    /// All calculators must implement the Calculate method.
    /// </summary>
    public interface IRebateCalculator
    {
        /// <summary>
        /// Calculates the rebate based on the provided rebate, product, and request.
        /// </summary>
        /// <param name="rebate">The rebate object containing rebate details.</param>
        /// <param name="product">The product object containing product details.</param>
        /// <param name="request">The request object containing request details.</param>
        /// <returns>A CalculateRebateResult object indicating success or failure and rebate amount.</returns>
        CalculateRebateResult Calculate(Rebate rebate, Product product, CalculateRebateRequest request);
    }
}
