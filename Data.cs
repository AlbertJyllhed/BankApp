using BankApp.BankAccounts;

namespace BankApp
{
    internal static class Data
    {

        
        internal static List<BankAccount> bankAccounts = [];
        internal static Dictionary<string, decimal> currency = new Dictionary<string, decimal>()
        {
            ["SEK"] = 1.0m,
            ["DKK"] = 0.68m,
            ["NOK"] = 1.07m,
            ["EUR"] = 0.09m,
            ["GBP"] = 0.08m,
            ["USD"] = 0.11m
        };

        // Checks every bank account and generates a unique ID
        internal static int GetUniqueID()
        {
            var random = new Random();
            int id = random.Next(00000, 99999);

            // Not sure we need to check if there are any accounts
            // As far as i know foreach will just skip if there are no items
            foreach (var account in bankAccounts)
            {
                if (account.ID == id)
                {
                    // This runs the method again if the ID is not unique
                    return GetUniqueID();
                }
            }

            // Method never gets here if the ID is not unique
            // because return ends the method
            return id;
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
