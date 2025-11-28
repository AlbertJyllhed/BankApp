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
                PrintUtilities.PrintError("Överskridit antal försökt, ditt konto är låst!");
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
                PrintUtilities.PrintColoredMessage($"Fel användarnamn eller lösenord\n" +
                        $"Försök kvar: {3 - loginAttempts}", ConsoleColor.Yellow);

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
            PrintUtilities.PrintInputPrompt("Bankkonto namn: ");
            var accountName = InputUtilities.GetString();

            var currency = Data.ChooseCurrency().Key;

            var bankAccount = CreateBankAccount(accountName, currency);

            PrintUtilities.PrintMessage($"Ditt nya {bankAccount.GetAccountType()} ({accountName}, {currency}) " +
                $"har skapats!");
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
                PrintUtilities.PrintError("Du har inget bankkonto.");
                return;
            }

            // Choose from which account to transfer
            PrintUtilities.PrintMessage("Från vilket konto vill du överföra pengarna?");
            PrintBankAccounts();
            BankAccount fromAccount = GetAccountByIndex();

            BankAccount? toAccount;

            // Choose to which account to transfer (using index or ID)
            if (ChooseTransferMethod())
            {
                PrintUtilities.PrintMessage("Vilket bankkonto vill du överföra pengarna till? Ange index.");
                toAccount = GetAccountByIndex();
            }
            else
            {
                PrintUtilities.PrintMessage("Vilket bankkonto vill du överföra pengarna till? Ange kontonummer");
                toAccount = GetAccountByID();
            }

            if (toAccount == null)
            {
                PrintUtilities.PrintError("Inget konto hittades, försök igen.");
                return;
            }

            // Choose amount to transfer
            PrintUtilities.PrintMessage("Hur mycket pengar vill du överföra?");
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
                PrintUtilities.PrintError("Överföring misslyckades på grund av för låg summa.");
            }
        }

        // Method to choose transfer method
        private bool ChooseTransferMethod()
        {
            PrintUtilities.PrintMessage("Hur vill du överföra pengarna?\n" +
                "1. Med index\n" +
                "2. Med kontonummer");
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
            decimal amountCurrentCurrency = Data.ToSEK(amount, fromAccount.Currency);
            decimal convertedAmount = Data.FromSEK(amountCurrentCurrency, toAccount.Currency);

            convertedAmount = Math.Round(convertedAmount, 2);

            // Perform the transfer
            fromAccount.RemoveBalance(amount, toAccount.Name);
            toAccount.AddBalance(convertedAmount, fromAccount.Name);
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
                PrintUtilities.PrintError("Du har inget bankkonto.");
                return;
            }

            PrintUtilities.PrintMessage("Dina bankkonto:");
            PrintUtilities.PrintList(BankAccounts, true);
        }

        internal void PrintTransactionsActivity()
        {
            PrintUtilities.PrintMessage("--- Dina överföringar ---");

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
                PrintUtilities.PrintError("Du har inget bankkonto, vänligen skapa ett innan du gör lån.");
                return;
            }

            //PrintBankAccount to show all user's accounts and amount of money. 
            decimal totalInSEK = 0;
            foreach (var account in BankAccounts)
            {
                decimal balance = account.GetBalance();
                decimal balanceInSEK = Data.ToSEK(balance, account.Currency);
                totalInSEK += balanceInSEK;
            }

            //Own money
            decimal ownMoney = totalInSEK - GetTotalLoanForEachAccount();

            //Maximum of what user can loan
            decimal maxLoan = ownMoney * 5;

            //What custumer can borrow apart from already borrowed money
            maxLoan -= GetTotalLoanForEachAccount();

            // Check if the requested loan amount is valid, stop method if not.
            if (maxLoan <= 0)
            {
                PrintUtilities.PrintError("Du har inga pengar och kan därför inte skapa lån.");
                return;
            }

            PrintUtilities.PrintMessage($"Ditt totala belopp: {totalInSEK} SEK\n" +
                $"Maximal summan du kan låna: {maxLoan} SEK\n" +
                $"Är du säker på att du vill skapa lån? y/n");


            // Confirm loan creation, stop method if user inputs no.
            if (!InputUtilities.GetYesOrNo())
            {
                PrintUtilities.PrintColoredMessage("Lånet har blivit avbrutet.", ConsoleColor.Yellow);
                return;
            }

            PrintUtilities.PrintMessage("Hur mycket vill du låna?");
            var borrowedAmountSEK = InputUtilities.GetPositiveDecimal();

            while (borrowedAmountSEK > maxLoan || borrowedAmountSEK <= 0)
            {
                PrintUtilities.PrintColoredMessage($"Du kan ej låna {borrowedAmountSEK}", ConsoleColor.Yellow);
                borrowedAmountSEK = InputUtilities.GetInt();
            }

            PrintUtilities.PrintMessage("Vilket konto vill du låna till? ");
            PrintBankAccounts();
            var chosenAccount = InputUtilities.GetIndex(BankAccounts.Count);

            Loan? newLoan = new Loan(borrowedAmountSEK);
            Loans.Add(newLoan);

            decimal depositedAmount = Data.FromSEK(borrowedAmountSEK, BankAccounts[chosenAccount].Currency);

            BankAccounts[chosenAccount].AddBalance(depositedAmount);
            BankAccounts[chosenAccount].PrintDepositDetails(borrowedAmountSEK);
        }

        internal void PrintLoans()
        {
            if (Loans.Count == 0)
            {
                PrintUtilities.PrintError("Du har inga lån.");
                return;
            }
            PrintUtilities.PrintMessage("--- Dina lån ---", 1);

            PrintUtilities.PrintList(Loans, true);

            PrintUtilities.PrintMessage($"Din totala skuld inklusive ränta: {GetTotalLoanForEachAccount()} SEK");
        }

        internal decimal GetTotalLoanForEachAccount()
        {
            decimal sum = 0;
            foreach (var loan in Loans)
            {
                sum += loan.GetLoanWithoutInterest();
            }

            return sum;
        }

        //Savings account creation method
        internal void CreateSavingAccount()
        {
            //Ask user to choose the name of the created account.
            PrintUtilities.PrintInputPrompt("Sparkonto namn: ");
            var accountName = InputUtilities.GetString();

            PrintUtilities.PrintMessage("Hur mycket vill du sätta in på sparkontot?");
            var amount = InputUtilities.GetPositiveDecimal();

            var currency = Data.ChooseCurrency().Key;

            //Add the new bank account into the list.
            var savingsAccount = new SavingsAccount(accountName, currency);

            PrintUtilities.PrintMessage(savingsAccount.GetInterestInfo(amount));

            savingsAccount.AddBalance(amount);
            savingsAccount.PrintDepositDetails(amount);

            BankAccounts.Add(savingsAccount);
        }

        // Method to insert money into a bank account
        internal void InsertMoney()
        {
            // Choose which account to insert money into
            PrintUtilities.PrintMessage("Vilket konto vill du sätta in pengar på?");
            PrintBankAccounts();
            int index = InputUtilities.GetIndex(BankAccounts.Count);
            BankAccount insertMoneyAccount = BankAccounts[index];

            // Choose amount to insert
            PrintUtilities.PrintMessage($"Hur mycket pengar vill du sätta in till {insertMoneyAccount.Name}?");
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
