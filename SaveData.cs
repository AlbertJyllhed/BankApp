using BankApp.BankAccounts;
using BankApp.Users;

namespace BankApp
{
    internal class SaveData
    {
        public List<User> Users { get; }
        public Dictionary<string, decimal> Currency { get; }

        internal SaveData(List<User> users, Dictionary<string, decimal> currency)
        {
            Users = users;
            Currency = currency;
        }
    }
}
