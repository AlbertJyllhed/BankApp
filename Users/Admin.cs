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
            string currency = Data.ChooseCurrency().Key;
            Console.Write($"Type the new exchange rate for {currency}: ");
            Data.SetCurrency(currency, InputUtilities.GetDecimal());
        }

        internal void CreateCustomer()
        {
            Console.WriteLine("Creating new user.");
            Console.Write("Type username: ");
            string name = InputUtilities.GetString();
            Console.Write("Type password: ");
            string password = InputUtilities.GetString();
            var customer = new Customer(name, password);
            Data.AddUser(customer);
            Console.WriteLine($"User {name} created.");
        }
    }
}
