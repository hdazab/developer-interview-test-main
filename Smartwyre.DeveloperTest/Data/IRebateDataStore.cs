using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data
{
    // Define the interface for rebate data store operations
    public interface IRebateDataStore
    {
        /// <summary>
        /// Retrieves a rebate based on the identifier provided.
        /// </summary>
        /// <param name="rebateIdentifier">The identifier for the rebate to retrieve.</param>
        /// <returns>The retrieved Rebate object.</returns>
        Rebate GetRebate(string rebateIdentifier);

        /// <summary>
        /// Adds a rebate to the data store.
        /// </summary>
        /// <param name="rebate">The rebate to add to the store.</param>
        void AddRebate(Rebate rebate);

        /// <summary>
        /// Stores the result of a rebate calculation.
        /// </summary>
        /// <param name="calculation">The rebate calculation result to store.</param>
        void StoreCalculationResult(RebateCalculation calculation);
    }
}
