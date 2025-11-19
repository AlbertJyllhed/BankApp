using BankApp.BankAccounts;

namespace BankApp.Users
{
    internal class Customer : User
    {
        private List<BankAccount> BankAccounts { get; set; }
        private List<Loan> Loans { get; set; }

        internal Customer(string name, string password) : base(name, password)
        {
            BankAccounts = new List<BankAccount>();
            Loans = new List<Loan>();
        }

        //Add a method to ensure bank account is unique

        internal virtual void CreateBankAccount()
        {
            //Ask user to choose the name of the created account.
            Console.WriteLine("Account name:");
            var accountName = Input.GetString();

            var currency = Data.GetCurrency();

            //Add the new bank account into the list.
            var bankAccount = new BankAccount(accountName, currency);
            BankAccounts.Add(bankAccount);
        }

        internal BankAccount? GetBankAccount(string id)
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

        // Transfers balance from one account to another
        internal void TransferBalance()
        {
            // Choose from which account to transfer
            Console.WriteLine("From which account do you want to transfer?");
            PrintBankAccounts();
            int fromIndex = Input.GetIndex(BankAccounts.Count);
            BankAccount fromAccount = BankAccounts[fromIndex];

            // Choose to which account to transfer
            Console.WriteLine("Which account do you want to transfer to?");
            string id = Input.GetString();
            BankAccount? toAccount = Data.GetBankAccount(id);
            if (toAccount != null)
            {
                // Choose amount to transfer
                Console.WriteLine("How much money do you want to transfer?");
                decimal amount = Input.GetDecimal();

                // Check if there are sufficient funds and perform the transfer
                decimal result = fromAccount.RemoveBalance(amount);
                if (result == 0)
                {
                    Console.WriteLine("Transfer failed due to insufficient funds.");
                }
                else
                {
                    Console.WriteLine("Transfer successful.");
                    toAccount.AddBalance(amount);
                }
            }
            else
            {
                Console.WriteLine("Account not found, please try again.");
            }
        }

        internal void PrintBankAccounts()
        {
            int index = 1;
            if (BankAccounts.Count > 0)
            {
                Console.WriteLine("Your bank accounts:");
                foreach (var account in BankAccounts)
                {
                    Console.Write($"{index}. ");
                    account.PrintInfo();
                    index++;
                }
            }
            else
            {
                Console.WriteLine("You don't have any accounts.");
            }
        }

        internal void CreateLoan()
        {
            //PrintBankAccount to show all user's accounts and amount of money. 
            decimal maxLoan = 0;
            foreach (var account in BankAccounts)
            {
                maxLoan += account.GetBalance();
            }
            maxLoan *= 5;
            Console.WriteLine($"The maximum amount of money you can borrow: {maxLoan}");

            Console.WriteLine("How much would you like to borrow?");
            var borrowedAmount = Input.GetInt();

            if (maxLoan <= 0)
            {
                Console.WriteLine("You have no money, you are not able to borrow.");

            }
            else
            {
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

                Console.Write($"Your current debt: {totalLoan}\n");
            }
            else
            {
                Console.WriteLine("You have no loans.");
            }
        }

        //Savings account creation method
        internal void CreateSavingAccount()
        {
            //Ask user to choose the name of the created account.
            Console.WriteLine("Savings account name:");
            var accountName = Input.GetString();

            Console.WriteLine("What amount do you want put in the savings account?");
            var amount = Input.GetDecimal();

            var currency = Data.GetCurrency();

            //Add the new bank account into the list.
            var savingsAccount = new SavingsAccount(accountName, currency);

            savingsAccount.PrintSavingsInterest(amount);

            savingsAccount.AddBalance(amount);

            BankAccounts.Add(savingsAccount);
        }
    }
}
