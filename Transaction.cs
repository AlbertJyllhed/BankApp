using BankApp.BankAccounts;
using System.Security.Principal;

namespace BankApp
{
    internal class Transaction
    {
        internal decimal Amount { get; set; }
        internal string Currency { get; }
        internal string Description { get; set; }
        internal DateTime TimeStamp { get; }

        internal Transaction(decimal amount, string currency, string description)
        {
            Amount = amount;
            Currency = currency;
            Description = description;
            TimeStamp = DateTime.Now;
        }

        // Override ToString method for transaction representation
        public override string ToString()
        {
            string dateFormat = TimeStamp.ToString("yyyy-MM-dd HH:mm");

            if (string.IsNullOrWhiteSpace(Description))
            {
                return $"{Amount} {Currency}, {dateFormat}";
            }

            return $"{Amount} {Currency} {Description}, {dateFormat}";
        }
    }
}
