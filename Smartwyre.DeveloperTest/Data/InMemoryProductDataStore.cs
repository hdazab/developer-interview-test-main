using Smartwyre.DeveloperTest.Types;
using System.Collections.Generic;

namespace Smartwyre.DeveloperTest.Data
{
    /// <summary>
    /// In-memory implementation of the IProductDataStore interface.
    /// Stores product data in memory for testing or non-persistent scenarios.
    /// </summary>
    public class InMemoryProductDataStore : IProductDataStore
    {
        // Dictionary to store products using their ProductIdentifier as the key
        private readonly Dictionary<string, Product> _products = new Dictionary<string, Product>();

        /// <summary>
        /// Retrieves a product by its unique identifier.
        /// </summary>
        /// <param name="productIdentifier">The unique identifier of the product.</param>
        /// <returns>A product object, or null if the product does not exist.</returns>
        public Product GetProduct(string productIdentifier)
        {
            _products.TryGetValue(productIdentifier, out var product);
            return product;
        }

        /// <summary>
        /// Adds a product to the in-memory data store.
        /// </summary>
        /// <param name="product">The product object to add.</param>
        public void AddProduct(Product product)
        {
            _products[product.ProductIdentifier] = product;
        }
    }
}
