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
            value = Math.Round(value, 2);
            CreateTransaction(value, fromAccount, "från");
            Balance += value;
        }
        
        // Print deposit details
        internal void PrintDepositDetails(decimal value)
        {
            PrintUtilities.PrintMessage($"{value} {Currency} var överfört till konto {ID}.");
        }

        // Print transfer details with to account IDs
        internal void PrintTransferDetails(decimal originalAmount, decimal convertedAmount, BankAccount toAccount)
        {
            if (Currency == toAccount.Currency)
            {
                PrintUtilities.PrintMessage($"{originalAmount} {Currency} var överfört från konto" +
                    $" {ID} till konto {toAccount.ID}.");
            }
            else
            {
                PrintUtilities.PrintMessage($"{originalAmount} {Currency} var överfört från konto {ID}," +
                    $" växlat till {convertedAmount} {toAccount.Currency} och överfört till konto {toAccount.ID}.");
            }
        }

        // Method to remove balance from account and return the removed value
        internal decimal RemoveBalance(decimal value, string toAccount = "")
        {
            value = Math.Round(value, 2);

            if (Balance >= value)
            {
                CreateTransaction(-value, toAccount, "till");
                Balance -= value;
                return value;
            }
            else
            {
                PrintUtilities.PrintError("Felaktigt begär, inte tillräckligt med pengar på kontot.\n" +
                    "Du är fattig :)");
                return 0;
            }
        }

        // Method to create a transaction and add it to the transaction list
        private void CreateTransaction(decimal value, string accountName, string transferType)
        {
            string transfer = $"{value} {Currency}";

            // If account name is provided, include it in the message
            string message = (accountName != "") ? $"{transfer} {transferType} {accountName}" : transfer;

            // Create new transaction and add to list
            var transaction = new Transaction(value, message);
            _transactions.Add(transaction);
        }

        internal decimal GetBalance()
        {
            return Balance;
        }

        //Method to print all transactions
        internal void PrintTransactions()
        {
            PrintUtilities.PrintMessage($"--- Transaktioner {Name} [{ID}] ---");
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
                $"Balans: {Balance} {Currency}";
        }
    }
}
