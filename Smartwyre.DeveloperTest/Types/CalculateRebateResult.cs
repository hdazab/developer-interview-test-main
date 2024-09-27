namespace Smartwyre.DeveloperTest.Types
{
    // Represents the result of a rebate calculation
    public class CalculateRebateResult
    {
        /// <summary>
        /// Indicates whether the rebate calculation was successful.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// The calculated amount of the rebate, if successful.
        /// </summary>
        public decimal RebateAmount { get; set; }

        /// <summary>
        // Gets or sets an error message indicating why the rebate calculation failed.
        /// </summary>
        public string ErrorMessage { get; set; }

    }
}
