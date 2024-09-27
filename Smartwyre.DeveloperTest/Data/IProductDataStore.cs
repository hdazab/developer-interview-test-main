using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data
{
    // Define the interface for product data store operations
    public interface IProductDataStore
    {
        /// <summary>
        /// Retrieves a product based on the identifier provided.
        /// </summary>
        /// <param name="productIdentifier">The identifier for the product to retrieve.</param>
        /// <returns>The retrieved Product object.</returns>
        Product GetProduct(string productIdentifier);

        /// <summary>
        /// Adds a product to the data store.
        /// </summary>
        /// <param name="product">The product to add to the store.</param>
        void AddProduct(Product product);
    }
}
