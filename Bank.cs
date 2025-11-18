using BankApp.Users;
using BankApp.BankAccounts;

namespace BankApp
{
    internal class Bank
    {
        User? activeUser;

        //Will run the program need more here later
        internal void Run()
        {
            LogIn();

            while (activeUser != null)
            {
                Console.WriteLine("-- Welcome to Liskov Bank --");
                CreateMenu();
            }
        }

        // Log in method with 3 attempts
        private bool LogIn()
        {
            int maxAttempts = 3;
            int attempts = 0;

            // Loop until the user is logged in or max attempts reached
            while (attempts < maxAttempts)
            {
                Console.WriteLine("Please enter username.");
                var username = Input.GetString();
                Console.WriteLine("Please enter password");
                var password = Input.GetString();

                // Check if the username and password are correct
                var user = Data.GetUser(username);
                if (user != null && user.Password == password)
                {
                    activeUser = user;
                    return true;
                }
                else
                {
                    activeUser = null;
                    attempts++;
                    Console.WriteLine($"Wrong username or password\n" +
                        $"Attempts left {maxAttempts - attempts}");
                }
            }

            return false;
        }

        // Log out method
        internal void LogOut()
        {
            Console.WriteLine("You have been logged out.");
            activeUser = null;
            LogIn();
        }

        // Method to create menu based on user type
        private void CreateMenu()
        {
            var menu = new Menu();
            menu.PrintTitle();

            if (activeUser != null)
            {
                bool restart = false;
               
                if (activeUser is Customer)
                {
                    var customer = activeUser as Customer;
                    restart = menu.PrintCustomerMenu(customer);
                }
                else if (activeUser is Admin)
                {
                    var admin = activeUser as Admin;
                    restart = menu.PrintAdminMenu(admin);
                }
                if (!restart)
                {
                    LogOut();
                }
            }
        }
    }
}
