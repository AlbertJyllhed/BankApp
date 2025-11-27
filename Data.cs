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
            // Setup user list
            users = new List<User>()
            {
                new Admin("Admin", "a1"),
                new Customer("User1", "u1"),
                new Customer("User2", "u2")
            };

            int counter = 1;

            // Loop through all users
            foreach (var user in users)
            {
                // If the user is a customer, create two bank accounts for them
                if (user is Customer customer)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        // Create ID from counter and pad to 5 digits
                        string id = PadID(counter);

                        // Create bank account for customer
                        var bankAccount = customer.CreateBankAccount($"{customer.Name} Account {i + 1}", "SEK", id);
                        bankAccount.AddBalance(100m);
                        counter++;
                    }
                }
            }
        }

        // Adds a user to the list of all users if the name is unique
        internal static void AddUser(User newUser)
        {
            foreach (var user in users)
            {
                if (newUser.Name == user.Name)
                {
                    throw new Exception("Användare med samma namn finns redan.");
                }
            }

            users.Add(newUser);
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

        // Returns a list of all locked customers
        internal static List<Customer> GetLockedCustomers()
        {
            List<Customer> lockedCustomers = new List<Customer>();
            foreach (var user in users)
            {
                if (user is Customer customer && customer.Locked)
                {
                    lockedCustomers.Add(customer);
                }
            }
            return lockedCustomers;
        }

        // Adds a bank account to the list of all bank accounts
        internal static void AddBankAccount(BankAccount account)
        {
            var existingAccount = GetBankAccount(account.ID);

            // Check if an account with the same ID already exists
            // If it does, throw an exception
            if (existingAccount != null)
            {
                throw new Exception("Bankkonto med samma ID finns redan.");
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
        internal static KeyValuePair<string, decimal> ChooseCurrency()
        {
            // Ask user to choose currency code by typing in number. 
            Console.WriteLine("Välj valuta: ");
            int count = 1;
            foreach (var item in currency)
            {
                Console.WriteLine($"{count}. {item.Key}");
                count++;
            }
            var index = InputUtilities.GetIndex(currency.Count);

            // Get the currency code from the dictionary based on the chosen index.
            var currencyCode = currency.ElementAt(index);

            return currencyCode;
        }

        // Method to get the exchange rate for a given currency code
        internal static KeyValuePair<string, decimal> GetCurrency(string code)
        {
            if (CurrencyExists(code))
            {
                return new KeyValuePair<string, decimal>(code, currency[code]);
            }
            else
            {
                return new KeyValuePair<string, decimal>("", 0);
            }
        }

        // Method to set the exchange rate for a given currency code
        internal static void SetCurrency(string code, decimal rate)
        {
            if (CurrencyExists(code))
            {
                currency[code] = rate;
            }
        }

        // Method to check if a currency code exists in the dictionary
        private static bool CurrencyExists(string code)
        {
            if (currency.ContainsKey(code))
            {
                return true;
            }
            else
            {
                Console.WriteLine($"{code} är inte en giltig valuta.\n" +
                    $"Vi ber om ursäkt för besväret.");

                return false;
            }
        }
    }
}
