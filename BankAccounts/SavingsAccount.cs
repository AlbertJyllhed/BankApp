namespace BankApp.BankAccounts
{
    internal class SavingsAccount : BankAccount
    {
        internal decimal Intrest { get; set; } = 1.0186m;
        internal decimal Amount { get; set; } = 0;

        // Constructors
        internal SavingsAccount(string name, string currency, decimal intrest) : base(name, currency)
        {
            Intrest = intrest;
        }

        internal void CreateSavingAccount(string countyCode)
        {
            //Ask user to choose the name of the created account.
            Console.WriteLine("Savings account name:");
            var accountName = Input.GetString();

            var currency = Data.GetCurrency();

            Console.WriteLine($"Intrest rate :{Intrest}");

            //Add the new bank account into the list.
            var bankAccount = new SavingsAccount(accountName, currency);
            BankAccounts.Add(bankAccount);
        }
    }
}
