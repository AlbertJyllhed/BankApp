using BankApp.BankAccounts;

namespace BankApp.Services
{
    internal class AccountService
    {
        // Method to create a new bank account without user input
        internal BankAccount CreateBankAccount(string accountName, string currency, string id = "")
        {
            var bankAccount = new BankAccount(accountName, currency, id);
            //BankAccounts.Add(bankAccount);
            Data.AddBankAccount(bankAccount);
            return bankAccount;
        }

        // Method to create a new savings account and add initial balance
        internal SavingsAccount CreateSavingsAccount(string accountName, decimal amount, string currency, string id = "")
        {
            var savingsAccount = new SavingsAccount(accountName, amount, currency, id);
            Data.AddBankAccount(savingsAccount);
            return savingsAccount;

            //UI.PrintMessage(savingsAccount.GetInterestInfo(amount));
            //UI.PrintMessage(savingsAccount.GetLatestTransactionInfo());
        }
    }
}
