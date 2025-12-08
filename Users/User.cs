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

        // Abstract method to attempt login for user
        internal abstract bool TryLogin(string password);
    }
}
