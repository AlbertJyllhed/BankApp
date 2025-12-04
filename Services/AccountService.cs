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
        internal static void Transfer(decimal amount, BankAccount toAccount, BankAccount fromAccount)
        {
            decimal convertedAmount = ConvertCurrency(fromAccount, toAccount, amount);

            // Check if the transfer is possible and perform it
            if (fromAccount.RemoveBalance(amount, toAccount.Name))
            {
                toAccount.AddBalance(convertedAmount, fromAccount.Name);
                //fromAccount.PrintTransferDetails(convertedAmount, toAccount);
            }
            else
            {
                throw new InvalidOperationException("Insufficient funds for the transfer.");
            }


            // Perform the transfer
            fromAccount.RemoveBalance(amount, toAccount.Name);

            Task.Delay(900000).ContinueWith(delay =>
            {
                decimal convertedAmount = ConvertCurrency(fromAccount, toAccount, amount);
                toAccount.AddBalance(convertedAmount, fromAccount.Name);
                fromAccount.PrintTransferDetails(convertedAmount, toAccount);
            });
        }

        // Convert amount to SEK and then to the target account's currency
        private static decimal ConvertCurrency(BankAccount fromAccount, BankAccount toAccount, decimal amount)
        {
            decimal amountInSEK = Data.ToSEK(amount, fromAccount.Currency);
            return Data.FromSEK(amountInSEK, toAccount.Currency);
        }
    }
}
