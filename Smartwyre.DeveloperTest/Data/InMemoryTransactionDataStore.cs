using System.Collections.Generic;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data
{
    /// <summary>
    /// In-memory implementation of the ITransactionDataStore interface.
    /// Stores customer transactions in memory.
    /// </summary>
    public class InMemoryTransactionDataStore : ITransactionDataStore
    {
        // Simulated data store for transactions
        private readonly List<Transaction> _transactions = new List<Transaction>();

        /// <summary>
        /// Records a transaction for the customer.
        /// </summary>
        /// <param name="transaction">The transaction details to record.</param>
        public void RecordTransaction(Transaction transaction)
        {
            _transactions.Add(transaction);
        }

        /// <summary>
        /// Retrieves the transaction history for a specific customer.
        /// </summary>
        /// <param name="customerId">The unique identifier of the customer.</param>
        /// <returns>A list of <see cref="Transaction"/> objects representing the customer's transaction history.</returns>
        public List<Transaction> GetTransactionsByCustomerId(string customerId)
        {
            // Return the transactions for the specified customer
            return _transactions.FindAll(t => t.CustomerId == customerId);
        }
    }
}
