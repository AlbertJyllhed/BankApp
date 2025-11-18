using BankApp.Users;

namespace BankApp.BankAccounts
{
    internal class SavingsAccount : BankAccount
    {
        internal decimal Interest { get; set; }

        // Constructors
        internal SavingsAccount(string name, string currency, decimal interest = 1.0186m) : base(name, currency)
        {
            Interest = interest;
        }

        internal void PrintSavingsInterest(decimal amount)
        {
            Console.WriteLine($"Money put in savings {amount} {Currency}\n" +
                $"Savings interest {amount * Interest - amount}\n" +
                $"Total amount after interest in savings account {amount * Interest}");
        }
      
    }
}
