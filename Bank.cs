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
            bool loggedIn = LogIn();

            while (loggedIn)
            {
                Console.WriteLine("-- Welcome to Liskov Bank --");
                CreateMenu();
            }
        }

        // Log in method with 3 attempts
        internal bool LogIn()
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
        }

        // Method to create menu based on user type
        internal void CreateMenu()
        {
            var menu = new Menu();
            menu.PrintTitle();

            if (activeUser != null)
            {
                if (activeUser is Customer)
                {
                    var customer = activeUser as Customer;
                    menu.PrintCustomerMenu(customer);
                }
                else if (activeUser is Admin)
                {
                    var admin = activeUser as Admin;
                    menu.PrintAdminMenu(admin);
                }
            }
        }
    }
}
