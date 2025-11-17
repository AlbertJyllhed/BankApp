namespace BankApp.BankAccounts
{
    internal class SavingsAccount : BankAccount
    {
        internal decimal Intrest { get; set; } = 1.0186m;

        // Constructors
        internal SavingsAccount(string name, string currency, decimal intrest) : base(name, currency)
        {
            Intrest = intrest;
        }

        internal void PrintSavingsIntrest()
        {

        }

        internal void CreateSavingsAccount()
        {

        }
    }
}
