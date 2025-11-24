using BankApp.Users;
using System.Transactions;

namespace BankApp.BankAccounts
{
    internal class SavingsAccount : BankAccount
    {
        private decimal Interest { get; }

        // Constructors
        internal SavingsAccount(string name, string currency, decimal interest = 1.0186m) : base(name, currency)
        {
            Interest = interest;
        }

        internal void PrintSavingsInterest(decimal amount)
        {
            Console.WriteLine($"Money put in savings account {amount} {Currency}\n" +
                $"Savings interest is {Interest}%\n" +
                $"After one year you will have {amount * Interest} {Currency}");
        }
    }
}
