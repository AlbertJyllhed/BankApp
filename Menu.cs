using BankApp.Services;
using BankApp.Users;

namespace BankApp
{
    internal class Menu
    {
        internal bool PrintCustomerMenu(Customer customer)
        {
            UI.PrintMessage("--- Kund Meny ---");
            UI.PrintMessages(["1. Skapa ett bankkonto",
                "2. Skapa ett sparbankskonto",
                "3. Visa bankkonton ",
                "4. Betala och överföra",
                "5. Gå till lånemenyn",
                "6. Logga ut"]);

            int input = InputUtilities.GetInt();
            Console.Clear();

            switch (input)
            {
                case 1:
                    customer.SetupBankAccount();
                    break;
                case 2:
                    customer.CreateSavingAccount();
                    break;
                case 3:
                    UI.PrintList(customer.GetBankAccounts(), true);
                    break;
                case 4:
                    PrintTransactionMenu(customer);
                    break;
                case 5:
                    while (PrintLoanMenu(customer)) ;
                    break;
                case 6:
                    return false;
                default:
                    UI.PrintError("Felaktigt val, försök igen.");
                    break;
            }

            return true;
        }

        internal bool PrintLoanMenu(Customer customer)
        {
            UI.PrintMessage("--- Lån Meny ---");
            UI.PrintMessages([
                "1. Ansök om lån",
                "2. Visa nuvarande lån",
                "3. Betala tillbaka lån",
                "4. Gå tillbaka till huvudmenyn"
            ]);

            int input = InputUtilities.GetInt();
            Console.Clear();

            switch (input)
            {
                case 1:
                    customer.LoanSetup();
                    break;
                case 2:
                    customer.PrintLoans();
                    break;
                case 3:
                    customer.PayBackLoan();
                    break;
                case 4:
                    return false;
                default:
                    UI.PrintError("Felaktigt val, försök igen.");
                    break;
            }
            return true;
        }

        internal void PrintTransactionMenu(Customer customer)
        {
            UI.PrintMessage("--- Betala och överföra ---");
            UI.PrintMessages([
                "1. Sätt in pengar",
                "2. Överföra pengar",
                "3. Skriv ut transaktioner"
            ]);

            int input = InputUtilities.GetInt();
            Console.Clear();

            switch (input)
            {
                case 1:
                    customer.InsertMoney();
                    break;
                case 2:
                    customer.TransferBalance();
                    break;
                case 3:
                    customer.PrintTransactionsActivity();
                    break;
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

        internal void PrintInsertMenu()
        {
            UI.PrintMessage("Vilket konto vill du sätta in pengar på?");

        }
    }
}
