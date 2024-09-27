using System.Collections.Generic;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data
{
    /// <summary>
    /// In-memory implementation of the ICustomerDataStore interface.
    /// Stores customer data in memory.
    /// </summary>
    public class InMemoryCustomerDataStore : ICustomerDataStore
    {
        // Simulated data store for customers
        private readonly Dictionary<string, Customer> _customers = new Dictionary<string, Customer>();

        /// <summary>
        /// Retrieves the customer by their unique identifier.
        /// </summary>
        /// <param name="customerId">The unique identifier of the customer.</param>
        /// <returns>A <see cref="Customer"/> object representing the customer, or null if not found.</returns>
        public Customer GetCustomer(string customerId)
        {
            _customers.TryGetValue(customerId, out var customer);
            return customer;
        }

        /// <summary>
        /// Adds a new customer to the data store.
        /// </summary>
        /// <param name="customer">The customer to add.</param>
        public void AddCustomer(Customer customer)
        {
            _customers[customer.CustomerId] = customer;
        }

        /// <summary>
        /// Updates the balance of a customer.
        /// </summary>
        /// <param name="customer">The customer whose balance needs to be updated.</param>
        public void UpdateCustomerBalance(Customer customer)
        {
            if (_customers.ContainsKey(customer.CustomerId))
            {
                _customers[customer.CustomerId] = customer;
            }
        }
    }
}
