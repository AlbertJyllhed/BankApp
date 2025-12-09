using BankApp.BankAccounts;
using BankApp.Users;

namespace BankApp.Services
{
    internal static class AccountService
    {
        // Method to get user input and create a new bank account
        internal static void SetupBankAccount(Customer customer)
        {
            UI.PrintMessage("Vad för typ av bankkonto vill du skapa?\n\n" +
                "1. Vanligt bankkonto\n" +
                "2. Sparkonto", 1);

            int choice = InputUtilities.GetIndex(2);

            // Ask user to choose the name of the created account.
            UI.PrintInputPrompt("Bankkonto namn: ");
            var accountName = InputUtilities.GetString();

            // Ask user to choose the currency of the created account.
            var currency = Data.ChooseCurrency().Key;

            // Create the bank account based on user choice
            BankAccount account;
            if (choice == 0)
            {
                account = CreateBankAccount(accountName, currency);
            }
            else
            {
                // Ask user for initial deposit amount for savings account
                UI.PrintMessage("Hur mycket vill du sätta in på sparkontot?", 1);
                var amount = InputUtilities.GetPositiveDecimal();
                account = CreateSavingsAccount(accountName, amount, currency);
                var savingsAccount = (SavingsAccount)account;
                UI.PrintMessage(savingsAccount.GetInterestInfo(amount));

                var latestTransaction = savingsAccount.GetLatestTransaction();
                UI.PrintMessage(latestTransaction.ToString());
            }

            // Add the new bank account into the list.
            customer.AddBankAccount(account);

            UI.PrintColoredMessage($"\nDitt nya {account.GetAccountType()} " +
                $"({accountName}, {currency}) har skapats!", ConsoleColor.Green, 1);
            UI.PrintResetMessage();
        }

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
        }
    }
}
