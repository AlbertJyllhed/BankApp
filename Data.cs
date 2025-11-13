using BankApp.BankAccounts;

namespace BankApp
{
    internal static class Data
    {
        internal static List<BankAccount> bankAccounts = [];

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
    }
}
