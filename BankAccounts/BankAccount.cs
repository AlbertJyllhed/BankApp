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
            Console.WriteLine($"{value} {Currency} was transferred  to account {ID}.");
        }

        // Print transfer details with from and to account IDs
        internal void PrintTransferDetails(decimal originalAmount, string fromCurrency, decimal convertedAmount,
            string toCurrency, string fromAccountId, string toAccountId)
        {
            if (fromCurrency == toCurrency)
            {
                Console.WriteLine(
        $"{originalAmount} {fromCurrency} was transferred from account {fromAccountId} to account {toAccountId}.");
            }
            else
            {
                Console.WriteLine(
                    $"{originalAmount} {fromCurrency} was transferred from account {fromAccountId}, converted into {convertedAmount} {toCurrency} and transferred to account {toAccountId}.");
            }
        }


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
                Console.WriteLine("Invalid request, not enough money in account.\n" +
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
            Console.WriteLine($"--- Transactions {Name} [{ID}] ---");
            foreach (var transaction in _transactions)
            {
                Console.WriteLine($"* {transaction} {Currency}");
            }
            Console.WriteLine("");
        }

        internal string GetAccountType()
        {
            return GetType().Name;
        }

        //Method to print info and the method gets called in get PrintBankAccounts
        internal void PrintInfo()
        {
            Console.WriteLine($"{GetAccountType()} {Name} [{ID}]\n" +
                $"Balance: {Balance} {Currency}");
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
