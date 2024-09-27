namespace Smartwyre.DeveloperTest.Types
{
    // Represents a rebate in the system
    public class Rebate
    {
        /// <summary>
        /// Unique identifier for the rebate.
        /// </summary>
        public string RebateIdentifier { get; set; }

        /// <summary>
        /// Type of incentive provided by this rebate.
        /// </summary>
        public IncentiveType Incentive { get; set; }

        /// <summary>
        /// Monetary amount associated with the rebate, if applicable.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Percentage for the rebate calculation, if applicable.
        /// </summary>
        public decimal Percentage { get; set; }
    }
}
