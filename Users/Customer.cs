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
                UI.PrintError("Överskridit antal försökt, ditt konto är låst!");
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
                UI.PrintColoredMessage($"Fel användarnamn eller lösenord\n" +
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
            UI.PrintInputPrompt("Bankkonto namn: ");
            var accountName = InputUtilities.GetString();

            var currency = Data.ChooseCurrency().Key;

            var bankAccount = CreateBankAccount(accountName, currency);

            UI.PrintMessage($"Ditt nya {bankAccount.GetAccountType()} ({accountName}, {currency}) " +
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
                UI.PrintError("Du har inget bankkonto.");
                return;
            }

            // Choose from which account to transfer
            UI.PrintMessage("Från vilket konto vill du överföra pengarna?");
            PrintBankAccounts();
            BankAccount fromAccount = GetAccountByIndex();

            BankAccount? toAccount;

            // Choose to which account to transfer (using index or ID)
            if (ChooseTransferMethod())
            {
                UI.PrintMessage("Vilket bankkonto vill du överföra pengarna till? Ange index.");
                toAccount = GetAccountByIndex();
            }
            else
            {
                UI.PrintMessage("Vilket bankkonto vill du överföra pengarna till? Ange kontonummer");
                toAccount = GetAccountByID();
            }

            if (toAccount == null)
            {
                UI.PrintError("Inget konto hittades, försök igen.");
                return;
            }

            // Choose amount to transfer
            UI.PrintMessage("Hur mycket pengar vill du överföra?");
            decimal amount = InputUtilities.GetPositiveDecimal();

            if (amount <= 0)
            {
                UI.PrintError("Felaktig summa försök igen.");
                return;
            }

            // Check if there are sufficient funds and perform the transfer
            if (CanTransfer(fromAccount, amount))
            {
                DepositToAccount(amount, toAccount, fromAccount);
            }
            else
            {
                UI.PrintError("Överföring misslyckades på grund av för låg summa.");
            }
        }

        // Method to choose transfer method
        private bool ChooseTransferMethod()
        {
            UI.PrintMessage("Hur vill du överföra pengarna?\n" +
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
            fromAccount.PrintTransferDetails(convertedAmount, toAccount);
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
                UI.PrintError("Du har inget bankkonto.");
                return;
            }

            UI.PrintMessage("Dina bankkonto:");
            UI.PrintList(BankAccounts, true);
        }

        internal void PrintTransactionsActivity()
        {
            UI.PrintMessage("--- Dina överföringar ---");

            foreach (var account in BankAccounts)
            {
                account.PrintTransactions();
            }
        }

        // Loan creation method
        //TODO: Refactor method to smaller methods
        internal void LoanSetup()
        {
            //Check if user has any bank accounts, stop method if not.
            if (BankAccounts.Count == 0)
            {
                UI.PrintError("Du har inget bankkonto, vänligen skapa ett innan du gör lån.");
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
                UI.PrintError("Du har inga pengar och kan därför inte skapa lån.");
                return;
            }

            LoanMessage(totalInSEK, maxLoan);

            // Confirm loan creation, stop method if user inputs no.
            if (!InputUtilities.GetYesOrNo())
            {
                UI.PrintColoredMessage("Lånet har blivit avbrutet.", ConsoleColor.Yellow);
                return;
            }

            CreateLoan(maxLoan);
        }

        internal void CreateLoan(decimal maxLoan)
        {
            UI.PrintMessage("Hur mycket vill du låna?");
            var borrowedAmountSEK = InputUtilities.GetPositiveDecimal();

            while (borrowedAmountSEK > maxLoan || borrowedAmountSEK <= 0)
            {
                UI.PrintColoredMessage($"Du kan ej låna {borrowedAmountSEK}", ConsoleColor.Yellow);
                borrowedAmountSEK = InputUtilities.GetInt();
            }

            UI.PrintMessage("Vilket konto vill du låna till? ");
            PrintBankAccounts();
            var chosenIndex = InputUtilities.GetIndex(BankAccounts.Count);
            var account = BankAccounts[chosenIndex];

            Loan? newLoan = new Loan(borrowedAmountSEK);
            Loans.Add(newLoan);

            decimal depositedAmount = Data.FromSEK(borrowedAmountSEK, account.Currency);

            account.AddBalance(depositedAmount);
            UI.PrintMessage(account.GetLatestTransactionInfo());
        }

        internal void LoanMessage(decimal total, decimal loan)
        {
            UI.PrintMessage($"Ditt totala belopp: {total} SEK\n" +
                $"Maximal summan du kan låna: {loan} SEK\n" +
                $"Är du säker på att du vill skapa lån? y/n");
        }

        internal void PrintLoans()
        {
            if (Loans.Count == 0)
            {
                UI.PrintError("Du har inga lån.");
                return;
            }
            UI.PrintMessage("--- Dina lån ---", 1);

            UI.PrintList(Loans, true);

            UI.PrintMessage($"Din totala skuld inklusive ränta: {GetTotalLoanForEachAccount()} SEK");
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
            UI.PrintInputPrompt("Sparkonto namn: ");
            var accountName = InputUtilities.GetString();

            UI.PrintMessage("Hur mycket vill du sätta in på sparkontot?");
            var amount = InputUtilities.GetPositiveDecimal();

            var currency = Data.ChooseCurrency().Key;

            //Add the new bank account into the list.
            var savingsAccount = new SavingsAccount(accountName, currency);

            UI.PrintMessage(savingsAccount.GetInterestInfo(amount));

            savingsAccount.AddBalance(amount);
            UI.PrintMessage(savingsAccount.GetLatestTransactionInfo());

            BankAccounts.Add(savingsAccount);
        }

        // Method to insert money into a bank account
        internal void InsertMoney()
        {
            // Choose which account to insert money into
            UI.PrintMessage("Vilket konto vill du sätta in pengar på?");
            PrintBankAccounts();
            int index = InputUtilities.GetIndex(BankAccounts.Count);
            BankAccount insertMoneyAccount = BankAccounts[index];

            // Choose amount to insert
            UI.PrintMessage($"Hur mycket pengar vill du sätta in till {insertMoneyAccount.Name}?");
            decimal amount = InputUtilities.GetPositiveDecimal();

            // Insert the money
            insertMoneyAccount.AddBalance(amount);
            UI.PrintMessage(insertMoneyAccount.GetLatestTransactionInfo());
        }
    }
}
