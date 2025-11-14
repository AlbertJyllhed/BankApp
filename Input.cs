namespace BankApp
{
    internal static class Input
    {
        //Method to get string input from user
        internal static string GetString()
        {
            string? input = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Invalid input try again!");
                input = Console.ReadLine();
            }

            return input;
        }

        //Method to get decimal input from the user
        internal static decimal GetDecimal()
        {
            decimal input;
            while(decimal.TryParse(Console.ReadLine(), out input))
            {
                Console.WriteLine("Invalid input try again!");
            }
            return input;
        }

        //Method to get int input from the user
        internal static int GetInt()
        {
            int input;
            while (!int.TryParse(Console.ReadLine(), out input))
            {
                Console.WriteLine("Invald input try again!");
            }
            return input;

        }

        //Method to get int input within a specific range
        internal static int GetIndex(int maxIndex)
        {
            int index = GetInt();
            while (index < 0 || index > maxIndex)
            {
                index = GetInt();
            }

            return index;
        }
    }
}
