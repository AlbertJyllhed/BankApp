using BankApp.BankAccounts;

namespace BankApp.Users
{
    internal class Customer : User
    {
        private bool locked = false;
        private int loginAttempts = 0;
        private List<BankAccount> BankAccounts { get; set; }
        private List<Loan> Loans { get; set; }

        internal Customer(string name, string password) : base(name, password)
        {
            BankAccounts = new List<BankAccount>();
            Loans = new List<Loan>();
        }

        internal void TryLogin()
        {
            loginAttempts++;

            if (loginAttempts >= 3)
            {
                locked = true;
                loginAttempts = 0;
            }
        }

        internal bool IsLocked()
        {
            return locked;
        }

        // Method to get user input and create a new bank account
        internal void SetupBankAccount()
        {
            // Ask user to choose the name of the created account.
            Console.Write("New bank account name: ");
            var accountName = InputUtilities.GetString();

            var currency = Data.ChooseCurrency().Key;

            var bankAccount = CreateBankAccount(accountName, currency);

            Console.WriteLine($"Your new {bankAccount.GetAccountType()} ({accountName}, {currency}) " +
                $"has been successfully created!");
        }

        // Method to create a new bank account without user input
        internal BankAccount CreateBankAccount(string accountName, string currency)
        {
            var bankAccount = new BankAccount(accountName, currency);
            BankAccounts.Add(bankAccount);
            Data.AddBankAccount(bankAccount);
            return bankAccount;
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
            // Check if there are any bank accounts to transfer from
            if (HasBankAccounts())
            {
                // Choose from which account to transfer
                Console.WriteLine("From which account do you want to transfer?");
                PrintBankAccounts();
                int fromIndex = InputUtilities.GetIndex(BankAccounts.Count);
                BankAccount fromAccount = BankAccounts[fromIndex];

                // Choose to which account to transfer
                Console.WriteLine("Which account do you want to transfer to? Enter account number.");
                string id = InputUtilities.GetString();
                BankAccount? toAccount = Data.GetBankAccount(id);
                if (toAccount != null)
                {
                    // Choose amount to transfer
                    Console.WriteLine("How much money do you want to transfer?");
                    decimal amount = InputUtilities.GetDecimal();

                    // Check if there are sufficient funds and perform the transfer
                    if (CanTransfer(fromAccount, amount))
                    {
                        Console.WriteLine($"Transferred {amount} {fromAccount.Currency} to {toAccount.Name}.");
                        fromAccount.RemoveBalance(amount);
                        toAccount.AddBalance(amount);
                    }
                    else
                    {
                        Console.WriteLine("Transfer failed due to insufficient funds.");
                    }
                }
                else
                {
                    Console.WriteLine("Account not found, please try again.");
                }
            }
            else
            {
                Console.WriteLine("You don't have any accounts to transfer from.");
            }
        }

        private bool HasBankAccounts()
        {
            return BankAccounts.Count > 0;
        }

        private bool CanTransfer(BankAccount fromAccount, decimal amount)
        {
            return fromAccount.GetBalance() >= amount;
        }

        internal void PrintBankAccounts()
        {
            int index = 1;
            if (HasBankAccounts())
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

        internal void PrintTransactionsActivity()
        {
            foreach (var account in BankAccounts)
            {
                account.PrintTransactions();
            }
        }

        // Loan creation method
        internal void CreateLoan()
        {
            //Check if user has any bank accounts
            if (BankAccounts.Count > 0)
            {
                //PrintBankAccount to show all user's accounts and amount of money. 
                decimal totalInSEK = 0;
                foreach (var account in BankAccounts)
                {
                    decimal balance = account.GetBalance();
                    decimal balanceInSEK = account.ToSEK(balance);
                    totalInSEK += balanceInSEK;
                }

                //Calculate max loan, 5 times the total balance in SEK)
                decimal maxLoan = totalInSEK * 5;
                Console.WriteLine($"Your total balance in SEK: {totalInSEK}");
                Console.WriteLine($"The maximum amount of money you can borrow: {maxLoan}");
                Console.WriteLine("Are you sure you want to make a loan? y/n");

                bool confirmLoan = InputUtilities.GetYesOrNo();

                if (confirmLoan)
                {
                    Console.WriteLine("How much would you like to borrow?");
                    var borrowedAmountSEK = InputUtilities.GetInt();

                    if (maxLoan <= 0)
                    {
                        Console.WriteLine("You have no money, you are not able to borrow.");
                    }
                    else
                    {
                        while (borrowedAmountSEK > maxLoan || borrowedAmountSEK <= 0)
                        {
                            Console.WriteLine($"You're not allowed to borrow {borrowedAmountSEK}");
                            borrowedAmountSEK = InputUtilities.GetInt();
                        }

                        Console.WriteLine("Which bank account would you like to put your borrowed money in?");
                        PrintBankAccounts();
                        var chosenAccount = InputUtilities.GetIndex(BankAccounts.Count);

                        var newLoan = new Loan(borrowedAmountSEK);
                        Loans.Add(newLoan);

                        decimal depositedAmount = BankAccounts[chosenAccount].FromSEK(borrowedAmountSEK);

                        BankAccounts[chosenAccount].AddBalance(depositedAmount);
                        BankAccounts[chosenAccount].PrintTransferDetails(borrowedAmountSEK);

                        Console.WriteLine($"Loan of {borrowedAmountSEK} SEK added to account #{chosenAccount}.");
                    }
                }
                else
                {
                    Console.WriteLine("Loan has been cancelled.");
                }
            }
            else
            {
                Console.WriteLine("You don´t have any accounts. Please make one before you make a loan.");
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

                Console.Write($"Your current debt: {totalLoan} SEK\n");
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
            var accountName = InputUtilities.GetString();

            Console.WriteLine("What amount do you want put in the savings account?");
            var amount = InputUtilities.GetDecimal();

            var currency = Data.ChooseCurrency().Key;

            //Add the new bank account into the list.
            var savingsAccount = new SavingsAccount(accountName, currency);

            savingsAccount.PrintSavingsInterest(amount);

            savingsAccount.AddBalance(amount);
            savingsAccount.PrintTransferDetails(amount);

            BankAccounts.Add(savingsAccount);
        }

        internal void InsertMoney()
        {

            Console.WriteLine("Which account do you want to insert money in to.");
            PrintBankAccounts();
            int fromIndex = InputUtilities.GetIndex(BankAccounts.Count);
            BankAccount InsertMoneyToAccount = BankAccounts[fromIndex];

            Console.WriteLine("How much money do you want to insert to account?");
            decimal amount = InputUtilities.GetDecimal();

            Console.WriteLine("Transfer successful.");
            InsertMoneyToAccount.AddBalance(amount);
            InsertMoneyToAccount.PrintTransferDetails(amount);

        }
    }
}
