using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GICBankingApp
{
    public class InterestRuleManager
    {
        private List<InterestRule> rules;

        public InterestRuleManager()
        {
            rules = new List<InterestRule>();
        }

        // This Method used to define interest rule
        public bool DefineInterestRule(DateTime date, string ruleId, double rate)
        {
            if (rate <= 0 || rate >= 100)
            {
                Console.WriteLine("Interest rate must be greater than 0 and less than 100.");
                return false;
            }

            // This mehtod used to Find if there's already a rule for this date
            var existingRule = rules.FirstOrDefault(r => r.Date == date);
            if (existingRule != null)
            {
                // Remove the old rule for the same date
                rules.Remove(existingRule);
            }

            // Add the new rule
            rules.Add(new InterestRule(date, ruleId, rate));
            return true;
        }

        // This  Method used to get all interest rules sorted by date
        public string GetInterestRulesStatement()
        {
            var sortedRules = rules.OrderBy(r => r.Date).ToList();
            string statement = "Interest rules:\n";
            statement += "| Date     | RuleId  | Rate (%) |\n";

            foreach (var rule in sortedRules)
            {
                statement += $"| {rule.Date:yyyyMMdd} | {rule.RuleId} | {rule.Rate:F2} |\n";
            }

            return statement;
        }

        //This method is used to get the interestrule by month.
        public InterestRule GetInterestRuleForMonth(DateTime date)
        {
            return rules
                .Where(rule => rule.Date.Month == date.Month && rule.Date.Year == date.Year)
                .OrderByDescending(rule => rule.Date)
                .FirstOrDefault();
        }
    }
}