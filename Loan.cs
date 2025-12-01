using BankApp.Users;
using BankApp.BankAccounts;
namespace BankApp
{
    internal class Loan
    {
        private decimal Amount { get; set; }
        private decimal Interest { get; set; }
        private decimal IntrestGained;

        internal Loan(decimal amount, decimal interest = 1.0284m)
        {
            Amount = amount;
            Interest = interest;
        }

        internal string GetLoanInfo()
        {
            return $"Begärt lån: {Amount}\n" +
                $"Lånets ränta: {Amount * Interest - Amount}\n" +
                $"Total summa att betala tillbaka {Amount * Interest}";
        }

        internal decimal GetTotalLoan()
        {
            var totalAmount = Amount * Interest;
            return Math.Round(totalAmount, 2);
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

            if(IntrestGained > 0)
            {
                if(payment >= IntrestGained)
                {
                    payment -= IntrestGained;
                    IntrestGained = 0;
                }
                else
                {
                    IntrestGained -= payment;
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
