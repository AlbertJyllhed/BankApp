using BankApp.BankAccounts;
using BankApp.Services;

namespace BankApp.Users
{
    internal class Customer : User
    {
        private int loginAttempts = 0;
        private List<BankAccount> BankAccounts { get; set; }
        private List<Loan> Loans { get; set; }
        internal bool Locked { get; set; } = false;

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
                loginAttempts = 0;
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

        // Method to get user input and create a new bank account
        internal void SetupBankAccount()
        {
            UI.PrintMessage("Vad för typ av bankkonto vill du skapa?\n" +
                "1. Vanligt bankkonto\n" +
                "2. Sparkonto");

            int choice = InputUtilities.GetIndex(2);

            // Ask user to choose the name of the created account.
            UI.PrintInputPrompt("Bankkonto namn: ");
            var accountName = InputUtilities.GetString();

            // Ask user to choose the currency of the created account.
            var currency = Data.ChooseCurrency().Key;

            // Create the bank account based on user choice
            BankAccount account;
            if (choice == 0)
            {
                account = AccountService.CreateBankAccount(accountName, currency);
            }
            else
            {
                // Ask user for initial deposit amount for savings account
                UI.PrintMessage("Hur mycket vill du sätta in på sparkontot?");
                var amount = InputUtilities.GetPositiveDecimal();
                account = AccountService.CreateSavingsAccount(accountName, amount, currency);
            }

            // Add the new bank account into the list.
            AddBankAccount(account);

            UI.PrintMessage($"Ditt nya {account.GetAccountType()} " +
                $"({accountName}, {currency}) har skapats!");
        }

        // Method to add an existing bank account to the user's list
        internal void AddBankAccount(BankAccount bankAccount)
        {
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

        internal List<BankAccount> GetBankAccounts()
        {
            if (!HasBankAccounts())
            {
                UI.PrintError("Du har inget bankkonto.");
            }
            return BankAccounts;
        }

        internal List<Loan> GetLoans()
        {
            return Loans;
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
            UI.PrintList(GetBankAccounts(), true);
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

            // Check if there are sufficient funds and perform the transfer
            if (CanTransfer(fromAccount, amount))
            {
                AccountService.Transfer(amount, toAccount, fromAccount);
                UI.PrintColoredMessage($"" +
                    $"Överföring påbörjad: {amount} {fromAccount.Currency}\n" +
                    $"Från: Konto [{fromAccount.ID}]\n" +
                    $"Till: [{toAccount.ID}]\n" +
                    $"Pengar kommer fram klockan {DateTime.Now.AddMinutes(15):HH:mm}",
                    ConsoleColor.DarkCyan);
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

        // Method to print all transactions from all bank accounts
        internal void PrintTransactionsActivity()
        {
            UI.PrintMessage("--- Dina överföringar ---");

            foreach (var account in BankAccounts)
            {
                account.PrintTransactions();
            }
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


            UI.PrintMessage($"Din totala skuld inklusive ränta: {GetTotalLoanWithInterest()} SEK");

        }
        internal decimal GetTotalLoanWithInterest()
        {
            decimal sum = 0;
            foreach (var loan in Loans)
            {
                sum += loan.GetTotalLoan();
            }
            return sum;
        }

        internal decimal GetTotalLoanWithoutInterest()
        {
            decimal sum = 0;
            foreach (var loan in Loans)
            {
                sum += loan.GetLoanWithoutInterest();
            }
            return sum;
        }

        // Method to insert money into a bank account
        internal void InsertMoney()
        {
            // Choose which account to insert money into
            UI.PrintMessage("Vilket konto vill du sätta in pengar på?");
            UI.PrintList(GetBankAccounts(), true);
            int index = InputUtilities.GetIndex(BankAccounts.Count);
            BankAccount insertMoneyAccount = BankAccounts[index];

            // Choose amount to insert
            UI.PrintMessage($"Hur mycket pengar vill du sätta in till {insertMoneyAccount.Name}?");
            decimal amount = InputUtilities.GetPositiveDecimal();

            // Insert the money
            insertMoneyAccount.AddBalance(amount);
            UI.PrintMessage(insertMoneyAccount.GetLatestTransactionInfo());
        }

        internal void AddLoan(Loan newLoan)
        {
            Loans.Add(newLoan);
        }
    }
}
