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

            while (activeUser != null)
            {
                CreateMenu();
                PrintUtilities.PrintResetMessage();
            }
        }

        // Log in method with 3 attempts
        private void LogIn()
        {
            // Loop until a user is logged in
            while (activeUser == null)
            {
                PrintUtilities.PrintInputPrompt("Please enter username: ");
                var username = InputUtilities.GetString();
                PrintUtilities.PrintInputPrompt("Please enter password: ");
                var password = InputUtilities.GetString();

                // Check if the username and password are correct
                var user = Data.GetUser(username);
                if (user != null && user.TryLogin(password))
                {
                    activeUser = user;
                }
                else
                {
                    activeUser = null;
                }
            }

            Console.Clear();
        }

        // Log out method
        internal void LogOut()
        {
            PrintUtilities.PrintColoredMessage("You have been logged out.", ConsoleColor.Yellow);
            activeUser = null;

            PrintUtilities.PrintColoredMessage("Do you want to exit the application? y/n", ConsoleColor.Red);
            bool answer = InputUtilities.GetYesOrNo();
            if (!answer)
            {
                LogIn();
            }
        }

        // Method to create menu based on user type
        private void CreateMenu()
        {
            if (activeUser == null) return;

            var menu = new Menu();
            PrintUtilities.PrintLogo();
            PrintUtilities.PrintMessage($"--- Welcome to Liskov Bank {activeUser.Name} ---");

            bool restart = false;

            // Print menu based on user type
            if (activeUser is Customer customer)
            {
                restart = menu.PrintCustomerMenu(customer);
            }
            else if (activeUser is Admin admin)
            {
                restart = menu.PrintAdminMenu(admin);
            }

            // Log out if menu returns false
            if (!restart)
            {
                LogOut();
            }
        }
    }
}
