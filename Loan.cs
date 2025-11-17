using BankApp.Users;
namespace BankApp
{
    internal class Loan
    {
        internal decimal Amount { get; set; }
        internal decimal Interest { get; set; }

        internal Loan(decimal amount, decimal interest = 1.0284m)
        {
            Amount = amount;
            Interest = interest;
        }
        internal void PrintLoanInterest()
        {
            Console.WriteLine($"Requested loan: {Amount}\n" +
                $"Loan interest: {Amount * Interest - Amount}\n" +
                $"Total amount payback {Amount * Interest}");
        }

        internal decimal GetTotalLoan()
        {
            return Amount * Interest;
        }
    }
}
