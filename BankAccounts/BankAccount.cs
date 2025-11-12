namespace BankApp.BankAccounts
{
    internal class BankAccount
    {
        internal int ID { get; set; }
        internal string Name { get; set; }
        private decimal Balance { get; set; } = 0;
        internal List<decimal> Transactions { get; set; } = [];

        internal BankAccount(string name)
        {
            Name = name;
            ID = GenerateID();
        }
        
        internal void AddBalance(decimal balance)
        {
            Transactions.Add(balance);
            Balance += balance;

        }

        //Not done yet, need rest value 
        internal decimal RemoveBalance(decimal value)
        {
            if (Balance > value)
            {
                Balance -= value;
                return value;
            }
            else
            {
                return 0;
            }
        }

        internal decimal GetBalance()
        {
            return Balance;
        }

        internal void PrintTransactions()
        {
            Console.WriteLine("--- Transactions ---");
            foreach (var transaction in Transactions)
            {
                Console.WriteLine($"* {transaction} kr");
            }
        }

        //Method to print info and the method gets called in get PrintBankAccounts
        internal void PrintInfo()
        {
            Console.WriteLine($"Name: {Name} [{ID}]\n" +
                $"Balance: {Balance}");
        }
    }
}
