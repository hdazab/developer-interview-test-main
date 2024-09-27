using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests
{
    /// <summary>
    /// Unit tests for the InMemoryCustomerDataStore class.
    /// Verifies that customer data is stored, retrieved, and updated correctly.
    /// </summary>
    public class InMemoryCustomerDataStoreTests
    {
        private readonly InMemoryCustomerDataStore _customerDataStore;

        public InMemoryCustomerDataStoreTests()
        {
            _customerDataStore = new InMemoryCustomerDataStore();
        }

        /// <summary>
        /// Tests that a customer can be successfully added and retrieved by ID.
        /// </summary>
        [Fact]
        public void AddCustomer_And_GetCustomer_Returns_Correct_Customer()
        {
            // Arrange
            var customer = new Customer { CustomerId = "Cust001", Name = "John Doe", Balance = 500m };

            // Act
            _customerDataStore.AddCustomer(customer);
            var result = _customerDataStore.GetCustomer("Cust001");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("John Doe", result.Name);
            Assert.Equal(500m, result.Balance);
        }

        /// <summary>
        /// Tests that updating a customer's balance works as expected.
        /// </summary>
        [Fact]
        public void UpdateCustomerBalance_Updates_Balance_Correctly()
        {
            // Arrange
            var customer = new Customer { CustomerId = "Cust001", Name = "John Doe", Balance = 500m };
            _customerDataStore.AddCustomer(customer);

            // Act
            customer.Balance = 400m;
            _customerDataStore.UpdateCustomerBalance(customer);

            // Assert
            var result = _customerDataStore.GetCustomer("Cust001");
            Assert.Equal(400m, result.Balance);
        }

        /// <summary>
        /// Tests that retrieving a non-existent customer returns null.
        /// </summary>
        [Fact]
        public void GetCustomer_Returns_Null_When_Customer_Not_Found()
        {
            // Act
            var result = _customerDataStore.GetCustomer("NonExistentCust");

            // Assert
            Assert.Null(result);
        }
    }
}
