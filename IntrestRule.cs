using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GICBankingApp
{
    public class InterestRule
    {
        public string RuleId { get; set; }
        public double Rate { get; set; }
        public DateTime Date { get; set; }

        public InterestRule(DateTime date, string ruleId, double rate)
        {
            Date = date;
            RuleId = ruleId;
            Rate = rate;
        }
    }
}