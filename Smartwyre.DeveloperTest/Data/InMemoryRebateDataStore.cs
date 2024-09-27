using Smartwyre.DeveloperTest.Types;
using System.Collections.Generic;

namespace Smartwyre.DeveloperTest.Data
{
    /// <summary>
    /// In-memory implementation of the IRebateDataStore interface.
    /// Stores rebate data in memory for testing or non-persistent scenarios.
    /// </summary>
    public class InMemoryRebateDataStore : IRebateDataStore
    {
        // Dictionary to store rebates using their RebateIdentifier as the key
        private readonly Dictionary<string, Rebate> _rebates = new Dictionary<string, Rebate>();

        // List to store the rebate calculation results
        private readonly List<RebateCalculation> _rebateCalculations = new List<RebateCalculation>();

        /// <summary>
        /// Retrieves a rebate by its unique identifier.
        /// </summary>
        /// <param name="rebateIdentifier">The unique identifier of the rebate.</param>
        /// <returns>A rebate object, or null if the rebate does not exist.</returns>
        public Rebate GetRebate(string rebateIdentifier)
        {
            _rebates.TryGetValue(rebateIdentifier, out var rebate);
            return rebate;
        }

        /// <summary>
        /// Adds a rebate to the in-memory data store.
        /// If the rebate already exists, it is updated.
        /// </summary>
        /// <param name="rebate">The rebate object to add or update.</param>
        public void AddRebate(Rebate rebate)
        {
            _rebates[rebate.RebateIdentifier] = rebate;
        }

        /// <summary>
        /// Stores the result of a rebate calculation.
        /// </summary>
        /// <param name="calculation">The rebate calculation result to store.</param>
        public void StoreCalculationResult(RebateCalculation calculation)
        {
            _rebateCalculations.Add(calculation);
        }
    }
}
