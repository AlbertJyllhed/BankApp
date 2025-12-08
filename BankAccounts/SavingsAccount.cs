using BankApp.Users;
using System.Transactions;

namespace BankApp.BankAccounts
{
    internal class SavingsAccount : BankAccount
    {
        private decimal _interest = 1.0186m;

        // Constructors
        internal SavingsAccount(string name, decimal amount, string currency, string id = "") : base(name, currency, id)
        {
            AddBalance(amount);
        }

        // Print interest information for a given amount
        internal string GetInterestInfo(decimal amount)
        {
            return $"Du har satt in {amount} {Currency} i ditt sparkonto\n" +
                $"Räntan ligger för nuvarande på {_interest}%\n" +
                $"Efter ett år kommer du ha {Math.Round(amount * _interest, 2)} {Currency} på ditt sparkonto";
        }
    }
}
