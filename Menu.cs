using BankApp.Users;
namespace BankApp
{
    internal class Menu
    {
        internal void PrintCustomerMenu(Customer customer)
        {
            Console.WriteLine("");
            Console.WriteLine("1. Create an account");
            Console.WriteLine("2. Create savings account");
            Console.WriteLine("3. Show bank accounts");
            Console.WriteLine("4. Create loan");
            Console.WriteLine("5. Show your loans");

            int choice = Input.GetIndex(5);
            switch (choice)
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
                default:
                    Console.WriteLine("Invalid choice, try again.");
                    break;

            }
        }
        internal void PrintAdminMenu(Admin admin)
        {
            Console.WriteLine("1. Update currency");
            Console.WriteLine("2. Create user");

            int choice = Input.GetIndex(2);
            switch (choice)
            {
                case 0:
                    admin.UpdateCurrency();
                    break;
                case 1:
                    admin.CreateCustomer();
                    break;
                default:
                    Console.WriteLine("Invalid choice, try again.");
                    break;
            }
        }
        internal void PrintTitle()
        {
            Console.WriteLine("██████████████████████████████████████████████████████████████████████████████████████████████████████████████\r\n█▌                                                                                                          ▐█\r\n█▌   $$\\       $$\\           $$\\                                 $$$$$$$\\                      $$\\          ▐█\r\n█▌   $$ |      \\__|          $$ |                                $$  __$$\\                     $$ |         ▐█\r\n█▌   $$ |      $$\\  $$$$$$$\\ $$ |  $$\\  $$$$$$\\ $$\\    $$\\       $$ |  $$ | $$$$$$\\  $$$$$$$\\  $$ |  $$\\    ▐█\r\n█▌   $$ |      $$ |$$  _____|$$ | $$  |$$  __$$\\\\$$\\  $$  |      $$$$$$$\\ | \\____$$\\ $$  __$$\\ $$ | $$  |   ▐█\r\n█▌   $$ |      $$ |\\$$$$$$\\  $$$$$$  / $$ /  $$ |\\$$\\$$  /       $$  __$$\\  $$$$$$$ |$$ |  $$ |$$$$$$  /    ▐█\r\n█▌   $$ |      $$ | \\____$$\\ $$  _$$<  $$ |  $$ | \\$$$  /        $$ |  $$ |$$  __$$ |$$ |  $$ |$$  _$$<     ▐█\r\n█▌   $$$$$$$$\\ $$ |$$$$$$$  |$$ | \\$$\\ \\$$$$$$  |  \\$  /         $$$$$$$  |\\$$$$$$$ |$$ |  $$ |$$ | \\$$\\    ▐█\r\n█▌   \\________|\\__|\\_______/ \\__|  \\__| \\______/    \\_/          \\_______/  \\_______|\\__|  \\__|\\__|  \\__|   ▐█\r\n█▌                                                                                                          ▐█\r\n██████████████████████████████████████████████████████████████████████████████████████████████████████████████");
        }
    }
}
