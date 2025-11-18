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

        internal void CreateCustomer()
        {
            Console.WriteLine("Creating new user.");
            Console.Write("Type username: ");
            var name = Input.GetString();
            Console.Write("Type password: ");
            var password = Input.GetString();
            var customer = new Customer(name, password);
            Data.users.Add(customer);
            Console.WriteLine($"User {name} created.");
        }
    }
}
