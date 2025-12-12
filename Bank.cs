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
            }
        }

        // Log in method with 3 attempts
        private void LogIn()
        {
            // Loop until a user is logged in
            while (_activeUser == null)
            {
                UI.PrintMessage("--- Logga in ---");
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
                    UI.PrintWarning($"Fel användarnamn eller lösenord", 1);
                    _activeUser = null;
                }
            }

            Console.Clear();
        }

        // Log out method
        internal void LogOut()
        {
            UI.PrintWarning("Du har blivit utloggad.", 1);
            _activeUser = null;

            UI.PrintError("Vill du stänga av applikationen? y/n", 1);
            bool answer = InputUtilities.GetYesOrNo();
            if (!answer)
            {
                Console.Clear();
                LogIn();
            }
        }

        // Method to create menu based on user type
        private void CreateMenu()
        {
            if (_activeUser == null) return;

            var menu = new Menu();
            UI.PrintLogo();
            UI.PrintMessage($"--- Välkommen till Liskov Bank {_activeUser.Name} ---", 1);

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
