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
        internal virtual void AddBalance(decimal value, string fromAccount = "")
        {
            CreateTransaction(value, fromAccount, "från");
            Balance += value;
        }

        // Print deposit details
        internal string GetLatestTransactionInfo()
        {
            var lastTransaction = _transactions.Last();
            return $"{lastTransaction.Amount} {Currency} överfördes till konto {ID}.";
        }

        // Print transfer details with to account IDs
        internal void PrintTransferDetails(decimal convertedAmount, BankAccount toAccount)
        {
            var lastTransaction = _transactions.Last();
            decimal positiveAmount = Math.Abs(lastTransaction.Amount);
            if (Currency == toAccount.Currency)
            {
                UI.PrintMessage($"{positiveAmount} {Currency} överfördes från konto" +
                    $" {ID} till konto {toAccount.ID}.");
            }
            else
            {
                UI.PrintMessage($"{positiveAmount} {Currency} hämtades från konto {ID}, " +
                    $"växlades till {Math.Round(convertedAmount, 2)} {toAccount.Currency} och överfördes till konto {toAccount.ID}.");
            }
        }

        internal bool RemoveBalance(decimal value, string toAccount = "")
        {


            if (Balance >= value)
            {
                CreateTransaction(-value, toAccount, "till");
                Balance -= value;
                return true;
            }
            else
            {
                UI.PrintError("Felaktigt begär, inte tillräckligt med pengar på kontot.\n" +
                    "Du är fattig :)");
                return false;
            }
        }

        // Method to create a transaction and add it to the transaction list
        private void CreateTransaction(decimal value, string accountName, string transferType)
        {
            // If account name is provided, include it in the message
            string message = (accountName != "") ? $"{transferType} {accountName}" : "";

            // Create new transaction and add to list
            var transaction = new Transaction(value, Currency, message);
            _transactions.Add(transaction);
        }

        internal decimal GetBalance()
        {
            return Balance;
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
                $"Balans: {Balance} {Currency}";
        }
    }
}
