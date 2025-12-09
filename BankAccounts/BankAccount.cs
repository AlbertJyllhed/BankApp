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
        internal decimal Balance { get; private set; } = 0;

        // Constructor
        internal BankAccount(string name, string currency, string id = "")
        {
            Name = name;
            Currency = currency;
            ID = id;
        }

        // Method to add balance to account
        internal virtual void AddBalance(decimal value, string account = "")
        {
            CreateTransaction(value, string.Empty, account, "från");
            Balance += value;
        }

        // Method to get all transactions
        internal List<Transaction> GetTransactions()
        {
            return _transactions;
        }

        // Method to get the latest transaction
        internal Transaction GetLatestTransaction()
        {
            return _transactions.Last();
        }

        // Method to remove balance from account
        internal bool RemoveBalance(decimal value, string fromAccountID = "", string toAccountID = "")
        {
            if (Balance >= value)
            {
                CreateTransaction(-value, fromAccountID, toAccountID, "till");
                Balance -= value;
                return true;
            }
            else
            {
                UI.PrintError("Du har inte tillräckligt med pengar på kontot.\n" +
                    "Du är fattig :)");
                return false;
            }
        }

        // Method to create a transaction and add it to the transaction list
        private void CreateTransaction(decimal value, string fromAccountID, string toAccountID, string transferType)
        {
            // If account name is provided, include it in the message
            string message = (toAccountID != "") ? $"{transferType} {toAccountID}" : "";

            // Create new transaction and add to list
            var transaction = new Transaction(value, Currency, message, fromAccountID, toAccountID);
            _transactions.Add(transaction);
        }

        //Method to print all transactions
        internal void PrintTransactions()
        {
            UI.PrintMessage($"--- Transaktioner {Name} [{ID}] ---");
            UI.PrintList(_transactions);
        }

        internal string GetAccountType()
        {
            return GetType().Name;
        }

        // Override ToString method to print account details
        public override string ToString()
        {
            return $"{GetAccountType()} {Name} [{ID}]\n" +
                $"Saldo: {Math.Round(Balance, 2)} {Currency}";
        }
    }
}
