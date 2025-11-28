using BankApp.Users;
using BankApp.BankAccounts;
namespace BankApp
{
    internal class Loan
    {
        private decimal Amount { get; set; }
        private decimal Interest { get; set; }

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
            return Math.Round(Amount, 2);
        }

        public override string ToString()
        {
            return $"Lån: {Amount * Interest} SEK";
        }
    }
}
