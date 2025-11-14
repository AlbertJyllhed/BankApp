using BankApp.BankAccounts;

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

        internal void CreateBankAccount(string countyCode)
        {   
            //Ask user to choose the name of the created account.
            Console.WriteLine("Account name:");
            var accountName = Input.GetString();
            //Console.WriteLine("Choose currency");
            //foreach (var currencyKey in Data.currency)
            //{
            //    Console.WriteLine($"Currency code: {currencyKey.Key}");
            //}
            //var chosenCurrency =  Console.ReadLine();
            Data.GetCurrency();

            //Add the new bank account into the list.
            var bankAccount = new BankAccount(accountName);
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
                Console.WriteLine("You don't have any accounts.");
            }
        }

        internal void CreateLoan(int id)
        {
            PrintBankAccounts();
            var newLoan = new Loan();
            Loans.Add(newLoan);
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
