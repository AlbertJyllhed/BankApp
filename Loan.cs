using BankApp.Users;
namespace BankApp
{
    internal class Loan
    {
        internal decimal Amount { get; set; }
        internal decimal Interest { get; set; } 

        internal Loan(decimal amount, decimal interest = 0.0284m)
        {
            Amount = amount;
            Interest = interest;
        }
        internal void PrintLoanInterest()
        {
            Console.WriteLine($"Requested loan: {Amount}\n" +
                $"Loan interest: {Interest}\n" +
                $"Total amount payback {Amount * Interest}");
        }
        private decimal GetMaxLoan()
        {
            decimal maxLoan = 0;
            foreach (var account in User.BankAccounts)
            {
                maxLoan += account.GetBalance();
            }
            return maxLoan * 5;
        }
    }
}
