namespace BankApp.BankAccounts
{
    internal class BankAccount
    {
        internal int ID { get; set; }
        internal string Name { get; set; }
        internal string Currency { get; set; } = "SEK";
        protected decimal Balance { get; set; } = 0;
        internal List<decimal> Transactions { get; set; } = [];

        // Constructor
        internal BankAccount(string name, string currency)
        {
            Name = name;
            Currency = currency;
            ID = Data.GetUniqueID();
        }

        // Method to add balance to account
        internal void AddBalance(decimal value)
        {
            value = ConvertCurrency(value);
            Transactions.Add(value);
            Balance += value;
            Console.WriteLine($"{value} {Currency} was transfered to account {ID}.");
        }

        // Method to remove balance from account
        internal decimal RemoveBalance(decimal value)
        {
            value = ConvertCurrency(value);

            if (Balance > value)
            {
                Console.WriteLine($"{value} {Currency} was transferred from account {ID}.");
                Balance -= value;
                Transactions.Add(-value);
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
            Console.WriteLine("--- Transactions ---");
            foreach (var transaction in Transactions)
            {
                Console.WriteLine($"* {transaction} {Currency}, Account: {ID}");
            }
        }

        //Method to print info and the method gets called in get PrintBankAccounts
        internal void PrintInfo()
        {
            Console.WriteLine($"{Name} [{ID}]\n" +
                $"Balance: {Balance} {Currency}");
        }

        // Convert current balance to SEK and then to account currency
        private decimal ConvertCurrency(decimal value)
        {
            return value /= Data.currency["SEK"] * Data.currency[Currency];
        }


    }
}
