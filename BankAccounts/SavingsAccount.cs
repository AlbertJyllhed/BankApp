using Newtonsoft.Json;

namespace BankApp.BankAccounts
{
    internal class SavingsAccount : BankAccount
    {
        private decimal _interest = 1.0186m;

        // Constructors
        [JsonConstructor]
        internal SavingsAccount(string name, decimal balance, string currency, string id = "") : base(name, balance, currency, id)
        {
        }

        // Print interest information for a given amount
        internal string GetInterestInfo(decimal amount)
        {
            return $"\nDu har satt in {amount} {Currency} i ditt sparkonto\n" +
                $"Räntan ligger för nuvarande på {_interest}%\n" +
                $"Efter ett år kommer du ha {Math.Round(amount * _interest, 2)} {Currency} på ditt sparkonto";
        }
    }
}
