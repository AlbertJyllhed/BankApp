using Newtonsoft.Json;

namespace BankApp.BankAccounts
{
    internal class BankAccount
    {
        private string _id = "";
        public List<Transaction> Transactions { get; } = [];
        public string ID
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
        public string Name { get; }
        public string Currency { get; set; } = "SEK";
        public decimal Balance { get; private set; } = 0;

        // Constructor
        [JsonConstructor]
        internal BankAccount(string name, decimal balance, string currency, string id = "")
        {
            Name = name;
            Balance = balance;
            Currency = currency;
            ID = id;
        }

        // Method to add balance to account
        internal virtual void AddBalance(decimal value, string account = "")
        {
            CreateTransaction(value, string.Empty, account, "från");
            Balance += value;
        }

        // Method to get all transactions which are not yet completed
        internal List<Transaction> GetPendingTransactions()
        {
            var pendingTransactions = new List<Transaction>();
            foreach (var transaction in Transactions)
            {
                if (!transaction.Completed)
                {
                    pendingTransactions.Add(transaction);
                }
            }
            return pendingTransactions;
        }

        // Method to get the latest transaction
        internal Transaction GetLatestTransaction()
        {
            return Transactions.Last();
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
            Transactions.Add(transaction);
        }

        //Method to print all transactions
        internal void PrintTransactions()
        {
            UI.PrintMessage($"--- Transaktioner {Name} [{ID}] ---");
            UI.PrintList(Transactions);
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
