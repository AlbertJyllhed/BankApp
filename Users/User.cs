using BankApp.BankAccounts;

namespace BankApp.Users
{
    internal class User
    {
        internal string Name { get; set; }
        internal string Password { get; set; }
        internal List<BankAccount> BankAccounts { get; set; } = [];
        internal List<Loan> Loans { get; set; }


        internal User(string name, string password)
        {
            Name = name;
            Password = password;
            BankAccounts = new List<BankAccount>();
            Loans = new List<Loan>();
        }

        internal virtual void CreateBankAccount(string countyCode)
        {   
            //Ask user to choose the name of the created account.
            Console.WriteLine("Account name:");
            var accountName = Input.GetString();

            var currency = Data.GetCurrency();

            //Add the new bank account into the list.
            var bankAccount = new BankAccount(accountName, currency);
            BankAccounts.Add(bankAccount);
        }

        internal BankAccount? GetAccount(int id)
        {
            //Check every ID in each BankAccounts
            foreach (var account in BankAccounts)
            {
                if (id == account.ID)
                {
                    return account;
                }
            }
            return null;
        }

        internal void PrintBankAccounts()
        {
            int index = 1;
            if (BankAccounts.Count > 0)
            {
                Console.WriteLine("Your bank accounts:");
                foreach (var account in BankAccounts)
                {
                    Console.Write(index);
                    account.PrintInfo();
                    index++;
                }
            }
            else
            {
                Console.WriteLine("You don't have any accounts.");
            }
        }

        internal void CreateLoan(int id)
        {
            //PrintBankAccount to show all user's accounts and amount of money. 
            Console.WriteLine("The maximum amount of money you can borrow:");
            decimal maxLoan = 0;
            foreach (var account in BankAccounts)
            {
                maxLoan += account.GetBalance();
            }
            maxLoan *= 5;

            Console.WriteLine("How much would you like to borrow?");
            var borrowedAmount = Input.GetInt();
            while (borrowedAmount > maxLoan || borrowedAmount <= 0)
            {
                Console.WriteLine("Input exceeded maximum allowed loan");
                borrowedAmount = Input.GetInt();
            }

            Console.WriteLine("Which bank account would put your borrowed money in?");
            PrintBankAccounts();
            var chosenAccount = Input.GetIndex(BankAccounts.Count);

            var newLoan = new Loan(borrowedAmount);
            Loans.Add(newLoan);

            BankAccounts[chosenAccount].AddBalance(borrowedAmount);
        }

        internal void PrintLoans()
        {
            
            if (Loans.Count > 0)
            {
                decimal totalLoan = 0;

                //Shows current debt
                foreach (var loan in Loans)
                {
                    totalLoan += loan.GetTotalLoan();
                }

                Console.Write($"Your current debt: {totalLoan}");
            }
            else
            {
                Console.WriteLine("You have no loans.");
            }
        }


    }
}
