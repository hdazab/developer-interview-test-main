using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests
{
    /// <summary>
    /// Unit tests for the InMemoryProductDataStore class.
    /// Verifies that product data is stored and retrieved correctly.
    /// </summary>
    public class InMemoryProductDataStoreTests
    {
        private readonly InMemoryProductDataStore _productDataStore;

        public InMemoryProductDataStoreTests()
        {
            _productDataStore = new InMemoryProductDataStore();
        }

        /// <summary>
        /// Tests that a product can be successfully added and retrieved by ID.
        /// </summary>
        [Fact]
        public void AddProduct_And_GetProduct_Returns_Correct_Product()
        {
            // Arrange
            var product = new Product { ProductIdentifier = "Prod001", Name = "Laptop", Price = 1000m };

            // Act
            _productDataStore.AddProduct(product);
            var result = _productDataStore.GetProduct("Prod001");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Laptop", result.Name);
            Assert.Equal(1000m, result.Price);
        }

        /// <summary>
        /// Tests that retrieving a non-existent product returns null.
        /// </summary>
        [Fact]
        public void GetProduct_Returns_Null_When_Product_Not_Found()
        {
            // Act
            var result = _productDataStore.GetProduct("NonExistentProd");

            // Assert
            Assert.Null(result);
        }
    }
}
