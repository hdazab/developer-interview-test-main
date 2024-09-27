namespace Smartwyre.DeveloperTest.Types
{
    /// <summary>
    /// Represents the result of a rebate calculation.
    /// This object is stored after a successful rebate calculation.
    /// </summary>
    public class RebateCalculation
    {
        /// <summary>
        /// Gets or sets the unique identifier of the rebate.
        /// </summary>
        public string RebateIdentifier { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the product.
        /// </summary>
        public string ProductIdentifier { get; set; }

        /// <summary>
        /// Gets or sets the total amount of rebate that was calculated.
        /// </summary>
        public decimal RebateAmount { get; set; }
    }
}
