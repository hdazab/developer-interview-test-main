using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services
{
    // Define the interface for the rebate service operations
    public interface IRebateService
    {
        /// <summary>
        /// Calculates the rebate based on the request parameters.
        /// </summary>
        /// <param name="request">The request containing all necessary data to perform the calculation.</param>
        /// <returns>The result of the rebate calculation.</returns>
        CalculateRebateResult Calculate(CalculateRebateRequest request);
    }
}
