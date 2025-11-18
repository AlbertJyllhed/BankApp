using BankApp.BankAccounts;

namespace BankApp.Users
{
    internal abstract class User
    {
        internal string Name { get; }
        internal string Password { get; }

        internal User(string name, string password)
        {
            Name = name;
            Password = password;
        }
    }
}
