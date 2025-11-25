namespace BankApp
{
    internal static class InputUtilities
    {
        //Method to get string input from user
        internal static string GetString()
        {
            string? input = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(input))
            {
                PrintUtilities.PrintError("Input cannot be empty. Please try again.");
                input = Console.ReadLine();
            }
            return input;
        }

        //Method to get decimal input from the user
        internal static decimal GetDecimal()
        {
            decimal input;
            while (!decimal.TryParse(Console.ReadLine(), out input))
            {
                PrintUtilities.PrintError("Invald input. Please try again.");
            }
            return Math.Round(input, 2);
        }

        //Method to get positive decimal input from the user
        internal static decimal GetPositiveDecimal()
        {
            decimal input = GetDecimal();
            while (input <= 0)
            {
                input = GetDecimal();
                PrintUtilities.PrintError("Felaktit värde, värdet måste vara positivt");
            }
            return input;
        }

        //Method to get int input from the user
        internal static int GetInt()
        {
            int input;
            while (!int.TryParse(Console.ReadLine(), out input))
            {
                PrintUtilities.PrintError("Invald input. Please try again.");
            }
            return input;

        }

        //Method to get int input within a specific range
        internal static int GetIndex(int maxIndex)
        {
            int index = GetInt() - 1;
            while (index < 0 || index >= maxIndex)
            {
                PrintUtilities.PrintError("Invalid index. Please try again.");
                index = GetInt() - 1;
            }
            return index;
        }

        internal static bool GetYesOrNo()
        {
            string input = GetString().ToLower();
            while (input != "y" && input != "n")
            {
                PrintUtilities.PrintError("Invalid input. Please enter 'y' for yes or 'n' for no.");
                input = GetString().ToLower();
            }
            return input == "y";
        }
    }
}
