namespace BankApp.BankAccounts
{
    internal class BankAccount
    {
        private string _id = "";
        private List<Transaction> _transactions = [];
        internal string ID
        {
            get { return _id; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    _id = Data.GetUniqueID();
                }
                else
                {
                    _id = value;
                }
            }
        }
        internal string Name { get; }
        internal string Currency { get; set; } = "SEK";
        protected decimal Balance { get; set; } = 0;

        // Constructor
        internal BankAccount(string name, string currency, string id = "")
        {
            Name = name;
            Currency = currency;
            ID = id;
        }

        // Method to add balance to account
        internal virtual void AddBalance(decimal value, BankAccount? account = null)
        {
            value = Math.Round(value, 2);
            CreateTransaction(value, account);
            Balance += value;
        }
        private void CreateTransaction(decimal value, BankAccount? account = null)
        {
            Transaction transaction;
            if (account == null)
            {
                transaction = new Transaction(value, $"Sätter in pengar på konto");
            }
            else
            {
                transaction = new Transaction(value, $"Flyttar pengar från konto {account.Name}");
            }

            _transactions.Add(transaction);
        }
        
        // Print deposit details
        internal void PrintDepositDetails(decimal value)
        {
            PrintUtilities.PrintMessage($"{value} {Currency} was transferred to account {ID}.");
        }

        // Print transfer details with to account IDs
        internal void PrintTransferDetails(decimal originalAmount, decimal convertedAmount, BankAccount toAccount)
        {
            if (Currency == toAccount.Currency)
            {
                PrintUtilities.PrintMessage($"{originalAmount} {Currency} was transferred from account" +
                    $" {ID} to account {toAccount.ID}.");
            }
            else
            {
                PrintUtilities.PrintMessage($"{originalAmount} {Currency} was transferred from account {ID}," +
                    $" converted into {convertedAmount} {toAccount.Currency} and transferred to account {toAccount.ID}.");
            }
        }

        // Method to remove balance from account and return the removed value
        internal decimal RemoveBalance(decimal value)
        {
            value = Math.Round(value, 2);

            if (Balance >= value)
            {
                Balance -= value;
                _transactions.Add(-value);
                return value;
            }
            else
            {
                PrintUtilities.PrintError("Invalid request, not enough money in account.\n" +
                    "You are poor.");
                return 0;
            }
        }

        internal decimal GetBalance()
        {
            return Balance;
        }

        //Method to print all transactions
        internal void PrintTransactions()
        {
            PrintUtilities.PrintMessage($"--- Transactions {Name} [{ID}] ---");
            PrintUtilities.PrintList(_transactions);
        }

        internal string GetAccountType()
        {
            return GetType().Name;
        }

        // Override ToString method to print account details
        public override string ToString()
        {
            return $"{GetAccountType()} {Name} [{ID}]\n" +
                $"Balance: {Balance} {Currency}";
        }
    }
}
