namespace BankApp.Users
{
    internal class Admin : User
    {
        internal Admin(string name, string password) : base(name, password)
        {

        }

        internal void UpdateCurrency()
        {
            Console.WriteLine("Which currency do you want to update?");
            var currency = Data.GetCurrency();
            Console.Write($"Type the new exchange rate for {currency}: ");
            Data.currency[currency] = Input.GetDecimal();
        }

        internal void CreateUser()
        {
            Console.WriteLine("Creating new user.");
            Console.Write("Type username: ");
            var name = Input.GetString();
            Console.Write("Type password: ");
            var password = Input.GetString();
            var user = new User(name, password);
            Data.users.Add(user);
            Console.WriteLine($"User {name} created.");
        }
    }
}
