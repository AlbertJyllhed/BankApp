namespace BankApp.BankAccounts
{
    internal class BankAccount
    {
        internal int ID { get; set; }
        internal string Name { get; set; }
        internal string Currency { get; set; } = "SEK";
        private decimal Balance { get; set; } = 0;
        internal List<decimal> Transactions { get; set; } = [];

        internal BankAccount(string name, string currency)
        {
            Name = name;
            Currency = currency;
            ID = Data.GetUniqueID();
        }

        internal void AddBalance(decimal value)
        {
            value = ConvertCurrency(value);
            Transactions.Add(value);
            Balance += value;
            Console.WriteLine($"{value} {Currency} was transfered to account {ID}.");
        }

        //Not done yet, need rest value 
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
            Console.WriteLine($"Name: {Name} [{ID}]\n" +
                $"Balance: {Balance}");
        }

        // Convert current balance to SEK and then to account currency
        private decimal ConvertCurrency(decimal value)
        {
            return value /= Data.currency["SEK"] * Data.currency[Currency];
        }


    }
}
