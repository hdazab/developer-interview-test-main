using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;
using Xunit;
using System.Collections.Generic;

namespace Smartwyre.DeveloperTest.Tests
{
    /// <summary>
    /// Unit tests for the InMemoryRebateDataStore class.
    /// Verifies that rebates and rebate calculations are stored and retrieved correctly.
    /// </summary>
    public class InMemoryRebateDataStoreTests
    {
        private readonly InMemoryRebateDataStore _rebateDataStore;

        /// <summary>
        /// Initializes a new instance of the InMemoryRebateDataStoreTests class.
        /// </summary>
        public InMemoryRebateDataStoreTests()
        {
            _rebateDataStore = new InMemoryRebateDataStore();
        }

        /// <summary>
        /// Tests that a rebate can be successfully added and retrieved by its identifier.
        /// </summary>
        [Fact]
        public void AddRebate_And_GetRebate_Returns_Correct_Rebate()
        {
            // Arrange
            var rebate = new Rebate
            {
                RebateIdentifier = "Reb001",
                Incentive = IncentiveType.FixedCashAmount,
                Amount = 200m
            };

            // Act
            _rebateDataStore.AddRebate(rebate);
            var result = _rebateDataStore.GetRebate("Reb001");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Reb001", result.RebateIdentifier);
            Assert.Equal(IncentiveType.FixedCashAmount, result.Incentive);
            Assert.Equal(200m, result.Amount);
        }

        /// <summary>
        /// Tests that adding a rebate with the same identifier updates the existing rebate.
        /// </summary>
        [Fact]
        public void AddRebate_Updates_Existing_Rebate()
        {
            // Arrange
            var rebate = new Rebate
            {
                RebateIdentifier = "Reb001",
                Incentive = IncentiveType.FixedCashAmount,
                Amount = 200m
            };

            _rebateDataStore.AddRebate(rebate);

            // Act
            // Add a rebate with the same identifier but different amount
            var updatedRebate = new Rebate
            {
                RebateIdentifier = "Reb001",
                Incentive = IncentiveType.FixedCashAmount,
                Amount = 300m
            };
            _rebateDataStore.AddRebate(updatedRebate);
            var result = _rebateDataStore.GetRebate("Reb001");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(300m, result.Amount); // Check if amount is updated
        }

        /// <summary>
        /// Tests that retrieving a non-existent rebate returns null.
        /// </summary>
        [Fact]
        public void GetRebate_Returns_Null_When_Rebate_Not_Found()
        {
            // Act
            var result = _rebateDataStore.GetRebate("NonExistentReb");

            // Assert
            Assert.Null(result);
        }
    }
}
