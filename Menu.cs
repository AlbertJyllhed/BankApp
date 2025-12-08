using BankApp.Users;
using BankApp.Services;

namespace BankApp
{
    internal class Menu
    {
        // Customer Menu
        internal bool PrintCustomerMenu(Customer customer)
        {
            UI.PrintMessage("1. Skapa bankkonto\n" +
                "2. Mina bankkonton\n" +
                "3. Betala och överföra\n" +
                "4. Lån meny\n" +
                "0. Logga ut");

            int input = InputUtilities.GetInt();
            Console.Clear();

            // Handle customer menu options
            switch (input)
            {
                case 1:
                    AccountService.SetupBankAccount(customer);
                    break;
                case 2:
                    UI.PrintMessage("--- Mina bankkonton ---", 1);
                    UI.PrintList(customer.GetBankAccounts(), true);
                    break;
                case 3:
                    PrintTransactionMenu(customer);
                    break;
                case 4:
                    PrintLoanMenu(customer);
                    break;
                case 0:
                    return false;
                default:
                    UI.PrintError("Felaktigt val, försök igen.");
                    break;
            }
            return true;
        }

        // Loan Menu
        internal void PrintLoanMenu(Customer customer)
        {
            UI.PrintMessage("--- Lån meny ---\n\n" +
                "1. Ansök om lån\n" +
                "2. Mina lån\n" +
                "3. Betala tillbaka lån\n" +
                "0. Gå tillbaka");

            int input = InputUtilities.GetInt();
            Console.Clear();

            LoanService.SetCustomer(customer);

            // Handle loan menu options
            switch (input)
            {
                case 1:
                    LoanService.LoanSetup();
                    break;
                case 2:
                    customer.PrintLoans();
                    break;
                case 3:
                    LoanService.PayBackLoan();
                    break;
                case 0:
                    return;
                default:
                    UI.PrintError("Felaktigt val, försök igen.");
                    break;
            }
        }

        // Transaction Menu
        internal void PrintTransactionMenu(Customer customer)
        {
            UI.PrintMessage("--- Betala och överföra ---\n\n" +
                "1. Sätt in pengar\n" +
                "2. Ta ut pengar\n" +
                "3. Överför pengar\n" +
                "4. Mina transaktioner\n" +
                "0. Gå tillbaka");

            int input = InputUtilities.GetInt();
            Console.Clear();

            TransactionService.SetCustomer(customer);

            // Handle transaction menu options
            switch (input)
            {
                case 1:
                    TransactionService.InsertMoney();
                    break;
                case 2:
                    TransactionService.WithdrawMoney();
                    break;
                case 3:
                    TransactionService.TransferMoney();
                    break;
                case 4:
                    customer.PrintTransactions();
                    break;
                case 0:
                    return;
                default:
                    UI.PrintError("Felaktigt val, försök igen.");
                    break;
            }
        }

        // Admin Menu
        internal bool PrintAdminMenu(Admin admin)
        {
            UI.PrintMessage("1. Uppdatera valuta\n" +
                "2. Skapa användare\n" +
                "3. Lås upp kund\n" +
                "0. Logga ut");

            int input = InputUtilities.GetInt();
            Console.Clear();

            // Handle admin menu options
            switch (input)
            {
                case 1:
                    admin.UpdateCurrency();
                    break;
                case 2:
                    admin.CreateCustomer();
                    break;
                case 3:
                    admin.UnlockCustomerAccount();
                    break;
                case 0:
                    return false;
                default:
                    UI.PrintError("Felaktigt val, försök igen.");
                    break;
            }
            return true;
        }
    }
}
