using Moq;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;
using Smartwyre.DeveloperTest.Services;
using Xunit;
using System.Collections.Generic;

namespace Smartwyre.DeveloperTest.Tests.Services
{
    /// <summary>
    /// Unit tests for the CustomerTransactionService class.
    /// Verifies that customer transactions such as purchases are handled correctly.
    /// </summary>
    public class CustomerTransactionServiceTests
    {
        private readonly Mock<ICustomerDataStore> _customerDataStoreMock;
        private readonly Mock<ITransactionDataStore> _transactionDataStoreMock;
        private readonly Mock<IProductDataStore> _productDataStoreMock;
        private readonly CustomerTransactionService _customerTransactionService;

        /// <summary>
        /// Initializes a new instance of the CustomerTransactionServiceTests class,
        /// setting up the necessary mocks and creating an instance of the service.
        /// </summary>
        public CustomerTransactionServiceTests()
        {
            _customerDataStoreMock = new Mock<ICustomerDataStore>();
            _transactionDataStoreMock = new Mock<ITransactionDataStore>();
            _productDataStoreMock = new Mock<IProductDataStore>();
            _customerTransactionService = new CustomerTransactionService(
                _customerDataStoreMock.Object,
                _transactionDataStoreMock.Object,
                _productDataStoreMock.Object);
        }

        /// <summary>
        /// Tests that the purchase process succeeds when the customer has sufficient balance
        /// and the product is valid.
        /// </summary>
        [Fact]
        public void ProcessPurchase_Succeeds_When_Customer_Has_Sufficient_Balance()
        {
            // Arrange
            var customerId = "Cust001";
            var productId = "Prod001";
            var productPrice = 100m;
            var quantity = 2;
            var totalCost = productPrice * quantity;
            var initialBalance = 500m;

            var customer = new Customer { CustomerId = customerId, Balance = initialBalance };
            var product = new Product { ProductIdentifier = productId, Price = productPrice };

            _customerDataStoreMock.Setup(c => c.GetCustomer(customerId)).Returns(customer);
            _productDataStoreMock.Setup(p => p.GetProduct(productId)).Returns(product);

            // Act
            var result = _customerTransactionService.ProcessPurchase(customerId, productId, quantity);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(initialBalance - totalCost, result.RemainingBalance);
            _transactionDataStoreMock.Verify(t => t.RecordTransaction(It.IsAny<Transaction>()), Times.Once);
            _customerDataStoreMock.Verify(c => c.UpdateCustomerBalance(It.IsAny<Customer>()), Times.Once);
        }

        /// <summary>
        /// Tests that the purchase process fails when the customer has insufficient balance.
        /// </summary>
        [Fact]
        public void ProcessPurchase_Fails_When_Customer_Has_Insufficient_Balance()
        {
            // Arrange
            var customerId = "Cust001";
            var productId = "Prod001";
            var productPrice = 100m;
            var quantity = 5;
            var totalCost = productPrice * quantity;
            var initialBalance = 300m;

            var customer = new Customer { CustomerId = customerId, Balance = initialBalance };
            var product = new Product { ProductIdentifier = productId, Price = productPrice };

            _customerDataStoreMock.Setup(c => c.GetCustomer(customerId)).Returns(customer);
            _productDataStoreMock.Setup(p => p.GetProduct(productId)).Returns(product);

            // Act
            var result = _customerTransactionService.ProcessPurchase(customerId, productId, quantity);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Insufficient balance.", result.ErrorMessage);
            _transactionDataStoreMock.Verify(t => t.RecordTransaction(It.IsAny<Transaction>()), Times.Never);
            _customerDataStoreMock.Verify(c => c.UpdateCustomerBalance(It.IsAny<Customer>()), Times.Never);
        }

        /// <summary>
        /// Tests that the purchase process fails when the customer does not exist.
        /// </summary>
        [Fact]
        public void ProcessPurchase_Fails_When_Customer_Not_Found()
        {
            // Arrange
            var customerId = "Cust001";
            var productId = "Prod001";
            var quantity = 1;

            _customerDataStoreMock.Setup(c => c.GetCustomer(customerId)).Returns((Customer)null);

            // Act
            var result = _customerTransactionService.ProcessPurchase(customerId, productId, quantity);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Customer not found.", result.ErrorMessage);
            _transactionDataStoreMock.Verify(t => t.RecordTransaction(It.IsAny<Transaction>()), Times.Never);
        }

        /// <summary>
        /// Tests that the purchase process fails when the product does not exist.
        /// </summary>
        [Fact]
        public void ProcessPurchase_Fails_When_Product_Not_Found()
        {
            // Arrange
            var customerId = "Cust001";
            var productId = "Prod001";
            var quantity = 1;

            var customer = new Customer { CustomerId = customerId, Balance = 500m };

            _customerDataStoreMock.Setup(c => c.GetCustomer(customerId)).Returns(customer);
            _productDataStoreMock.Setup(p => p.GetProduct(productId)).Returns((Product)null);

            // Act
            var result = _customerTransactionService.ProcessPurchase(customerId, productId, quantity);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Product not found.", result.ErrorMessage);
            _transactionDataStoreMock.Verify(t => t.RecordTransaction(It.IsAny<Transaction>()), Times.Never);
        }

        /// <summary>
        /// Tests that the purchase process fails if the quantity is zero or negative.
        /// </summary>
        [Fact]
        public void ProcessPurchase_Fails_When_Quantity_Is_Zero_Or_Negative()
        {
            // Arrange
            var customerId = "Cust001";
            var productId = "Prod001";
            var quantity = 0;

            var customer = new Customer { CustomerId = customerId, Balance = 500m };
            var product = new Product { ProductIdentifier = productId, Price = 100m };

            _customerDataStoreMock.Setup(c => c.GetCustomer(customerId)).Returns(customer);
            _productDataStoreMock.Setup(p => p.GetProduct(productId)).Returns(product);

            // Act
            var result = _customerTransactionService.ProcessPurchase(customerId, productId, quantity);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Invalid quantity.", result.ErrorMessage);
            _transactionDataStoreMock.Verify(t => t.RecordTransaction(It.IsAny<Transaction>()), Times.Never);
        }

        /// <summary>
        /// Tests that GetTransactionHistory returns the correct list of transactions for a customer.
        /// </summary>
        [Fact]
        public void GetTransactionHistory_Returns_Correct_Transactions()
        {
            // Arrange
            var customerId = "Cust001";
            var transactions = new List<Transaction>
            {
                new Transaction { CustomerId = customerId, ProductId = "Prod001", Quantity = 1, TotalAmount = 100m },
                new Transaction { CustomerId = customerId, ProductId = "Prod002", Quantity = 2, TotalAmount = 200m }
            };

            _transactionDataStoreMock.Setup(t => t.GetTransactionsByCustomerId(customerId)).Returns(transactions);

            // Act
            var result = _customerTransactionService.GetTransactionHistory(customerId);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("Prod001", result[0].ProductId);
            Assert.Equal("Prod002", result[1].ProductId);
        }
    }
}
