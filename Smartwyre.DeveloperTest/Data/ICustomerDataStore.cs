using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data
{
    /// <summary>
    /// Interface for managing customer data in the data store.
    /// </summary>
    public interface ICustomerDataStore
    {
        /// <summary>
        /// Retrieves a customer by their unique ID.
        /// </summary>
        /// <param name="customerId">The unique identifier of the customer.</param>
        /// <returns>A Customer object if found, otherwise null.</returns>
        Customer GetCustomer(string customerId);

        /// <summary>
        /// Adds a new customer to the data store.
        /// If a customer with the same ID exists, their data is updated.
        /// </summary>
        /// <param name="customer">The customer to add or update.</param>
        void AddCustomer(Customer customer);

        /// <summary>
        /// Updates the balance of an existing customer.
        /// </summary>
        /// <param name="customer">The customer with the updated balance.</param>
        void UpdateCustomerBalance(Customer customer);
    }
}
