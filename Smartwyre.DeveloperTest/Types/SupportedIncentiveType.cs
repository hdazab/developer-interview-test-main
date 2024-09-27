using System;

namespace Smartwyre.DeveloperTest.Types
{
    /// <summary>
    /// Flags to indicate the types of incentives that are supported by a product.
    /// </summary>
    [Flags]
    public enum SupportedIncentiveType
    {
        /// <summary>
        /// No incentives supported.
        /// </summary>
        None = 0,

        /// <summary>
        /// Supports a fixed cash amount per unit sold.
        /// </summary>
        FixedCashAmount = 1,

        /// <summary>
        /// Supports a percentage of the product's price as a rebate.
        /// </summary>
        FixedRateRebate = 2,

        /// <summary>
        /// Supports a specific amount for each unit of measure sold.
        /// </summary>
        AmountPerUom = 4
    }
}
