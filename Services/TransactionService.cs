using BankApp.BankAccounts;
using BankApp.Users;
using System.Timers;

namespace BankApp.Services
{
    internal static class TransactionService
    {
        private static Customer? _customer;

        // Simulate delay of 15 minutes (900,000 milliseconds)
        private static System.Timers.Timer _transferTimer = new System.Timers.Timer(30000);

        internal static void SetCustomer(Customer customer)
        {
            _customer = customer;
        }

        // Method to set up the timer for transfers
        internal static void SetupAutoTransfers()
        {
            _transferTimer.Elapsed += PerformTransfer;
            _transferTimer.AutoReset = true;
            _transferTimer.Enabled = true;
        }

        // Method to get remaining time for next transfer
        internal static double GetRemainingTimeForNextTransfer()
        {
            return _transferTimer.Interval;
        }

        // Method to insert money into a bank account
        internal static void InsertMoney()
        {
            // Check if customer is set
            if (_customer == null)
            {
                UI.PrintError("Ingen kund hittades.");
                UI.PrintResetMessage();
                return;
            }

            // Check if there are any bank accounts to transfer from
            if (!_customer.HasBankAccounts())
            {
                UI.PrintError("Inget bankkonto hittades.");
                UI.PrintResetMessage();
                return;
            }

            // Choose which account to insert money into
            UI.PrintMessage("Vilket konto vill du sätta in pengar på?", 1);
            var bankAccounts = _customer.GetBankAccounts();

            UI.PrintList(bankAccounts, true);
            int index = InputUtilities.GetIndex(bankAccounts.Count);
            BankAccount insertMoneyAccount = bankAccounts[index];

            // Choose amount to insert
            UI.PrintMessage($"\nHur mycket pengar vill du sätta in till bankkonto {insertMoneyAccount.ID}?");
            decimal amount = InputUtilities.GetPositiveDecimal();

            // Insert the money
            insertMoneyAccount.AddBalance(amount);

            var latestTransaction = insertMoneyAccount.GetLatestTransaction();
            UI.PrintColoredMessage($"\n{latestTransaction.ToString()}", ConsoleColor.Green, 1);
            UI.PrintResetMessage();
        }

        // Method to withdraw money from a bank account
        internal static void WithdrawMoney()
        {
            // Check if customer is set
            if (_customer == null)
            {
                UI.PrintError("Ingen kund hittades.");
                UI.PrintResetMessage();
                return;
            }

            // Check if there are any bank accounts to transfer from
            if (!_customer.HasBankAccounts())
            {
                UI.PrintError("Inget bankkonto hittades.");
                UI.PrintResetMessage();
                return;
            }

            // Choose which account to withdraw money from
            UI.PrintMessage("Vilket konto vill du ta ut pengar från?", 1);
            var bankAccounts = _customer.GetBankAccounts();

            UI.PrintList(bankAccounts, true);
            int index = InputUtilities.GetIndex(bankAccounts.Count);
            BankAccount withdrawMoneyAccount = bankAccounts[index];

            // Choose amount to withdraw
            UI.PrintMessage($"\nHur mycket pengar vill du ta ut från bankkonto {withdrawMoneyAccount.ID}?");
            decimal amount = InputUtilities.GetPositiveDecimal();

            // Withdraw the money
            if (withdrawMoneyAccount.RemoveBalance(amount))
            {
                var latestTransaction = withdrawMoneyAccount.GetLatestTransaction();
                UI.PrintColoredMessage($"\n{latestTransaction.ToString()}", ConsoleColor.Red, 1);
            }
            else
            {
                UI.PrintError("Uttag misslyckades, " +
                    "det finns inte tillräckligt med pengar på kontot.");
            }
            UI.PrintResetMessage();
        }

