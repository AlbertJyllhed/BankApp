namespace BankApp
{
    internal class Loan
    {
        private decimal Amount { get; set; }

        internal Loan(decimal amount)
        {
            Amount = amount;
        }

        internal static string GetLoanInfo(decimal total, decimal maxLoan)
        {
            return $"Ditt totala belopp (inklusive lånade pengar): {total} SEK\n" +
                $"Maximal summan du kan låna: {maxLoan} SEK\n" +
                $"Är du säker på att du vill låna pengar? y/n";
        }

        internal decimal GetTotalLoan()
        {
            return Amount;
        }

        internal decimal GetLoanWithoutInterest()
        {
            return Amount;
        }

        public override string ToString()
        {
            return $"Lån: {Amount} SEK";
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
