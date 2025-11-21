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
            string currency = Data.GetCurrency().Key;
            Console.Write($"Type the new exchange rate for {currency}: ");
            Data.SetCurrency(currency, Input.GetDecimal());
        }

        internal void CreateCustomer()
        {
            Console.WriteLine("Creating new user.");
            Console.Write("Type username: ");
            string name = Input.GetString();
            Console.Write("Type password: ");
            string password = Input.GetString();
            var customer = new Customer(name, password);
            Data.AddUser(customer);
            Console.WriteLine($"User {name} created.");
        }
    }
}
