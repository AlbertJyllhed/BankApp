using BankApp.BankAccounts;

namespace BankApp.Users
{
    internal class User
    {
        internal string Name { get; set; }
        internal string Password { get; set; }

        internal User(string name, string password)
        {
            Name = name;
            Password = password;
        }
    }
}
