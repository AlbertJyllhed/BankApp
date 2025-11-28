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

        internal string GetInterestInfo(decimal amount)
        {
            return $"Du har satt in {amount} {Currency} i ditt sparkonto\n" +
                $"Räntan på sparkonton är {Interest}%\n" +
                $"Efter ett år kommer du ha {amount * Interest} {Currency} på ditt sparkonto";
        }
    }
}
