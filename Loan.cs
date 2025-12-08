namespace BankApp
{
    internal class Loan
    {
        private decimal Amount { get; set; }
        private decimal Interest { get; set; }
        private decimal _interestGained;

        internal Loan(decimal amount, decimal interest = 1.0284m)
        {
            Amount = amount;
            Interest = interest;
            _interestGained = amount * (interest - 1);
        }

        internal static string GetLoanInfo(decimal total, decimal maxLoan)
        {
            return $"Ditt totala belopp (inklusive lånade pengar): {total} SEK\n" +
                $"Maximal summan du kan låna: {maxLoan} SEK\n" +
                $"Är du säker på att du vill skapa lån? y/n";
        }

        internal decimal GetTotalLoan()
        {
            return Amount + _interestGained;
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

            if(_interestGained > 0)
            {
                if(payment >= _interestGained)
                {
                    payment -= _interestGained;
                    _interestGained = 0;
                }
                else
                {
                    _interestGained -= payment;
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
