using BankApp.BankAccounts;

namespace BankApp.Services
{
    internal static class AccountService
    {
        // Method to create a new bank account without user input
        internal static BankAccount CreateBankAccount(string accountName, string currency, string id = "")
        {
            var bankAccount = new BankAccount(accountName, currency, id);
            Data.AddBankAccount(bankAccount);
            return bankAccount;
        }

        // Method to create a new savings account and add initial balance
        internal static SavingsAccount CreateSavingsAccount(string accountName, decimal amount, string currency, string id = "")
        {
            var savingsAccount = new SavingsAccount(accountName, amount, currency, id);
            Data.AddBankAccount(savingsAccount);
            return savingsAccount;

            //UI.PrintMessage(savingsAccount.GetInterestInfo(amount));
            //UI.PrintMessage(savingsAccount.GetLatestTransactionInfo());
        }

        // Method to handle transfer to another account
        internal static bool Transfer(decimal amount, BankAccount toAccount, BankAccount fromAccount)
        {
            // Check if the transfer is possible and return false if not
            if (!fromAccount.RemoveBalance(amount, toAccount.Name))
            {
                return false;
            }

            // Simulate delay of 15 minutes (900,000 milliseconds)
            Task.Delay(900000).ContinueWith(delay =>
            {
                // Perform currency conversion and add balance to the target account
                decimal convertedAmount = ConvertCurrency(fromAccount, toAccount, amount);
                toAccount.AddBalance(convertedAmount, fromAccount.Name);
                //fromAccount.PrintTransferDetails(convertedAmount, toAccount);
            });
            return true;
        }

        // Convert amount to SEK and then to the target account's currency
        private static decimal ConvertCurrency(BankAccount fromAccount, BankAccount toAccount, decimal amount)
        {
            decimal amountInSEK = Data.ToSEK(amount, fromAccount.Currency);
            return Data.FromSEK(amountInSEK, toAccount.Currency);
        }
    }
}
