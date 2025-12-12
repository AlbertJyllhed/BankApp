using BankApp.Users;

namespace BankApp
{
    internal class SaveData
    {
        public List<User> Users { get; set; } = [];
        public Dictionary<string, decimal> Currency { get; set; } = new Dictionary<string, decimal>();
    }
}
