using BankApp.BankAccounts;
using BankApp.Users;

namespace BankApp
{
    internal static class Data
    {
        internal static List<BankAccount> bankAccounts = [];
        internal static List<User> users = new List<User>()
        {
            new Admin("Admin", "admin123"),
            new Customer("User", "user123")
        };

        internal static Dictionary<string, decimal> currency = new Dictionary<string, decimal>()
        {
            ["SEK"] = 1.0m,
            ["DKK"] = 0.68m,
            ["NOK"] = 1.07m,
            ["EUR"] = 0.09m,
            ["GBP"] = 0.08m,
            ["USD"] = 0.11m
        };

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

        // Checks every bank account and generates a unique ID
        internal static string GetUniqueID()
        {
            var random = new Random();
            int id = random.Next(0, 99999);
            string idString = id.ToString();

            // Pads the ID with leading zeros to ensure it is 5 digits long
            for (int i = 0; i < 5; i++)
            {
                if (idString.Length < 5)
                {
                    idString = "0" + idString;
                }
            }

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

        // Method to get a valid currency from user input
        internal static string GetCurrency()
        {
            //Ask user to choose currency code by typing in number. 
            Console.WriteLine("Choose currency: ");
            int count = 1;
            foreach (var currencyKey in currency.Keys)
            {
                Console.WriteLine($"{count}. {currencyKey}");
                count++;
            }
            var index = Input.GetIndex(currency.Count);

            var currencyCode = currency.ElementAt(index).Key;

            return currencyCode;
        }
    }
}
