namespace BankApp.Users
{
    internal class User
    {
        internal string Name { get; set; }
        internal string Password { get; set; }
        internal List<BankAccount> BankAccounts { get; set; }
        internal List<Loan> Loans { get; set; }

        internal User(string name, string password)
        {
            Name = name;
            Password = password;
            BankAccounts = new List<BankAccount>();
            Loans = new List<Loan>();
        }

        internal void CreateBankAccount()
        {
             
        }

        internal int GetAccount(int id)
        {
            return 0;
        }

        internal void PrintBankAccounts()
        {
            if (BankAccounts.Count > 0)
            {
                Console.WriteLine("Your bank accounts:");
                foreach (var account in BankAccounts)
                {
                    account.PrintInfo();
                }
            }
            else
            {
                Console.WriteLine("You don't have any account.");
            }
        }

        internal void CreateLoan(int id)
        {

        }

        internal void PrintLoans()
        {
            if (Loans.Count > 0)
            {
                Console.WriteLine("Your loans:");
                foreach (var loan in Loans)
                {
                    Console.WriteLine(loan);
                }
            }
            else
            {
                Console.WriteLine("You have no loans.");
            }
        }
    }
}
