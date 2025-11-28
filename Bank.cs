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
                UI.PrintResetMessage();
            }
        }

        // Log in method with 3 attempts
        private void LogIn()
        {
            // Loop until a user is logged in
            while (activeUser == null)
            {
                UI.PrintInputPrompt("Vänligen ange användarnamn: ");
                var username = InputUtilities.GetString();
                UI.PrintInputPrompt("Vänligen ange lösenord: ");
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
            UI.PrintColoredMessage("Du har blivit utloggad.", ConsoleColor.Yellow);
            activeUser = null;

            UI.PrintColoredMessage("Vill du stänga av applikationen y/n", ConsoleColor.Red);
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
            UI.PrintLogo();
            UI.PrintMessage($"--- Välkommen till Liskov Bank {activeUser.Name} ---");

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
