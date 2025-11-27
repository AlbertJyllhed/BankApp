using BankApp.BankAccounts;

namespace BankApp.Users
{
    internal class Customer : User
    {
        private int loginAttempts = 0;
        private List<BankAccount> BankAccounts { get; set; }
        private List<Loan> Loans { get; set; }
        internal bool Locked { get; private set; } = false;

        internal Customer(string name, string password) : base(name, password)
        {
            BankAccounts = new List<BankAccount>();
            Loans = new List<Loan>();
        }

        // Override TryLogin method for Customer
        internal override bool TryLogin(string password)
        {
            if (Locked)
            {
                // Check if account is locked and block login if so
                PrintUtilities.PrintError("Your account is locked due to too many failed login attempts.");
                return false;
            }

            if (Password == password)
            {
                // Reset login attempts on successful login
                ResetLoginAttempts();
                return true;
            }
            else
            {
                // Increment login attempts on failed login
                loginAttempts++;

                // Lock account if maximum attempts reached
                if (loginAttempts >= 3)
                {
                    Locked = true;
                }
                PrintUtilities.PrintColoredMessage($"Wrong username or password\n" +
                        $"Attempts left {3 - loginAttempts}", ConsoleColor.Yellow);

                return false;
            }
        }

        private void ResetLoginAttempts()
        {
            loginAttempts = 0;
        }

        internal void UnlockAccount()
        {
            Locked = false;
        }

        // Method to get user input and create a new bank account
        internal void SetupBankAccount()
        {
            // Ask user to choose the name of the created account.
            PrintUtilities.PrintInputPrompt("New bank account name: ");
            var accountName = InputUtilities.GetString();

            var currency = Data.ChooseCurrency().Key;

            var bankAccount = CreateBankAccount(accountName, currency);

            PrintUtilities.PrintMessage($"Your new {bankAccount.GetAccountType()} ({accountName}, {currency}) " +
                $"has been successfully created!");
        }

        // Method to create a new bank account without user input
        internal BankAccount CreateBankAccount(string accountName, string currency, string id = "")
        {
            var bankAccount = new BankAccount(accountName, currency, id);
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
            if (!HasBankAccounts())
            {
                PrintUtilities.PrintError("You don't have any bank accounts.");
                return;
            }

            // Choose from which account to transfer
            PrintUtilities.PrintMessage("From which account do you want to transfer?");
            PrintBankAccounts();
            BankAccount fromAccount = GetAccountByIndex();

            BankAccount? toAccount;

            // Choose to which account to transfer (using index or ID)
            if (ChooseTransferMethod())
            {
                PrintUtilities.PrintMessage("Which account do you want to transfer to? Enter account index.");
                toAccount = GetAccountByIndex();
            }
            else
            {
                PrintUtilities.PrintMessage("Which account do you want to transfer to? Enter account number.");
                toAccount = GetAccountByID();
            }

            if (toAccount == null)
            {
                PrintUtilities.PrintError("Account not found, please try again.");
                return;
            }

            // Choose amount to transfer
            PrintUtilities.PrintMessage("How much money do you want to transfer?");
            decimal amount = InputUtilities.GetPositiveDecimal();

            if (amount <= 0)
            {
                PrintUtilities.PrintError("Felaktig summa försök igen.");
                return;
            }

            // Check if there are sufficient funds and perform the transfer
            if (CanTransfer(fromAccount, amount))
            {
                DepositToAccount(amount, toAccount, fromAccount);
            }
            else
            {
                PrintUtilities.PrintError("Transfer failed due to insufficient funds.");
            }
        }

        // Method to choose transfer method
        private bool ChooseTransferMethod()
        {
            PrintUtilities.PrintMessage("How do you want to transfer your money?\n" +
                "1. By index\n" +
                "2. By account ID");
            return InputUtilities.GetIndex(2) == 0;
        }

        // Method to find bank account by index
        private BankAccount GetAccountByIndex()
        {
            int index = InputUtilities.GetIndex(BankAccounts.Count);
            return BankAccounts[index];
        }

