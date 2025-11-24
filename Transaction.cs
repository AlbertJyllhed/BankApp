using BankApp.BankAccounts;

namespace BankApp
{
    internal class Transaction
    {
        public BankAccount From { get; set; }
        public BankAccount To { get; set; }
        public decimal TransactionAmount { get; set; }
    }
}
