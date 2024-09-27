using Smartwyre.DeveloperTest.Types;
using System.Collections.Generic;

namespace Smartwyre.DeveloperTest.Data
{
    /// <summary>
    /// Interface for managing transaction data in the data store.
    /// </summary>
    public interface ITransactionDataStore
    {
        /// <summary>
        /// Records a transaction in the data store.
        /// </summary>
        /// <param name="transaction">The transaction to record.</param>
        void RecordTransaction(Transaction transaction);

        /// <summary>
        /// Retrieves all transactions for a specific customer by their customer ID.
        /// </summary>
        /// <param name="customerId">The unique identifier of the customer.</param>
        /// <returns>A list of transactions for the specified customer.</returns>
        List<Transaction> GetTransactionsByCustomerId(string customerId);
    }
}
