namespace Smartwyre.DeveloperTest.Types
{
    /// <summary>
    /// Represents the result of a transaction, such as a purchase.
    /// </summary>
    public class TransactionResult
    {
        /// <summary>
        /// Gets or sets a value indicating whether the transaction was successful.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the remaining balance of the customer after the transaction.
        /// </summary>
        public decimal RemainingBalance { get; set; }

        /// <summary>
        /// Gets or sets the error message if the transaction failed.
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
