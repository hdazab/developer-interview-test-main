namespace Smartwyre.DeveloperTest.Types
{
    // Represents a request to calculate a rebate
    public class CalculateRebateRequest
    {
        /// <summary>
        /// Identifier for the rebate
        /// </summary>
        public string RebateIdentifier { get; set; }

        /// <summary>
        /// Identifier for the product
        /// </summary>
        public string ProductIdentifier { get; set; }

        /// <summary>
        /// Volume of product being considered for the rebate
        /// </summary>
        public decimal Volume { get; set; }
    }
}