        // Transfers balance from one account to another
        internal static void TransferMoney()
        {
            // Check if customer is set
            if (_customer == null)
            {
                UI.PrintError("Ingen kund hittades.");
                UI.PrintResetMessage();
                return;
            }

            // Check if there are any bank accounts to transfer from
            if (!_customer.HasBankAccounts())
            {
                UI.PrintError("Inget bankkonto hittades.");
                UI.PrintResetMessage();
                return;
            }

            // Choose from which account to transfer
            UI.PrintMessage("Vilket konto vill du överföra pengar ifrån?", 1);
            UI.PrintList(_customer.GetBankAccounts(), true);
            BankAccount fromAccount = _customer.GetAccountByIndex();

            BankAccount? toAccount;

            // Choose to which account to transfer (using index or ID)
            if (ChooseTransferMethod())
            {
                UI.PrintMessage("\nVilket bankkonto vill du överföra pengarna till? Ange index.");
                toAccount = _customer.GetAccountByIndex();
            }
            else
            {
                UI.PrintMessage("\nVilket bankkonto vill du överföra pengarna till? Ange kontonummer");
                toAccount = _customer.GetAccountByID();
            }
            if(fromAccount == toAccount)
            {
                UI.PrintError("Du kan inte överföra pengar till samma konto.");
                UI.PrintResetMessage();
                return;
            }

            if (toAccount == null)
            {
                UI.PrintError("Inget konto hittades, försök igen.");
                UI.PrintResetMessage();
                return;
            }

            // Choose amount to transfer
            UI.PrintMessage("\nHur mycket pengar vill du överföra?");
            decimal amount = InputUtilities.GetPositiveDecimal();

            // Check if there are sufficient funds and perform the transfer
            if (CanTransfer(fromAccount, toAccount, amount))
            {
                UI.PrintColoredMessage($"\nÖverföring påbörjad: {amount} {fromAccount.Currency}\n" +
                    $"Från: Konto [{fromAccount.ID}]\n" +
                    $"Till: [{toAccount.ID}]\n" +
                    $"Pengarna kommer fram klockan {GetRemainingTimeForNextTransfer():HH:mm}",
                    ConsoleColor.DarkCyan, 1);
            }
            else
            {
                UI.PrintError("Överföring misslyckades, " +
                    "det finns inte tillräckligt med pengar på kontot.");
            }
            UI.PrintResetMessage();
        }

        // Method to choose transfer method
        private static bool ChooseTransferMethod()
        {
            UI.PrintMessage("\nVill du flytta pengar mellan dina egna konton " +
                "eller till någon annan användare?\n\n" +
                "1. Eget konto\n" +
                "2. Annat konto");
            return InputUtilities.GetIndex(2) == 0;
        }

        // Check if there are sufficient funds to transfer
        private static bool CanTransfer(BankAccount fromAccount, BankAccount toAccount, decimal amount)
        {
            return fromAccount.RemoveBalance(amount, fromAccount.ID, toAccount.ID);
        }

        // Method to perform transfer for timer event
        private static void PerformTransfer(object? sender, ElapsedEventArgs e)
        {
            var transactions = Data.GetTransactions();

            foreach (var transaction in transactions)
            {
                // Skip if transaction is already completed
                if (transaction.Completed)
                {
                    continue;
                }

                // Get data from transaction
                var fromAccount = Data.GetBankAccount(transaction.FromAccountID);
                var toAccount = Data.GetBankAccount(transaction.ToAccountID);
                decimal amount = Math.Abs(transaction.Amount);

                // Skip if accounts are not found
                if (fromAccount == null || toAccount == null)
                {
                    continue;
                }

                // Perform currency conversion and add balance to the target account
                decimal convertedAmount = ConvertCurrency(fromAccount, toAccount, amount);
                toAccount.AddBalance(convertedAmount, fromAccount.ID);
                transaction.SetCompleted();
            }
        }

        // Convert amount to SEK and then to the target account's currency
        private static decimal ConvertCurrency(BankAccount fromAccount, BankAccount toAccount, decimal amount)
        {
            decimal amountInSEK = Data.ToSEK(amount, fromAccount.Currency);
            return Data.FromSEK(amountInSEK, toAccount.Currency);
        }
    }
}
