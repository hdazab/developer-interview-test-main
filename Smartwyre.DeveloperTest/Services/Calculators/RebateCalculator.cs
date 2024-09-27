using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;

namespace Smartwyre.DeveloperTest.Services.Calculators
{
    public class RebateCalculator : IRebateCalculator
    {
        // Dictionary to store the logic for each incentive type
        private readonly Dictionary<IncentiveType, Func<Rebate, Product, CalculateRebateRequest, decimal>> _calculationStrategies;

        public RebateCalculator()
        {
            // Initialize the dictionary with the logic for each incentive type
            _calculationStrategies = new Dictionary<IncentiveType, Func<Rebate, Product, CalculateRebateRequest, decimal>>
            {
                { IncentiveType.FixedCashAmount, CalculateFixedCashAmount },
                { IncentiveType.FixedRateRebate, CalculateFixedRateRebate },
                { IncentiveType.AmountPerUom, CalculateAmountPerUom }
                // You can easily add more incentive types here
            };
        }

        /// <summary>
        /// Calculates the rebate based on the provided rebate, product, and request.
        /// </summary>
        public CalculateRebateResult Calculate(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            var result = new CalculateRebateResult();

            if (rebate == null || product == null || request == null || !_calculationStrategies.ContainsKey(rebate.Incentive))
            {
                result.Success = false;
                return result;
            }

            // Retrieve and execute the corresponding calculation logic from the dictionary
            var rebateAmount = _calculationStrategies[rebate.Incentive](rebate, product, request);

            result.RebateAmount = rebateAmount;
            result.Success = rebateAmount > 0;
            return result;
        }

        // Calculation logic for Fixed Cash Amount
        private decimal CalculateFixedCashAmount(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            return product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedCashAmount) ? rebate.Amount : 0m;
        }

        // Calculation logic for Fixed Rate Rebate
        private decimal CalculateFixedRateRebate(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedRateRebate) || product.Price == 0 || request.Volume == 0)
            {
                return 0m;
            }

            return product.Price * rebate.Percentage * request.Volume;
        }

        // Calculation logic for Amount Per Uom
        private decimal CalculateAmountPerUom(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            return product.SupportedIncentives.HasFlag(SupportedIncentiveType.AmountPerUom) && request.Volume > 0
                ? rebate.Amount * request.Volume
                : 0m;
        }
    }
}
