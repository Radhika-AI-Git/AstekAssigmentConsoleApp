using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace GICBankingApp
{
    public class BankSystem
    {
        private Dictionary<string, BankAccount> accounts;
        private InterestRuleManager interestRuleManager;
        public BankSystem()
        {
            accounts = new Dictionary<string, BankAccount>();
            interestRuleManager = new InterestRuleManager();
        }
        public void DefineInterestRule()
        {
            Console.WriteLine("Please enter interest rules details in <Date> <RuleId> <Rate in %> format");
            Console.WriteLine("(or enter blank to go back to main menu):");

            string input = Console.ReadLine();

            if (string.IsNullOrEmpty(input)) return;

            string[] parts = input.Split(' ');

            if (parts.Length != 3)
            {
                Console.WriteLine("Invalid input format. Please try again.");
                return;
            }

            DateTime date;
            string ruleId = parts[1];
            double rate;

            if (!DateTime.TryParseExact(parts[0], "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out date))
            {
                Console.WriteLine("Invalid date format. Please use YYYYMMdd.");
                return;
            }

            if (!double.TryParse(parts[2], out rate))
            {
                Console.WriteLine("Invalid rate. Please enter a valid number for the rate.");
                return;
            }

            // Add or update the interest rule
            if (interestRuleManager.DefineInterestRule(date, ruleId, rate))
            {
                // Display all interest rules
                Console.WriteLine(interestRuleManager.GetInterestRulesStatement());
            }
        }
        // This Method used to get or create account
        public BankAccount GetOrCreateAccount(string accountId)
        {
            if (!accounts.ContainsKey(accountId))
            {
                accounts[accountId] = new BankAccount(accountId);
            }
            return accounts[accountId];
        }

        // This Method used to handle user input for transactions
        public void InputTransaction()
        {
            Console.WriteLine("Please enter transaction details in <Date> <Account> <Type> <Amount> format");
            Console.WriteLine("(or enter blank to go back to main menu):");

            string input = Console.ReadLine();

            if (string.IsNullOrEmpty(input)) return;

            string[] parts = input.Split(' ');

            if (parts.Length != 4)
            {
                Console.WriteLine("Invalid input format. Please try again.");
                return;
            }

            DateTime date;
            string accountId = parts[1];
            string type = parts[2].ToUpper();
            double amount;

            if (!DateTime.TryParseExact(parts[0], "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out date))
            {
                Console.WriteLine("Invalid date format. Please use YYYYMMdd.");
                return;
            }

            if (!double.TryParse(parts[3], out amount) || amount <= 0)
            {
                Console.WriteLine("Invalid amount. Amount must be greater than zero.");
                return;
            }

            var account = GetOrCreateAccount(accountId);

            // Process the transaction
            if (account.ProcessTransaction(date, type, amount))
            {
                Console.WriteLine(account.GetAccountStatement());
            }
        }
            public void PrintAccountStatement()
           {
            Console.WriteLine("Please enter account and month to generate the statement <Account> <Year><Month>");
            Console.WriteLine("(or enter blank to go back to main menu):");

            string input = Console.ReadLine();

            if (string.IsNullOrEmpty(input)) return;

            string[] parts = input.Split(' ');

            if (parts.Length != 2)
            {
                Console.WriteLine("Invalid input format. Please try again.");
                return;
            }

            string accountId = parts[0];
            string yearMonth = parts[1];

            if (yearMonth.Length != 6 || !int.TryParse(yearMonth, out int monthYear))
            {
                Console.WriteLine("Invalid month format. Please use YYYYMM.");
                return;
            }

            DateTime date = DateTime.ParseExact(yearMonth, "yyyyMM", null);
            BankAccount account = GetOrCreateAccount(accountId);

            // Apply interest for that month if an interest rule exists
            var interestRule = interestRuleManager.GetInterestRuleForMonth(date);
            if (interestRule != null)
            {
                account.ApplyInterest(date, interestRule.Rate);
            }

            // Print the account statement
            Console.WriteLine(account.GetAccountStatement(date));
        
    

        }
    }
}
