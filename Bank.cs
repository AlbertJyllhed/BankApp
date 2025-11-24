using BankApp.Users;

namespace BankApp
{
    internal class Bank
    {
        User? activeUser;

        // Loops the application until user logs out
        internal void Run()
        {
            Data.Setup();
            LogIn();
            //Console.Clear();

            while (activeUser != null)
            {
                CreateMenu();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        // Log in method with 3 attempts
        private void LogIn()
        {
            int maxAttempts = 3, attempts = 0;

            // Loop until the user is logged in or max attempts reached
            while (activeUser == null)
            {
                Console.WriteLine("Please enter username.");
                var username = InputUtilities.GetString();
                Console.WriteLine("Please enter password");
                var password = InputUtilities.GetString();

                // Check if the username and password are correct
                var user = Data.GetUser(username);
                if (user != null && user.Password == password)
                {
                    activeUser = user;
                }
                else
                {

                    activeUser = null;
                    attempts++;
                    Console.WriteLine($"Wrong username or password\n" +
                        $"Attempts left {maxAttempts - attempts}");
                }
            }
            if (attempts >= maxAttempts)
            {
                LockUser(activeUser);
            }
        }

        private void LockUser(User? user)
        {
            if(user != null && user is Customer)
            {
                var customer = user as Customer;
                customer.TryLogin();
            }
        }

        private bool SetActiveUser(User? user)
        {
            if (user != null && user is Customer)
            {
                var customer = user as Customer;
                return customer.IsLocked();
            }
            return false;
        }

        // Log out method
        internal void LogOut()
        {
            Console.WriteLine("You have been logged out.");
            activeUser = null;

            Console.WriteLine("Do you want to exit the application? y/n");
            bool answer = InputUtilities.GetYesOrNo();
            if (!answer)
            {
                LogIn();
            }
        }

        // Method to create menu based on user type
        private void CreateMenu()
        {
            var menu = new Menu();
            menu.PrintTitle();
            Console.WriteLine("--- Welcome to Liskov Bank ---");

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
