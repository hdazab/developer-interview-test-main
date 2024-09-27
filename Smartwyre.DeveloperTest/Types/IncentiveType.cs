namespace Smartwyre.DeveloperTest.Types
{
    // Enumerates the types of incentives that can be applied in rebate calculations
    public enum IncentiveType
    {
        /// <summary>
        /// A fixed cash amount per unit sold.
        /// </summary>
        FixedCashAmount,

        /// <summary>
        /// A percentage of the product's price.
        /// </summary>
        FixedRateRebate,

        /// <summary>
        /// A specific amount for each unit of measure.
        /// </summary>
        AmountPerUom
    }
}
