using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests
{
    /// <summary>
    /// Unit tests for the ICustomerDataStore and its in-memory implementation.
    /// Verifies that customer data is stored, retrieved, and updated correctly.
    /// </summary>
    public class ICustomerDataStoreTests
    {
        private readonly InMemoryCustomerDataStore _customerDataStore;

        /// <summary>
        /// Initializes a new instance of the ICustomerDataStoreTests class
        /// and creates an in-memory data store for testing purposes.
        /// </summary>
        public ICustomerDataStoreTests()
        {
            _customerDataStore = new InMemoryCustomerDataStore();
        }

        /// <summary>
        /// Tests that a customer can be successfully added and retrieved by their ID.
        /// </summary>
        [Fact]
        public void AddCustomer_And_GetCustomer_Returns_Correct_Customer()
        {
            // Arrange
            var customer = new Customer
            {
                CustomerId = "Cust001",
                Name = "John Doe",
                Balance = 500m
            };

            // Act
            _customerDataStore.AddCustomer(customer);
            var result = _customerDataStore.GetCustomer("Cust001");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("John Doe", result.Name);
            Assert.Equal(500m, result.Balance);
        }

        /// <summary>
        /// Tests that adding a customer with the same ID updates the existing customer's data.
        /// </summary>
        [Fact]
        public void AddCustomer_Updates_Existing_Customer()
        {
            // Arrange
            var customer = new Customer
            {
                CustomerId = "Cust001",
                Name = "John Doe",
                Balance = 500m
            };

            _customerDataStore.AddCustomer(customer);

            // Act
            var updatedCustomer = new Customer
            {
                CustomerId = "Cust001",
                Name = "Jane Doe",
                Balance = 800m
            };
            _customerDataStore.AddCustomer(updatedCustomer);
            var result = _customerDataStore.GetCustomer("Cust001");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Jane Doe", result.Name); // Check if name is updated
            Assert.Equal(800m, result.Balance);    // Check if balance is updated
        }

        /// <summary>
        /// Tests that updating a customer's balance works correctly.
        /// </summary>
        [Fact]
        public void UpdateCustomerBalance_Updates_Balance_Correctly()
        {
            // Arrange
            var customer = new Customer
            {
                CustomerId = "Cust001",
                Name = "John Doe",
                Balance = 500m
            };

            _customerDataStore.AddCustomer(customer);

            // Act
            customer.Balance = 400m;
            _customerDataStore.UpdateCustomerBalance(customer);
            var result = _customerDataStore.GetCustomer("Cust001");

            // Assert
            Assert.Equal(400m, result.Balance); // Check if balance is updated
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

        /// <summary>
        /// Tests that adding multiple customers works and retrieves them independently.
        /// </summary>
        [Fact]
        public void AddMultipleCustomers_Retrieves_Them_Independently()
        {
            // Arrange
            var customer1 = new Customer
            {
                CustomerId = "Cust001",
                Name = "John Doe",
                Balance = 500m
            };
            var customer2 = new Customer
            {
                CustomerId = "Cust002",
                Name = "Jane Doe",
                Balance = 600m
            };

            // Act
            _customerDataStore.AddCustomer(customer1);
            _customerDataStore.AddCustomer(customer2);
            var result1 = _customerDataStore.GetCustomer("Cust001");
            var result2 = _customerDataStore.GetCustomer("Cust002");

            // Assert
            Assert.NotNull(result1);
            Assert.Equal("John Doe", result1.Name);
            Assert.Equal(500m, result1.Balance);

            Assert.NotNull(result2);
            Assert.Equal("Jane Doe", result2.Name);
            Assert.Equal(600m, result2.Balance);
        }
    }
}
