using Newtonsoft.Json;

namespace BankApp
{
    internal class Loan
    {
        public decimal Amount { get; private set; }

        [JsonConstructor]
        internal Loan(decimal amount)
        {
            Amount = amount;
        }

        internal static string GetLoanInfo(decimal total, decimal maxLoan)
        {
            return $"Ditt totala belopp (inklusive lånade pengar): {Math.Round(total,2)} SEK\n" +
                $"Maximal summan du kan låna: {Math.Round(maxLoan,2)} SEK\n" +
                $"Är du säker på att du vill låna pengar? y/n";
        }

        internal decimal GetLoanWithoutInterest()
        {
            return Amount;
        }

        public override string ToString()
        {
            return $"Lån: {Math.Round(Amount, 2)} SEK";
        }

        internal void ReduceLoan(decimal payment)
        {
            if (payment <= 0)
            {
                return;
            }

            Amount -= payment;

            if (Amount <= 0)
            {
                Amount = 0;
            }
        }
    }
}
