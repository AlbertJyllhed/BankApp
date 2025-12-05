namespace BankApp.Users
{
    internal class Admin : User
    {
        internal Admin(string name, string password) : base(name, password)
        {

        }

        // Override TryLogin method for Admin
        internal override bool TryLogin(string password)
        {
            if (Password == password)
            {
                // Admin login always succeeds if the password matches
                return true;
            }
            else
            {
                UI.PrintError("Fel användarnamn eller lösenord");
                return false;
            }
        }

        // Method to update currency exchange rates
        internal void UpdateCurrency()
        {
            // Prompt admin to choose a currency to update
            UI.PrintMessage("Vilken valuta vill du uppdatera?");
            string currency = Data.ChooseCurrency().Key;

            // Prompt for new exchange rate and update it in Data
            UI.PrintInputPrompt($"Skriv in den nya växelkurden för {currency}: ");
            Data.SetCurrency(currency, InputUtilities.GetDecimal());
        }

        // Method to create a new customer
        internal void CreateCustomer()
        {
            UI.PrintMessage("Skapar ny användare.");

            // Get username and password from input
            UI.PrintInputPrompt("Skriv in användarnamn: ");
            string name = InputUtilities.GetString();
            UI.PrintInputPrompt("Skriv in lösenord: ");
            string password = InputUtilities.GetString();
           
            // Create new Customer object with provided name and password
            var customer = new Customer(name, password);

            // Add the new customer to Data
            Data.AddUser(customer);
        }

        // Method to unlock a customer's account
        internal void UnlockCustomerAccount()
        {

            UI.PrintMessage("Vilken kund profil vill du låsa upp?");

            // Get list of customers from Data
            var lockedCustomers = Data.GetLockedCustomers();
            if (lockedCustomers.Count == 0)
            {
                UI.PrintColoredMessage("Ingen låst kund profil", ConsoleColor.Yellow);
                return;
            }
            // Print list of customers with indices
            for (int i = 0; i < lockedCustomers.Count; i++)
            {
                UI.PrintMessage($"{i + 1}. {lockedCustomers[i].Name}");
            }

            // Choose customer by index
            int choice = InputUtilities.GetIndex(lockedCustomers.Count);
            var customer = lockedCustomers[choice];

            // Unlock the selected customer's account
            customer.Locked = false;
            UI.PrintMessage($"Användare {customer.Name} profil har blivit upplåst.");
        }
    }
}
