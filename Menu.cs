using BankApp.Users;
namespace BankApp
{
    internal class Menu
    {
        internal bool PrintCustomerMenu(Customer customer)
        {
            Console.WriteLine("What type of banking transaction would you like to proceed with?");
            Console.WriteLine("1. Create an account");
            Console.WriteLine("2. Create savings account");
            Console.WriteLine("3. Show bank accounts");
            Console.WriteLine("4. Create loan");
            Console.WriteLine("5. Show current debt");
            Console.WriteLine("6. Log out");

            switch (Input.GetInt())
            {
                case 0:
                    customer.CreateBankAccount();
                    break;
                case 1:
                    customer.CreateSavingAccount();
                    break;
                case 2:
                    customer.PrintBankAccounts();
                    break;
                case 3:
                    customer.CreateLoan();
                    break;
                case 4:
                    customer.PrintLoans();
                    break;
                case 5:
                    return false;
                default:
                    Console.WriteLine("Invalid choice, try again.");
                    break;

            }
            Console.Clear();
            return true;
        }
        internal bool PrintAdminMenu(Admin admin)
        {
            Console.WriteLine("1. Update currency");
            Console.WriteLine("2. Create user");
            Console.WriteLine("3. Log out ");
           
            switch (Input.GetInt())
            {
                case 0:
                    admin.UpdateCurrency();
                    break;
                case 1:
                    admin.CreateCustomer();
                    break;
                case 2:
                    return false;
                default:
                    Console.WriteLine("Invalid choice, try again.");
                    break;
            }
            Console.Clear();
            return true;
            
        }
        internal void PrintTitle()
        {
            Console.WriteLine("██████████████████████████████████████████████████████████████████████████████████████████████████████████████\r\n█▌                                                                                                          ▐█\r\n█▌   $$\\       $$\\           $$\\                                 $$$$$$$\\                      $$\\          ▐█\r\n█▌   $$ |      \\__|          $$ |                                $$  __$$\\                     $$ |         ▐█\r\n█▌   $$ |      $$\\  $$$$$$$\\ $$ |  $$\\  $$$$$$\\ $$\\    $$\\       $$ |  $$ | $$$$$$\\  $$$$$$$\\  $$ |  $$\\    ▐█\r\n█▌   $$ |      $$ |$$  _____|$$ | $$  |$$  __$$\\\\$$\\  $$  |      $$$$$$$\\ | \\____$$\\ $$  __$$\\ $$ | $$  |   ▐█\r\n█▌   $$ |      $$ |\\$$$$$$\\  $$$$$$  / $$ /  $$ |\\$$\\$$  /       $$  __$$\\  $$$$$$$ |$$ |  $$ |$$$$$$  /    ▐█\r\n█▌   $$ |      $$ | \\____$$\\ $$  _$$<  $$ |  $$ | \\$$$  /        $$ |  $$ |$$  __$$ |$$ |  $$ |$$  _$$<     ▐█\r\n█▌   $$$$$$$$\\ $$ |$$$$$$$  |$$ | \\$$\\ \\$$$$$$  |  \\$  /         $$$$$$$  |\\$$$$$$$ |$$ |  $$ |$$ | \\$$\\    ▐█\r\n█▌   \\________|\\__|\\_______/ \\__|  \\__| \\______/    \\_/          \\_______/  \\_______|\\__|  \\__|\\__|  \\__|   ▐█\r\n█▌                                                                                                          ▐█\r\n██████████████████████████████████████████████████████████████████████████████████████████████████████████████");
        }
    }
}
