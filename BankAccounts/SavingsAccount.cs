namespace BankApp.BankAccounts
{
    internal class SavingsAccount : BankAccount
    {
        internal decimal Intrest { get; set; } = 0.0186m;

        internal SavingsAccount(string name, string currency, decimal intrest) : base(name, currency)
        {
            Intrest = intrest;
        }

        internal void CreateSavingsAccount()
        {

        }
    }
}
