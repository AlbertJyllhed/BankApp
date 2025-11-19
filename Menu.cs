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
            Console.WriteLine("4. Insert money to account");
            Console.WriteLine("5. Transfer balance");
            Console.WriteLine("6. Create loan");
            Console.WriteLine("7. Show current debt");
            Console.WriteLine("8. Log out");

            switch (Input.GetInt())
            {
                case 1:
                    //Console.Clear();
                    customer.CreateBankAccount();
                    break;
                case 2:
                    //Console.Clear();
                    customer.CreateSavingAccount();
                    break;
                case 3:
                    //Console.Clear();
                    customer.PrintBankAccounts();
                    break;
                case 4:
                    customer.InsertMoney();
                    break;
                case 5:
                    //Console.Clear();
                    customer.TransferBalance();
                    break;
                case 6:
                    //Console.Clear();
                    customer.CreateLoan();
                    break;
                case 7:
                    //Console.Clear();
                    customer.PrintLoans();
                    break;
                case 8:
                    //Console.Clear();
                    return false;
                default:
                    Console.WriteLine("Invalid choice, try again.");
                    break;

            }
            Console.WriteLine("Press an key to continue...");
            Console.ReadKey();
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
                case 1:
                    //Console.Clear();
                    admin.UpdateCurrency();
                    break;
                case 2:
                    //Console.Clear();
                    admin.CreateCustomer();
                    break;
                case 3:
                    //Console.Clear();
                    return false;
                default:
                    Console.WriteLine("Invalid choice, try again.");
                    break;
            }
            Console.WriteLine("Press an key to continue...");
            Console.Clear();
            return true;
            
        }
        internal void PrintTitle()
        {
            Console.WriteLine("██████████████████████████████████████████████████████████████████████████████████████████████████████████████\r\n█▌                                                                                                          ▐█\r\n█▌   $$\\       $$\\           $$\\                                 $$$$$$$\\                      $$\\          ▐█\r\n█▌   $$ |      \\__|          $$ |                                $$  __$$\\                     $$ |         ▐█\r\n█▌   $$ |      $$\\  $$$$$$$\\ $$ |  $$\\  $$$$$$\\ $$\\    $$\\       $$ |  $$ | $$$$$$\\  $$$$$$$\\  $$ |  $$\\    ▐█\r\n█▌   $$ |      $$ |$$  _____|$$ | $$  |$$  __$$\\\\$$\\  $$  |      $$$$$$$\\ | \\____$$\\ $$  __$$\\ $$ | $$  |   ▐█\r\n█▌   $$ |      $$ |\\$$$$$$\\  $$$$$$  / $$ /  $$ |\\$$\\$$  /       $$  __$$\\  $$$$$$$ |$$ |  $$ |$$$$$$  /    ▐█\r\n█▌   $$ |      $$ | \\____$$\\ $$  _$$<  $$ |  $$ | \\$$$  /        $$ |  $$ |$$  __$$ |$$ |  $$ |$$  _$$<     ▐█\r\n█▌   $$$$$$$$\\ $$ |$$$$$$$  |$$ | \\$$\\ \\$$$$$$  |  \\$  /         $$$$$$$  |\\$$$$$$$ |$$ |  $$ |$$ | \\$$\\    ▐█\r\n█▌   \\________|\\__|\\_______/ \\__|  \\__| \\______/    \\_/          \\_______/  \\_______|\\__|  \\__|\\__|  \\__|   ▐█\r\n█▌                                                                                                          ▐█\r\n██████████████████████████████████████████████████████████████████████████████████████████████████████████████");
        }
    }
}
