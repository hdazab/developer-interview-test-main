using Smartwyre.DeveloperTest.Types;
using Smartwyre.DeveloperTest.Services.Calculators;

namespace Smartwyre.DeveloperTest.Services
{
    /// <summary>
    /// Interface for a factory that provides the appropriate rebate calculator 
    /// based on the type of incentive.
    /// </summary>
    public interface IRebateCalculatorFactory
    {
        /// <summary>
        /// Retrieves the appropriate rebate calculator for the given incentive type.
        /// </summary>
        /// <param name="incentiveType">The type of incentive.</param>
        /// <returns>An instance of IRebateCalculator that corresponds to the incentive type.</returns>
        IRebateCalculator GetCalculator(IncentiveType incentiveType);

        /// <summary>
        /// Registers a new incentive type and its corresponding rebate calculator.
        /// </summary>
        /// <param name="incentiveType">The type of incentive.</param>
        /// <param name="calculator">The rebate calculator for the incentive type.</param>
        void RegisterCalculator(IncentiveType incentiveType, IRebateCalculator calculator);
    }
}
