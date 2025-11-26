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
            PrintUtilities.PrintMessage("Which currency do you want to update?");
            string currency = Data.ChooseCurrency().Key;

            // Prompt for new exchange rate and update it in Data
            PrintUtilities.PrintInputPrompt($"Type the new exchange rate for {currency}: ");
            Data.SetCurrency(currency, InputUtilities.GetDecimal());
        }

        // Method to create a new customer
        internal void CreateCustomer()
        {
            PrintUtilities.PrintMessage("Creating new user.");

            // Get username and password from input
            PrintUtilities.PrintInputPrompt("Type username: ");
            string name = InputUtilities.GetString();
            PrintUtilities.PrintInputPrompt("Type password: ");
            string password = InputUtilities.GetString();

            // Create new Customer object with provided name and password
            var customer = new Customer(name, password);

            // Add the new customer to Data
            Data.AddUser(customer);
            PrintUtilities.PrintMessage($"User {name} created.");
        }

        // Method to unlock a customer's account
        internal void UnlockCustomerAccount()
        {
            PrintUtilities.PrintMessage("Which customer account do you want to unlock?");

            // Get list of customers from Data
            var lockedCustomers = Data.GetLockedCustomers();

            // Print list of customers with indices
            for (int i = 0; i < lockedCustomers.Count; i++)
            {
                PrintUtilities.PrintMessage($"{i + 1}. {lockedCustomers[i].Name}");
            }

            // Choose customer by index
            int choice = InputUtilities.GetIndex(lockedCustomers.Count);
            var customer = lockedCustomers[choice];

            // Unlock the selected customer's account
            customer.UnlockAccount();
            PrintUtilities.PrintMessage($"Customer {customer.Name} account unlocked.");
        }
    }
}
