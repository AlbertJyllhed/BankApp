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
            // Admin login always succeeds if the password matches
            return Password == password;
        }

        // Method to update currency exchange rates
        internal void UpdateCurrency()
        {
            // Prompt admin to choose a currency to update
            UI.PrintMessage("Vilken valuta vill du uppdatera?", 1);
            string currency = Data.ChooseCurrency(excludeSEK: true).Key;

            // Prompt for new exchange rate and update it in Data
            UI.PrintInputPrompt($"Skriv in den nya växelkursen för {currency}: ");
            Data.SetCurrency(currency, InputUtilities.GetDecimal());

            UI.PrintSuccess($"\nVäxelkursen för {currency} uppdaterad.", 1);
            UI.PrintResetMessage();
        }

        // Method to create a new customer
        internal void CreateCustomer()
        {
            UI.PrintMessage("Skapar ny användare.");

            // Get username and password from input
            UI.PrintInputPrompt("Skriv ett användarnamn till den nya användaren: ");
            string name = InputUtilities.GetString();
            UI.PrintInputPrompt("Skriv ett lösenord till den nya användaren: ");
            string password = InputUtilities.GetString();
           
            // Create new Customer object with provided name and password
            var customer = new Customer(name, password);

            // Add the new customer to Data
            if (Data.AddUser(customer))
            {
                UI.PrintSuccess($"\nAnvändare {name} skapad.", 1);
            }
            else
            {
                UI.PrintError("\nDet finns redan en användare med samma namn.", 1);
            }
            UI.PrintResetMessage();
        }

        // Method to unlock a customer's account
            
        internal void UnlockCustomerAccount()
        {
            // Get list of customers from Data
            var lockedCustomers = Data.GetLockedCustomers();
            if (lockedCustomers.Count == 0)
            {
                UI.PrintWarning("Det finns inga låsta kunder.", 1);
                UI.PrintResetMessage();
                return;
            }

            UI.PrintMessage("Vilken kund vill du låsa upp?", 1);

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
            customer._loginAttempts = 0;
            UI.PrintSuccess($"\nAnvändare: {customer.Name} upplåst.", 1);
            UI.PrintResetMessage();
        }
    }
}