        // Method to choose which account to transfer to by ID
        private BankAccount? GetAccountByID()
        {
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

        // Check if the user has any bank accounts
        private bool HasBankAccounts()
        {
            return BankAccounts.Count > 0;
        }

        // Check if there are sufficient funds to transfer
        private bool CanTransfer(BankAccount fromAccount, decimal amount)
        {
            return fromAccount.GetBalance() >= amount;
        }

        internal void PrintBankAccounts()
        {
            if (!HasBankAccounts())
            {
                PrintUtilities.PrintError("You don't have any bank accounts.");
                return;
            }

            PrintUtilities.PrintMessage("Your bank accounts:");
            PrintUtilities.PrintList(BankAccounts, true);
        }

        internal void PrintTransactionsActivity()
        {
            PrintUtilities.PrintMessage("--- Your Transactions ---");

            foreach (var account in BankAccounts)
            {
                account.PrintTransactions();
            }
        }

        // Loan creation method
        //TODO: Refactor method to smaller methods
        internal void CreateLoan()
        {
            //Check if user has any bank accounts, stop method if not.
            if (BankAccounts.Count == 0)
            {
                PrintUtilities.PrintError("You don´t have any accounts. Please make one before you make a loan.");
                return;
            }

            //PrintBankAccount to show all user's accounts and amount of money. 
            decimal totalInSEK = 0;
            foreach (var account in BankAccounts)
            {
                decimal balance = account.GetBalance();
                decimal balanceInSEK = account.ToSEK(balance);
                totalInSEK += balanceInSEK;
            }

            //Calculate max loan, 5 times the total balance in SEK)
            decimal maxLoan = totalInSEK;
            foreach (var loan in Loans)
            {
                maxLoan -= loan.GetTotalLoan();
            }
            maxLoan *= 5;

            PrintUtilities.PrintMessage($"Your total balance in SEK: {totalInSEK}\n" +
                $"The maximum amount of money you can borrow: {maxLoan}\n" +
                $"Are you sure you want to make a loan? y/n");

            // Confirm loan creation, stop method if user inputs no.
            if (!InputUtilities.GetYesOrNo())
            {
                PrintUtilities.PrintColoredMessage("Loan has been cancelled.", ConsoleColor.Yellow);
                return;
            }

            PrintUtilities.PrintMessage("How much would you like to borrow?");
            var borrowedAmountSEK = InputUtilities.GetPositiveDecimal();

            // Check if the requested loan amount is valid, stop method if not.
            if (maxLoan <= 0)
            {
                PrintUtilities.PrintError("You have no money, you are not able to borrow.");
                return;
            }

            while (borrowedAmountSEK > maxLoan || borrowedAmountSEK <= 0)
            {
                PrintUtilities.PrintColoredMessage($"You're not allowed to borrow {borrowedAmountSEK}", ConsoleColor.Yellow);
                borrowedAmountSEK = InputUtilities.GetInt();
            }

            PrintUtilities.PrintMessage("Which bank account would you like to put your borrowed money in?");
            PrintBankAccounts();
            var chosenAccount = InputUtilities.GetIndex(BankAccounts.Count);

            var newLoan = new Loan(borrowedAmountSEK);
            Loans.Add(newLoan);

            decimal depositedAmount = BankAccounts[chosenAccount].FromSEK(borrowedAmountSEK);

            BankAccounts[chosenAccount].AddBalance(depositedAmount);
            BankAccounts[chosenAccount].PrintDepositDetails(borrowedAmountSEK);

            PrintUtilities.PrintMessage($"Loan of {borrowedAmountSEK} SEK added to account #{chosenAccount}.");
        }

        internal void PrintLoans()
        {
            if (Loans.Count == 0)
            {
                PrintUtilities.PrintError("You have no loans.");
                return;
            }

            decimal totalLoan = 0;

            //Shows current debt
            foreach (var loan in Loans)
            {
                totalLoan += loan.GetTotalLoan();
            }

            PrintUtilities.PrintMessage($"Your current debt: {totalLoan} SEK");
        }

        //Savings account creation method
        internal void CreateSavingAccount()
        {
            //Ask user to choose the name of the created account.
            PrintUtilities.PrintInputPrompt("Savings account name: ");
            var accountName = InputUtilities.GetString();

            PrintUtilities.PrintMessage("What amount do you want put in the savings account?");
            var amount = InputUtilities.GetPositiveDecimal();

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
            PrintUtilities.PrintMessage("Which account do you want to insert money in to.");
            PrintBankAccounts();
            int index = InputUtilities.GetIndex(BankAccounts.Count);
            BankAccount insertMoneyAccount = BankAccounts[index];

            // Choose amount to insert
            PrintUtilities.PrintMessage($"How much money do you want to insert to {insertMoneyAccount.Name}?");
            decimal amount = InputUtilities.GetPositiveDecimal();

            if (amount <= 0)
            {
                PrintUtilities.PrintError("Felaktigt val, kan inte sätta in negativt värde.");
                return;
            }

            // Insert the money
            insertMoneyAccount.AddBalance(amount);
            insertMoneyAccount.PrintDepositDetails(amount);
        }
    }
}
