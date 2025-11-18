using BankApp.Users;

namespace BankApp.BankAccounts
{
    internal class SavingsAccount : BankAccount
    {
        internal decimal Interest { get; set; } = 1.0186m;

        // Constructors
        internal SavingsAccount(string name, string currency, decimal interest) : base(name, currency)
        {
            Interest = interest;
        }


        //internal void CreateSavingAccount(string countyCode)
        //{
        //    //Ask user to choose the name of the created account.
        //    Console.WriteLine("Savings account name:");
        //    var accountName = Input.GetString();

        //    Console.WriteLine("What amount do you want put in the savings account?");
        //    var amount = Input.GetDecimal();

        //    var currency = Data.GetCurrency();

        //    Console.WriteLine($"Intrest rate :{Intrest}");

        //    //Add the new bank account into the list.
        //    var bankAccount = new BankAccount(accountName, currency);
        //    BankAccounts.Add(bankAccount);
        //}
    }
}
