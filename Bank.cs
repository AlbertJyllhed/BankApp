using BankApp.Users;

namespace BankApp
{
    internal class Bank
    {
        private User? _activeUser;

        // Loops the application until user logs out
        internal void Run()
        {
            Data.Setup();
            LogIn();

            while (_activeUser != null)
            {
                CreateMenu();
                UI.PrintResetMessage();
            }
        }

        // Log in method with 3 attempts
        private void LogIn()
        {
            // Loop until a user is logged in
            while (_activeUser == null)
            {
                UI.PrintInputPrompt("Vänligen ange användarnamn: ");
                var username = InputUtilities.GetString();
                UI.PrintInputPrompt("Vänligen ange lösenord: ");
                var password = InputUtilities.GetString();

                // Check if the username and password are correct
                var user = Data.GetUser(username);
                if (user != null && user.TryLogin(password))
                {
                    _activeUser = user;
                }
                else
                {
                    UI.PrintColoredMessage($"Fel användarnamn eller lösenord", ConsoleColor.Yellow, 1);
                    _activeUser = null;
                }
            }

            Console.Clear();
        }

        // Log out method
        internal void LogOut()
        {
            UI.PrintColoredMessage("Du har blivit utloggad.", ConsoleColor.Yellow);
            _activeUser = null;

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
            if (_activeUser == null) return;

            var menu = new Menu();
            UI.PrintLogo();
            UI.PrintMessage($"--- Välkommen till Liskov Bank {_activeUser.Name} ---");

            bool restart = false;

            // Print menu based on user type
            if (_activeUser is Customer customer)
            {
                restart = menu.PrintCustomerMenu(customer);
            }
            else if (_activeUser is Admin admin)
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
