using BankApp.Users;

namespace BankApp
{
    internal class Menu
    {
        internal bool PrintCustomerMenu(Customer customer)
        {
            PrintUtilities.PrintMessage("--- Customer Menu ---");
            PrintUtilities.PrintMessages(["1. Create an account",
                "2. Create savings account",
                "3. Show bank accounts",
                "4. Insert money to account",
                "5. Transfer balance",
                "6. Print transactions",
                "7. Create loan",
                "8. Show current debt",
                "9. Log out"]);

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
                    PrintUtilities.PrintError("Invalid choice, try again.");
                    break;
            }

            return true;
        }

        internal bool PrintAdminMenu(Admin admin)
        {
            PrintUtilities.PrintMessage("--- Admin Menu ---");
            PrintUtilities.PrintMessages(["1. Update currency",
                "2. Create user",
                "3. Unlock customer",
                "4. Log out"]);

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
                    PrintUtilities.PrintError("Invalid choice, try again.");
                    break;
            }

            return true;
        }
    }
}
