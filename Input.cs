namespace BankApp
{
    internal class Input
    {
        //Method to get string input from user
        internal string GetString()
        {
            string input = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Invalid input try again!");
                input = Console.ReadLine();
            }

            return input;
        }

        //Method to get decimal input from the user
        internal decimal GetDecimal()
        {
            decimal input;
            while(decimal.TryParse(Console.ReadLine(), out input))
            {
                Console.WriteLine("Invalid input try again!");
            }
            return input;
        }
        
    }
}
