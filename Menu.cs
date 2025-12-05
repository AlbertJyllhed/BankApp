using BankApp.Users;
using BankApp.Services;

namespace BankApp
{
    internal class Menu
    {
        internal bool PrintCustomerMenu(Customer customer)
        {
            UI.PrintMessage("--- Kund Meny ---");
            UI.PrintMessages(["1. Skapa ett bankkonto",
                "2. Visa bankkonton ",
                "3. Betala och överföra",
                "4. Gå till lånemenyn",
                "5. Logga ut"]);

            int input = InputUtilities.GetInt();
            Console.Clear();

            switch (input)
            {
                case 1:
                    AccountService.SetupBankAccount(customer);
                    break;
                case 2:
                    UI.PrintList(customer.GetBankAccounts(), true);
                    break;
                case 3:
                    PrintTransactionMenu(customer);
                    break;
                case 4:
                    PrintLoanMenu(customer);
                    break;
                case 5:
                    return false;
                default:
                    UI.PrintError("Felaktigt val, försök igen.");
                    break;
            }
            return true;
        }

        internal void PrintLoanMenu(Customer customer)
        {
            UI.PrintMessage("--- Lån Meny ---");
            UI.PrintMessages([
                "1. Ansök om lån",
                "2. Visa nuvarande lån",
                "3. Betala tillbaka lån",
                "4. Gå tillbaka"
            ]);

            int input = InputUtilities.GetInt();
            Console.Clear();

            LoanService.SetCustomer(customer);

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
                case 4:
                    return;
                default:
                    UI.PrintError("Felaktigt val, försök igen.");
                    break;
            }
        }

        internal void PrintTransactionMenu(Customer customer)
        {
            UI.PrintMessage("--- Betala och överföra ---");
            UI.PrintMessages([
                "1. Sätt in pengar",
                "2. Ta ut pengar",
                "3. Överför pengar",
                "4. Skriv ut transaktioner",
                "5. Gå tillbaka"
            ]);

            int input = InputUtilities.GetInt();
            Console.Clear();

            TransactionService.SetCustomer(customer);

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
                case 5:
                    return;
                default:
                    UI.PrintError("Felaktigt val, försök igen.");
                    break;
            }
        }

        internal bool PrintAdminMenu(Admin admin)
        {
            UI.PrintMessage("--- Admin Meny ---");
            UI.PrintMessages(["1. Uppdatera valuta",
                "2. Skapa användare",
                "3. Lås upp låst kund profil",
                "4. Logga ut"]);

            int input = InputUtilities.GetInt();
            Console.Clear();

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
                case 4:
                    return false;
                default:
                    UI.PrintError("Felaktigt val, försök igen.");
                    break;
            }
            return true;
        }
    }
}
