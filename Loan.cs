namespace BankApp
{
    internal class Loan
    {
        private decimal Amount { get; set; }
        private decimal Interest { get; set; }
        private decimal InterestGained;

        internal Loan(decimal amount, decimal interest = 1.0284m)
        {
            Amount = amount;
            Interest = interest;
            InterestGained = amount * (interest - 1);
        }

        internal static string GetLoanInfo(decimal total, decimal maxLoan)
        {
            return $"Ditt totala belopp (inklusive lånade pengar): {total} SEK\n" +
                $"Maximal summan du kan låna: {maxLoan} SEK\n" +
                $"Är du säker på att du vill skapa lån? y/n";
        }

        internal decimal GetTotalLoan()
        {
            return Math.Round(Amount + InterestGained, 2);
        }

        internal decimal GetLoanWithoutInterest()
        {
            return Amount;
        }

        public override string ToString()
        {
            return $"Lån: {Amount * Interest} SEK";
        }

        internal void ReduceLoan(decimal payment)
        {
            if (payment <= 0)
            {
                return;
            }

            if(InterestGained > 0)
            {
                if(payment >= InterestGained)
                {
                    payment -= InterestGained;
                    InterestGained = 0;
                }
                else
                {
                    InterestGained -= payment;
                    return;
                }
            }

            Amount -= payment;

            if (Amount <= 0)
            {
                Amount = 0;
            }
        }

    }
}
