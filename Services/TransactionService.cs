using BankApp.BankAccounts;
using BankApp.Users;

namespace BankApp.Services
{
    internal static class TransactionService
    {
        private static Customer? _customer;

        internal static void SetCustomer(Customer customer)
        {
            _customer = customer;
        }

        // Method to insert money into a bank account
        internal static void InsertMoney()
        {
            // Check if customer is set
            if (_customer == null)
            {
                UI.PrintError("Ingen kund hittades.");
                return;
            }

            // Choose which account to insert money into
            UI.PrintMessage("Vilket konto vill du sätta in pengar på?");
            var bankAccounts = _customer.GetBankAccounts();

            UI.PrintList(bankAccounts, true);
            int index = InputUtilities.GetIndex(bankAccounts.Count);
            BankAccount insertMoneyAccount = bankAccounts[index];

            // Choose amount to insert
            UI.PrintMessage($"Hur mycket pengar vill du sätta in till {insertMoneyAccount.Name}?");
            decimal amount = InputUtilities.GetPositiveDecimal();

            // Insert the money
            insertMoneyAccount.AddBalance(amount);
            UI.PrintMessage(insertMoneyAccount.GetLatestTransactionInfo());
        }

        // Transfers balance from one account to another
        internal static void TransferBalance()
        {
            // Check if customer is set
            if (_customer == null)
            {
                UI.PrintError("Ingen kund hittades.");
                return;
            }

            // Check if there are any bank accounts to transfer from
            if (!_customer.HasBankAccounts())
            {
                UI.PrintError("Du har inget bankkonto.");
                return;
            }

            // Choose from which account to transfer
            UI.PrintMessage("Från vilket konto vill du överföra pengarna?");
            UI.PrintList(_customer.GetBankAccounts(), true);
            BankAccount fromAccount = _customer.GetAccountByIndex();

            BankAccount? toAccount;

            // Choose to which account to transfer (using index or ID)
            if (ChooseTransferMethod())
            {
                UI.PrintMessage("Vilket bankkonto vill du överföra pengarna till? Ange index.");
                toAccount = _customer.GetAccountByIndex();
            }
            else
            {
                UI.PrintMessage("Vilket bankkonto vill du överföra pengarna till? Ange kontonummer");
                toAccount = _customer.GetAccountByID();
            }

            if (toAccount == null)
            {
                UI.PrintError("Inget konto hittades, försök igen.");
                return;
            }

            // Choose amount to transfer
            UI.PrintMessage("Hur mycket pengar vill du överföra?");
            decimal amount = InputUtilities.GetPositiveDecimal();

            // Check if there are sufficient funds and perform the transfer
            if (CanTransfer(fromAccount, amount))
            {
                Transfer(amount, toAccount, fromAccount);
                UI.PrintColoredMessage($"" +
                    $"Överföring påbörjad: {amount} {fromAccount.Currency}\n" +
                    $"Från: Konto [{fromAccount.ID}]\n" +
                    $"Till: [{toAccount.ID}]\n" +
                    $"Pengar kommer fram klockan {DateTime.Now.AddMinutes(15):HH:mm}",
                    ConsoleColor.DarkCyan);
            }
            else
            {
                UI.PrintError("Överföring misslyckades på grund av för låg summa.");
            }
        }

        // Method to choose transfer method
        private static bool ChooseTransferMethod()
        {
            UI.PrintMessage("Hur vill du överföra pengarna?\n" +
                "1. Med index\n" +
                "2. Med kontonummer");
            return InputUtilities.GetIndex(2) == 0;
        }

        // Check if there are sufficient funds to transfer
        private static bool CanTransfer(BankAccount fromAccount, decimal amount)
        {
            return fromAccount.GetBalance() >= amount;
        }

        // Method to handle transfer to another account
        private static bool Transfer(decimal amount, BankAccount toAccount, BankAccount fromAccount)
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
                fromAccount.PrintTransferDetails(convertedAmount, toAccount);
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
