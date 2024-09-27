using System;

namespace Smartwyre.DeveloperTest.Types
{
    /// <summary>
    /// Represents a customer transaction, such as a purchase.
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// Gets or sets the unique identifier of the customer who made the transaction.
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the product involved in the transaction.
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the product purchased in the transaction.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the total amount of the transaction.
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets the amount of rebate applied to the transaction.
        /// </summary>
        public decimal RebateAmount { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the transaction occurred.
        /// </summary>
        public DateTime TransactionDate { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Transaction"/> class.
        /// </summary>
        public Transaction()
        {
            TransactionDate = DateTime.Now;
        }
    }
}
