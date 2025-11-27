using BankApp.Users;

namespace BankApp
{
    internal class Menu
    {
        internal bool PrintCustomerMenu(Customer customer)
        {
            PrintUtilities.PrintMessage("--- Kund Meny ---");
            PrintUtilities.PrintMessages(["1. Skapa ett bankkonto",
                "2. Skapa ett sparbankskonto",
                "3. Visa bankkonton ",
                "4. Sätt in pengar",
                "5. Överför pengar",
                "6. Skriv ut transaktioner",
                "7. Ansök om lån",
                "8. Visa nuvarande skuld",
                "9. Logga ut"]);

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
                    customer.PrintBankAccounts();
                    break;
                case 4:
                    customer.InsertMoney();
                    break;
                case 5:
                    customer.TransferBalance();
                    break;
                case 6:
                    customer.PrintTransactionsActivity();
                    break;
                case 7:
                    customer.CreateLoan();
                    break;
                case 8:
                    customer.PrintLoans();
                    break;
                case 9:
                    return false;
                default:
                    PrintUtilities.PrintError("Felaktigt val, försök igen.");
                    break;
            }

            return true;
        }

        internal bool PrintAdminMenu(Admin admin)
        {
            PrintUtilities.PrintMessage("--- Admin Meny ---");
            PrintUtilities.PrintMessages(["1. Uppdatera valuta",
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
                    PrintUtilities.PrintError("Felaktigt val, försök igen.");
                    break;
            }

            return true;
        }
    }
}
