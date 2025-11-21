using BankApp.BankAccounts;
using BankApp.Users;

namespace BankApp
{
    internal static class Data
    {
        private static List<BankAccount> bankAccounts = [];
        private static List<User> users = [];
        private static Dictionary<string, decimal> currency = new Dictionary<string, decimal>()
        {
            ["SEK"] = 1.0m,
            ["DKK"] = 1.54m,
            ["NOK"] = 1.01m,
            ["EUR"] = 11.5m,
            ["GBP"] = 13.0m,
            ["USD"] = 10.8m
        };

        // Setup method to initialize user data
        internal static void Setup()
        {
            users = new List<User>()
            {
                new Admin("Admin", "admin123"),
                new Customer("User", "user123")
            };

            foreach (var user in users)
            {
                if (user is Customer customer)
                {
                    var account = new BankAccount("User1 Account", "SEK");
                    account.AddBalance(500.0m);
                    customer.CreateBankAccount();
                    AddBankAccount(account);
                }
            }
        }

        internal static void AddUser(User user)
        {
            users.Add(user);
        }

        // Loops through all users and returns the user with the matching name
        internal static User? GetUser(string name)
        {
            foreach (var user in users)
            {
                if (user.Name == name)
                {
                    return user;
                }
            }

            return null;
        }

        // Returns the list of all users
        internal static List<User> GetUsers()
        {
            return users;
        }

        // Adds a bank account to the list of all bank accounts
        internal static void AddBankAccount(BankAccount account)
        {
            var existingAccount = GetBankAccount(account.ID);

            // Check if an account with the same ID already exists
            // If it does, throw an exception
            if (existingAccount != null)
            {
                throw new Exception("Bank account with the same ID already exists.");
            }

            bankAccounts.Add(account);
        }

        // Loops through all bank accounts and returns the account with the matching ID
        internal static BankAccount? GetBankAccount(string id)
        {
            foreach (var account in bankAccounts)
            {
                if (account.ID == id)
                {
                    return account;
                }
            }

            return null;
        }

        internal static List<BankAccount> GetBankAccounts()
        {
            return bankAccounts;
        }

        // Checks every bank account and generates a unique ID
        internal static string GetUniqueID()
        {
            var random = new Random();
            int id = random.Next(0, 99999);
            string idString = PadID(id);

            // Not sure we need to check if there are any accounts
            // As far as i know foreach will just skip if there are no items
            foreach (var account in bankAccounts)
            {
                if (account.ID == idString)
                {
                    // This runs the method again if the ID is not unique
                    return GetUniqueID();
                }
            }

            // Method never gets here if the ID is not unique
            // because return ends the method
            return idString;
        }

        // Pads the ID with leading zeros to ensure it is 5 digits long
        private static string PadID(int id)
        {
            string idString = id.ToString();
            for (int i = 0; i < 5; i++)
            {
                if (idString.Length < 5)
                {
                    idString = "0" + idString;
                }
            }
            return idString;
        }

        // Method to get a valid currency from user input
        internal static KeyValuePair<string, decimal> GetCurrency()
        {
            // Ask user to choose currency code by typing in number. 
            Console.WriteLine("Choose currency: ");
            int count = 1;
            foreach (var currencyKey in currency)
            {
                Console.WriteLine($"{count}. {currencyKey}");
                count++;
            }
            var index = Input.GetIndex(currency.Count);

            // Get the currency code from the dictionary based on the chosen index.
            var currencyCode = currency.ElementAt(index);

            return currencyCode;
        }

        // Method to set the exchange rate for a given currency code
        internal static void SetCurrency(string code, decimal rate)
        {
            if (currency.ContainsKey(code))
            {
                currency[code] = rate;
            }
            else
            {
                Console.WriteLine($"{code} is not a valid currency.\n" +
                    $"We apologize for the inconvenience.");
            }
        }
    }
}
