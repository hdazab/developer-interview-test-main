namespace Smartwyre.DeveloperTest.Types
{
    /// <summary>
    /// Represents a customer in the system.
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Gets or sets the unique identifier for the customer.
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the balance of the customer.
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// Gets or sets the name of the customer.
        /// </summary>
        public string Name { get; set; }
    }
}
