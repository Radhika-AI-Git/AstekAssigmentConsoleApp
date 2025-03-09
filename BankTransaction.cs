using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GICBankingApp
{
    public class BankTransaction
    {
        public string TransactionId { get; set; }
        public DateTime Date { get; set; }
        public string Account { get; set; }
        public string Type { get; set; }  // 'D' for deposit, 'W' for withdrawal
        public double Amount { get; set; }

        public BankTransaction(string transactionId, DateTime date, string account, string type, double amount)
        {
            TransactionId = transactionId;
            Date = date;
            Account = account;
            Type = type.ToUpper();
            Amount = amount;
        }
    }
}