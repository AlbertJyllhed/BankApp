using BankApp.BankAccounts;
using BankApp.Services;

namespace BankApp.Users
{
    internal class Customer : User
    {
        private int _loginAttempts = 0;
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
                UI.PrintError("Överskridit antal försök, ditt konto är låst!");
                return false;
            }

            if (Password == password)
            {
                // Reset login attempts on successful login
                _loginAttempts = 0;
                return true;
            }
            else
            {
                // Increment login attempts on failed login
                _loginAttempts++;

                // Lock account if maximum attempts reached
                if (_loginAttempts >= 3)
                {
                    Locked = true;
                }
                UI.PrintColoredMessage($"Försök kvar: {3 - _loginAttempts}", ConsoleColor.Yellow);

                return false;
            }
        }

        // Method to add an existing bank account to the user's list
        internal void AddBankAccount(BankAccount bankAccount)
        {
            BankAccounts.Add(bankAccount);
        }

        // Method to get all bank accounts of the user
        internal List<BankAccount> GetBankAccounts()
        {
            if (!HasBankAccounts())
            {
                UI.PrintError("Inget bankkonto hittades.");
            }
            return BankAccounts;
        }

        // Check if the user has any bank accounts
        internal bool HasBankAccounts()
        {
            return BankAccounts.Count > 0;
        }

        // Method to find bank account by index
        internal BankAccount GetAccountByIndex()
        {
            int index = InputUtilities.GetIndex(BankAccounts.Count);
            return BankAccounts[index];
        }

        // Method to choose which account to transfer to by ID
        internal BankAccount? GetAccountByID()
        {
            string id = InputUtilities.GetString();
            return Data.GetBankAccount(id);
        }

        // Method to print all transactions from all bank accounts
        internal void PrintTransactions()
        {
            UI.PrintMessage("--- Mina överföringar ---\n");

            foreach (var account in BankAccounts)
            {
                account.PrintTransactions();
            }
        }

        // Method to add a new loan to the user's list
        internal void AddLoan(Loan newLoan)
        {
            Loans.Add(newLoan);
        }

        // Method to get all loans of the user
        internal List<Loan> GetLoans()
        {
            return Loans;
        }

        // Print all loans of the user
        internal void PrintLoans()
        {
            if (Loans.Count == 0)
            {
                UI.PrintError("Du har inga lån.");
                UI.PrintResetMessage();
                return;
            }

            UI.PrintMessage("--- Mina lån ---", 1);
            UI.PrintList(Loans, true);
            UI.PrintMessage($"Din totala skuld: {GetTotalLoanWithoutInterest()} SEK");
        }

        // Calculate total loan amount without interest
        internal decimal GetTotalLoanWithoutInterest()
        {
            decimal sum = 0;
            foreach (var loan in Loans)
            {
                sum += loan.GetLoanWithoutInterest();
            }
            return sum;
        }
    }
}
