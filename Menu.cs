using BankApp.Users;
namespace BankApp
{
    internal class Menu
    {
        internal void PrintUserMenu()
        {
            Console.WriteLine("");
            Console.WriteLine("1. Create an account");
            Console.WriteLine("2. Show bank accounts");
            Console.WriteLine("3. Create loan");
            Console.WriteLine("4. Show your loans");

            int choice = Input.GetIndex( 4);
            switch (choice)
            {
                case 1:
                    User.CreateBankAccount();
                    break;
                case 2:
                    User.PrintBankAccounts();
                    break; 
                case 3:
                    User.CreateLoan();
                    break;
                case 4:
                    User.PrintLoans();
                
            }
        }
        internal void PrintAdminMenu()
        {
            Console.WriteLine("1. Update currency");
            Console.WriteLine("2. Create user");

            int choice = Input.GetIndex(2);
            switch (choice)
            {
                case 1:
                    Admin.UpdeteCurrency();
                    break;
                case 2:
                    Admin.CreateUser();
                    break;
            }
        }
        internal void Title()
        {
            Console.WriteLine("██████████████████████████████████████████████████████████████████████████████████████████████████████████████\r\n█▌                                                                                                          ▐█\r\n█▌   $$\\       $$\\           $$\\                                 $$$$$$$\\                      $$\\          ▐█\r\n█▌   $$ |      \\__|          $$ |                                $$  __$$\\                     $$ |         ▐█\r\n█▌   $$ |      $$\\  $$$$$$$\\ $$ |  $$\\  $$$$$$\\ $$\\    $$\\       $$ |  $$ | $$$$$$\\  $$$$$$$\\  $$ |  $$\\    ▐█\r\n█▌   $$ |      $$ |$$  _____|$$ | $$  |$$  __$$\\\\$$\\  $$  |      $$$$$$$\\ | \\____$$\\ $$  __$$\\ $$ | $$  |   ▐█\r\n█▌   $$ |      $$ |\\$$$$$$\\  $$$$$$  / $$ /  $$ |\\$$\\$$  /       $$  __$$\\  $$$$$$$ |$$ |  $$ |$$$$$$  /    ▐█\r\n█▌   $$ |      $$ | \\____$$\\ $$  _$$<  $$ |  $$ | \\$$$  /        $$ |  $$ |$$  __$$ |$$ |  $$ |$$  _$$<     ▐█\r\n█▌   $$$$$$$$\\ $$ |$$$$$$$  |$$ | \\$$\\ \\$$$$$$  |  \\$  /         $$$$$$$  |\\$$$$$$$ |$$ |  $$ |$$ | \\$$\\    ▐█\r\n█▌   \\________|\\__|\\_______/ \\__|  \\__| \\______/    \\_/          \\_______/  \\_______|\\__|  \\__|\\__|  \\__|   ▐█\r\n█▌                                                                                                          ▐█\r\n██████████████████████████████████████████████████████████████████████████████████████████████████████████████");
        }
    }
}
