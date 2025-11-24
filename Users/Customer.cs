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

        // Override TryLogin method for Customer
        internal override bool TryLogin(string password)
        {
            if (locked)
            {
                // Check if account is locked and block login if so
                Console.WriteLine("Your account is locked due to too many failed login attempts.");
                return false;
            }

            if (Password == password)
            {
                // Reset login attempts on successful login
                UnlockAccount();
                return true;
            }
            else
            {
                // Increment login attempts on failed login
                loginAttempts++;

                // Lock account if maximum attempts reached
                if (loginAttempts >= 3)
                {
                    locked = true;
                }
                Console.WriteLine($"Wrong username or password\n" +
                        $"Attempts left {3 - loginAttempts}");

                return false;
            }
        }

        internal void UnlockAccount()
        {
            loginAttempts = 0;
            locked = false;
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
                BankAccount fromAccount = GetTransferAccountByIndex();

                BankAccount? toAccount;

                // Choose to which account to transfer (using index or ID)
                if (ChooseTransferMethod())
                {
                    toAccount = GetDepositAccountByIndex();
                }
                else
                {
                    toAccount = GetDepositAccountByID();
                }

                if (toAccount != null)
                {
                    // Choose amount to transfer
                    Console.WriteLine("How much money do you want to transfer?");
                    decimal amount = InputUtilities.GetDecimal();

                    // Check if there are sufficient funds and perform the transfer
                    if (CanTransfer(fromAccount, amount))
                    {
                        DepositToAccount(amount, toAccount, fromAccount);
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
        }

        // Method to choose transfer method
        private bool ChooseTransferMethod()
        {
            Console.WriteLine("How do you want to transfer your money?\n" +
                "1. By index\n" +
                "2. By account ID");
            return InputUtilities.GetIndex(2) == 0;
        }

        // Method to choose which account to transfer from
        private BankAccount GetTransferAccountByIndex()
        {
            Console.WriteLine("From which account do you want to transfer?");
            PrintBankAccounts();
            int index = InputUtilities.GetIndex(BankAccounts.Count);
            return BankAccounts[index];
        }

        // Method to choose which account to transfer to by index
        private BankAccount? GetDepositAccountByIndex()
        {
            Console.WriteLine("Which account do you want to transfer to? Enter account index.");
            int index = InputUtilities.GetIndex(BankAccounts.Count);
            return Data.GetBankAccount(BankAccounts[index].ID);
        }

        // Method to choose which account to transfer to by ID
        private BankAccount? GetDepositAccountByID()
        {
            Console.WriteLine("Which account do you want to transfer to? Enter account number.");
            string id = InputUtilities.GetString();
            return Data.GetBankAccount(id);
        }

        // Method to handle transfer to another account
        private void DepositToAccount(decimal amount, BankAccount toAccount, BankAccount fromAccount)
        {
            // Convert amount to SEK and then to the target account's currency
            decimal amountCurrentCurrency = fromAccount.ToSEK(amount);
            decimal convertedAmount = toAccount.FromSEK(amountCurrentCurrency);

            convertedAmount = Math.Round(convertedAmount, 2);

            // Perform the transfer
            fromAccount.RemoveBalance(amount);
            toAccount.AddBalance(convertedAmount);
            fromAccount.PrintTransferDetails(amount, convertedAmount, toAccount);
        }

        // Check if the user has any bank accounts and print a message if not
        private bool HasBankAccounts()
        {
            if (BankAccounts.Count > 0)
            {
                return true;
            }
            else
            {
                Console.WriteLine("You don't have any bank accounts.");
                return false;
            }
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
        }

        internal void PrintTransactionsActivity()
        {
            Console.WriteLine("--- Your Transactions ---\n");

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
                        BankAccounts[chosenAccount].PrintDepositDetails(borrowedAmountSEK);

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
            savingsAccount.PrintDepositDetails(amount);

            BankAccounts.Add(savingsAccount);
        }

        // Method to insert money into a bank account
        internal void InsertMoney()
        {
            // Choose which account to insert money into
            Console.WriteLine("Which account do you want to insert money in to.");
            PrintBankAccounts();
            int index = InputUtilities.GetIndex(BankAccounts.Count);
            BankAccount insertMoneyAccount = BankAccounts[index];

            // Choose amount to insert
            Console.WriteLine($"How much money do you want to insert to {insertMoneyAccount.Name}?");
            decimal amount = InputUtilities.GetDecimal();

            // Insert the money
            insertMoneyAccount.AddBalance(amount);
            insertMoneyAccount.PrintDepositDetails(amount);
        }
    }
}
