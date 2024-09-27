using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;
using Xunit;
using System.Collections.Generic;

namespace Smartwyre.DeveloperTest.Tests
{
    /// <summary>
    /// Unit tests for the InMemoryTransactionDataStore class.
    /// Verifies that transactions are stored and retrieved correctly.
    /// </summary>
    public class InMemoryTransactionDataStoreTests
    {
        private readonly InMemoryTransactionDataStore _transactionDataStore;

        /// <summary>
        /// Initializes a new instance of the InMemoryTransactionDataStoreTests class.
        /// </summary>
        public InMemoryTransactionDataStoreTests()
        {
            _transactionDataStore = new InMemoryTransactionDataStore();
        }

        /// <summary>
        /// Tests that a transaction can be successfully recorded and retrieved by customer ID.
        /// </summary>
        [Fact]
        public void RecordTransaction_And_GetTransactionsByCustomerId_Returns_Correct_Transactions()
        {
            // Arrange
            var transaction = new Transaction
            {
                CustomerId = "Cust001",
                ProductId = "Prod001",
                Quantity = 2,
                TotalAmount = 200m
            };

            // Act
            _transactionDataStore.RecordTransaction(transaction);
            var result = _transactionDataStore.GetTransactionsByCustomerId("Cust001");

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);  // Only one transaction recorded
            Assert.Equal(200m, result[0].TotalAmount);
            Assert.Equal(2, result[0].Quantity);
        }

        /// <summary>
        /// Tests that multiple transactions can be stored and retrieved for a customer.
        /// </summary>
        [Fact]
        public void RecordMultipleTransactions_And_GetTransactionsByCustomerId_Returns_Correct_Transactions()
        {
            // Arrange
            var transaction1 = new Transaction
            {
                CustomerId = "Cust001",
                ProductId = "Prod001",
                Quantity = 1,
                TotalAmount = 100m
            };

            var transaction2 = new Transaction
            {
                CustomerId = "Cust001",
                ProductId = "Prod002",
                Quantity = 2,
                TotalAmount = 300m
            };

            // Act
            _transactionDataStore.RecordTransaction(transaction1);
            _transactionDataStore.RecordTransaction(transaction2);
            var result = _transactionDataStore.GetTransactionsByCustomerId("Cust001");

            // Assert
            Assert.Equal(2, result.Count);  // Two transactions recorded
            Assert.Equal(100m, result[0].TotalAmount);
            Assert.Equal(300m, result[1].TotalAmount);
        }

        /// <summary>
        /// Tests that retrieving transactions for a non-existent customer returns an empty list.
        /// </summary>
        [Fact]
        public void GetTransactionsByCustomerId_Returns_EmptyList_When_NoTransactionsFound()
        {
            // Act
            var result = _transactionDataStore.GetTransactionsByCustomerId("NonExistentCustomer");

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        /// <summary>
        /// Tests that transactions for different customers are stored and retrieved independently.
        /// </summary>
        [Fact]
        public void RecordTransaction_ForDifferentCustomers_Stores_Independently()
        {
            // Arrange
            var transactionCust1 = new Transaction
            {
                CustomerId = "Cust001",
                ProductId = "Prod001",
                Quantity = 1,
                TotalAmount = 100m
            };

            var transactionCust2 = new Transaction
            {
                CustomerId = "Cust002",
                ProductId = "Prod002",
                Quantity = 1,
                TotalAmount = 200m
            };

            // Act
            _transactionDataStore.RecordTransaction(transactionCust1);
            _transactionDataStore.RecordTransaction(transactionCust2);

            var resultCust1 = _transactionDataStore.GetTransactionsByCustomerId("Cust001");
            var resultCust2 = _transactionDataStore.GetTransactionsByCustomerId("Cust002");

            // Assert
            Assert.Single(resultCust1);
            Assert.Single(resultCust2);
            Assert.Equal(100m, resultCust1[0].TotalAmount);
            Assert.Equal(200m, resultCust2[0].TotalAmount);
        }
    }
}
