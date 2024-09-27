using System;

namespace Smartwyre.DeveloperTest.Types
{
    // Represents a product in the system
    public class Product
    {
        /// <summary>
        /// Unique identifier for the product.
        /// </summary>
        public string ProductIdentifier { get; set; }

        /// <summary>
        /// Name of the product.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Price of the product.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Flags to indicate the types of incentives that are supported by the product.
        /// </summary>
        public SupportedIncentiveType SupportedIncentives { get; set; }
    }
}
