using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;
using System.Collections.Generic;

namespace Smartwyre.DeveloperTest.Services
{
    /// <summary>
    /// Service responsible for handling customer transactions such as purchases and payments.
    /// </summary>
    public class CustomerTransactionService
    {
        private readonly ICustomerDataStore _customerDataStore;
        private readonly ITransactionDataStore _transactionDataStore;
        private readonly IProductDataStore _productDataStore;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerTransactionService"/> class with the required dependencies.
        /// </summary>
        /// <param name="customerDataStore">The data store for accessing and updating customer information.</param>
        /// <param name="transactionDataStore">The data store for recording transaction history.</param>
        /// <param name="productDataStore">The data store for accessing product information.</param>
        public CustomerTransactionService(ICustomerDataStore customerDataStore, ITransactionDataStore transactionDataStore, IProductDataStore productDataStore)
        {
            _customerDataStore = customerDataStore;
            _transactionDataStore = transactionDataStore;
            _productDataStore = productDataStore;
        }

        /// <summary>
        /// Processes a purchase for the customer, deducting the cost from their balance and recording the transaction.
        /// </summary>
        /// <param name="customerId">The unique identifier of the customer.</param>
        /// <param name="productId">The unique identifier of the product being purchased.</param>
        /// <param name="quantity">The quantity of the product being purchased.</param>
        /// <returns>A <see cref="TransactionResult"/> object indicating the success or failure of the transaction.</returns>
        public TransactionResult ProcessPurchase(string customerId, string productId, int quantity)
        {
            // Retrieve customer and product information
            var customer = _customerDataStore.GetCustomer(customerId);
            var product = _productDataStore.GetProduct(productId);

            if (customer == null)
            {
                return new TransactionResult
                {
                    Success = false,
                    ErrorMessage = "Customer not found."
                };
            }

            if (product == null)
            {
                return new TransactionResult
                {
                    Success = false,
                    ErrorMessage = "Product not found."
                };
            }

            // Check if quantity is valid
            if (quantity <= 0)
            {
                return new TransactionResult
                {
                    Success = false,
                    ErrorMessage = "Invalid quantity."
                };
            }

            var totalCost = product.Price * quantity;

            // Check if the customer has enough balance
            if (customer.Balance < totalCost)
            {
                return new TransactionResult
                {
                    Success = false,
                    ErrorMessage = "Insufficient balance."
                };
            }

            // Deduct the total cost from the customer's balance
            customer.Balance -= totalCost;
            _customerDataStore.UpdateCustomerBalance(customer);

            // Record the transaction
            var transaction = new Transaction
            {
                CustomerId = customerId,
                ProductId = productId,
                Quantity = quantity,
                TotalAmount = totalCost
            };
            _transactionDataStore.RecordTransaction(transaction);

            return new TransactionResult
            {
                Success = true,
                RemainingBalance = customer.Balance
            };
        }


        /// <summary>
        /// Retrieves the transaction history for a specific customer.
        /// </summary>
        /// <param name="customerId">The unique identifier of the customer.</param>
        /// <returns>A list of <see cref="Transaction"/> objects representing the customer's transaction history.</returns>
        public List<Transaction> GetTransactionHistory(string customerId)
        {
            return _transactionDataStore.GetTransactionsByCustomerId(customerId);
        }
    }
}
