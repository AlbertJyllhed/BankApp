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
            Console.WriteLine("Which currency do you want to update?");
            string currency = Data.ChooseCurrency().Key;

            // Prompt for new exchange rate and update it in Data
            Console.Write($"Type the new exchange rate for {currency}: ");
            Data.SetCurrency(currency, InputUtilities.GetDecimal());
        }

        // Method to create a new customer
        internal void CreateCustomer()
        {
            Console.WriteLine("Creating new user.");

            // Get username and password from input
            Console.Write("Type username: ");
            string name = InputUtilities.GetString();
            Console.Write("Type password: ");
            string password = InputUtilities.GetString();

            // Create new Customer object with provided name and password
            var customer = new Customer(name, password);

            // Add the new customer to Data
            Data.AddUser(customer);
            Console.WriteLine($"User {name} created.");
        }

        // Method to unlock a customer's account
        internal void UnlockCustomerAccount()
        {
            Console.WriteLine("Which customer account do you want to unlock?");

            // Get list of customers from Data
            var customers = Data.GetCustomers();

            // Print list of customers with indices
            for (int i = 0; i < customers.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {customers[i].Name}");
            }

            // Choose customer by index
            int choice = InputUtilities.GetIndex(customers.Count);
            var customer = customers[choice];

            // Unlock the selected customer's account
            customer.UnlockAccount();
            Console.WriteLine($"Customer {customer.Name} account unlocked.");
        }
    }
}
