using System;
using System.Transactions;

namespace GICBankingApp
{
    public class BankAccount
    {
        public string AccountId { get; set; }
        public double Balance { get; private set; }
        private List<BankTransaction> Transactions { get; set; }

        private int transactionCount;

        public BankAccount(string accountId)
        {
            AccountId = accountId;
            Balance = 0;
            Transactions = new List<BankTransaction>();
            transactionCount = 1;
        }

        // Method to handle a transaction
        public bool ProcessTransaction(DateTime date, string type, double amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Amount must be greater than zero.");
                return false;
            }
            
            if (type != "D" && type != "W" && type != "I")
            {
                Console.WriteLine("Invalid Account Type Transaction.");
                return false;
            }
            if (type == "W" && Balance - amount < 0)
            {
                Console.WriteLine("Insufficient balance for withdrawal.");
                return false;
            }

            string transactionId = $"{date:yyyyMMdd}-{transactionCount:D2}";
            var banktransaction = new BankTransaction(transactionId, date, AccountId, type, amount);

            if (type == "D")
            {
                Balance += amount;
            }
            else if (type == "W")
            {
                Balance -= amount;
            }

            Transactions.Add(banktransaction);
            transactionCount++;
            return true;
        }

        // This method used to get the account statement
        public string GetAccountStatement()
        {
            var statement = $"Account: {AccountId}\n";
            statement += "| Date    | Txn Id    | Type  | Amount    |\n";

            foreach (var txn in Transactions)
            {
                statement += $"| {txn.Date:yyyyMMdd} | {txn.TransactionId} | {txn.Type}   | {txn.Amount:F2} |\n";
            }

            return statement;
        }
        public string GetAccountStatement(DateTime date)
        {
            var startOfMonth = new DateTime(date.Year, date.Month, 1);
            var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

            var filteredTransactions = Transactions
                .Where(txn => txn.Date >= startOfMonth && txn.Date <= endOfMonth)
                .OrderBy(txn => txn.Date)
                .ToList();

            string statement = $"Account: {AccountId}\n";
            statement += "| Date    | Txn Id    | Type  | Amount    | Balance   |\n";

            double currentBalance = Balance;

            foreach (var txn in filteredTransactions)
            {
                if (txn.Type == "D" || txn.Type == "W")
                {
                    currentBalance = txn.Type == "D" ? currentBalance + txn.Amount : currentBalance - txn.Amount;
                }
                else if (txn.Type == "I") // Interest
                {
                    currentBalance += txn.Amount;
                }

                statement += $"| {txn.Date:yyyyMMdd} | {txn.TransactionId} | {txn.Type}   | {txn.Amount:F2} | {currentBalance:F2} |\n";
            }

            return statement;
        }
        public void ApplyInterest(DateTime date, double interestRate)
        {
            double interestAmount = Balance * (interestRate / 100);
            DateTime interestDate = new DateTime(date.Year, date.Month, 1);
            string transactionId = $"{interestDate:yyyyMMdd}-{Transactions.Count + 1:D2}";
            ProcessTransaction(interestDate, "I", interestAmount);
        }
    }
}