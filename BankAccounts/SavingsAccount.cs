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
            Console.WriteLine($"Money put in savings {amount} {Currency}\n" +
                $"Savings interest is {Interest}%\n" +
                $"After One Year you will have {amount * Interest} {Currency}\n"); //+
                //$"Total amount after interest in savings account {amount * Interest} {Currency}");

        }

        internal override void AddBalance(decimal value)
        {
            Balance += value;
            Console.WriteLine($"{value} {Currency} was transfered to account {ID}.");
        }
    }
}
