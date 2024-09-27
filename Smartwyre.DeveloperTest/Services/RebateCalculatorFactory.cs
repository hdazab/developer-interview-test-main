using Smartwyre.DeveloperTest.Services.Calculators;
using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;

namespace Smartwyre.DeveloperTest.Services
{
    public class RebateCalculatorFactory : IRebateCalculatorFactory
    {
        // Dictionary to map each incentive type to its calculator
        private readonly Dictionary<IncentiveType, IRebateCalculator> _calculatorRegistry;

        public RebateCalculatorFactory()
        {
            _calculatorRegistry = new Dictionary<IncentiveType, IRebateCalculator>
            {
                { IncentiveType.FixedCashAmount, new RebateCalculator() },
                { IncentiveType.FixedRateRebate, new RebateCalculator() },
                { IncentiveType.AmountPerUom, new RebateCalculator() }
                // You can add more incentives easily here
            };
        }

        /// <summary>
        /// Retrieves the appropriate rebate calculator for the given incentive type.
        /// </summary>
        public IRebateCalculator GetCalculator(IncentiveType incentiveType)
        {
            if (_calculatorRegistry.TryGetValue(incentiveType, out var calculator))
            {
                return calculator;
            }

            throw new InvalidOperationException($"No calculator registered for incentive type {incentiveType}");
        }

        /// <summary>
        /// Registers a new incentive type and its corresponding calculator.
        /// </summary>
        public void RegisterCalculator(IncentiveType incentiveType, IRebateCalculator calculator)
        {
            if (!_calculatorRegistry.ContainsKey(incentiveType))
            {
                _calculatorRegistry.Add(incentiveType, calculator);
            }
            else
            {
                throw new ArgumentException($"Calculator for incentive type {incentiveType} is already registered.");
            }
        }
    }
}
