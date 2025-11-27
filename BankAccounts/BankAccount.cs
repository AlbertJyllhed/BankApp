namespace BankApp.BankAccounts
{
    internal class BankAccount
    {
        private List<decimal> _transactions = [];
        internal string ID { get; }
        internal string Name { get; }
        internal string Currency { get; set; } = "SEK";
        protected decimal Balance { get; set; } = 0;

        // Constructor
        internal BankAccount(string name, string currency)
        {
            Name = name;
            Currency = currency;
            ID = Data.GetUniqueID();
        }

        // Method to add balance to account
        internal virtual void AddBalance(decimal value)
        {
            value = Math.Round(value, 2);
            _transactions.Add(value);
            Balance += value;
            //Console.WriteLine($"{value} {Currency} was transfered to account {ID}.");
        }

        // Print deposit details
        internal void PrintDepositDetails(decimal value)
        {
            Console.WriteLine($"{value} {Currency} was transferred to account {ID}.");
        }

        // Print transfer details with to account IDs
        internal void PrintTransferDetails(decimal originalAmount, decimal convertedAmount, BankAccount toAccount)
        {
            if (Currency == toAccount.Currency)
            {
                Console.WriteLine($"{originalAmount} {Currency} was transferred from account" +
                    $" {ID} to account {toAccount.ID}.");
            }
            else
            {
                Console.WriteLine($"{originalAmount} {Currency} was transferred from account {ID}," +
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
            foreach (var transaction in _transactions)
            {
                PrintUtilities.PrintMessage($"* {transaction} {Currency}");
            }
            PrintUtilities.PrintEmptyLine();
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

        // Convert value to SEK from account currency
        internal decimal ToSEK(decimal value)
        {
            return Math.Round(value * Data.GetCurrency(Currency).Value, 2);
        }

        // Convert value from SEK to account currency
        internal decimal FromSEK(decimal value)
        {
            return Math.Round(value / Data.GetCurrency(Currency).Value, 2);
        }
    }
}
