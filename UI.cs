using System.Reflection;

namespace BankApp
{
    internal static class UI
    {
        // Method to print a single message
        internal static void PrintMessage(string message, int padding = 0)
        {
            Console.WriteLine(message);

            // Print padding lines after message if specified
            if (padding <= 0) return;

            for (int i = 0; i < padding; i++)
            {
                Console.WriteLine();
            }
        }

        // Method to print a colored message
        internal static void PrintColoredMessage(string message, ConsoleColor color, int padding = 0)
        {
            Console.ForegroundColor = color;
            PrintMessage(message, padding);
            Console.ResetColor();
        }

        // Method to print an input prompt
        internal static void PrintInputPrompt(string prompt)
        {
            Console.Write(prompt);
        }

        // Method to print a list of items with ToString method
        internal static void PrintList<T>(IEnumerable<T> items, bool withIndex = false)
        {
            int counter = 1;

            foreach (var item in items)
            {
                // Skip null items
                if (item == null) continue;

                // Get the type of the item
                var type = item.GetType();

                // Check if the item has a ToString method
                if (type.GetMethod("ToString") != null)
                {
                    // Print index if required
                    if (withIndex)
                    {
                        Console.Write($"{counter}. ");
                    }

                    // Print the string representation of the item
                    Console.WriteLine(item.ToString());
                    counter++;
                }
            }
            Console.WriteLine();
        }

        // Method to print error messages in red color
        internal static void PrintError(string errorMessage, int padding = 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            PrintMessage(errorMessage, padding);
            Console.ResetColor();
        }

        // Method to print a separator line
        internal static void PrintLine()
        {
            Console.WriteLine("--------------------------------------------------");
        }

        // Method to print reset message and wait for user input
        internal static void PrintResetMessage()
        {
            Console.WriteLine("Tryck på valfri knapp för att forsätta...");
            Console.ReadKey();
            Console.Clear();
        }

        // Method to print the bank logo
        internal static void PrintLogo()
        {
            Console.WriteLine("███████████████████████████████████████████████████████████████████████" +
                "███████████████████████████████████████\r\n█▌                                        " +
                "                                                                  ▐█\r\n█▌   $$\\    " +
                "   $$\\           $$\\                                 $$$$$$$\\                     " +
                " $$\\          ▐█\r\n█▌   $$ |      \\__|          $$ |                              " +
                "  $$  __$$\\                     $$ |         ▐█\r\n█▌   $$ |      $$\\  $$$$$$$\\ $$" +
                " |  $$\\  $$$$$$\\ $$\\    $$\\       $$ |  $$ | $$$$$$\\  $$$$$$$\\  $$ |  $$\\    ▐" +
                "█\r\n█▌   $$ |      $$ |$$  _____|$$ | $$  |$$  __$$\\\\$$\\  $$  |      $$$$$$$\\ | " +
                "\\____$$\\ $$  __$$\\ $$ | $$  |   ▐█\r\n█▌   $$ |      $$ |\\$$$$$$\\  $$$$$$  / $$ " +
                "/  $$ |\\$$\\$$  /       $$  __$$\\  $$$$$$$ |$$ |  $$ |$$$$$$  /    ▐█\r\n█▌   $$ | " +
                "     $$ | \\____$$\\ $$  _$$<  $$ |  $$ | \\$$$  /        $$ |  $$ |$$  __$$ |$$ |  $" +
                "$ |$$  _$$<     ▐█\r\n█▌   $$$$$$$$\\ $$ |$$$$$$$  |$$ | \\$$\\ \\$$$$$$  |  \\$  /  " +
                "       $$$$$$$  |\\$$$$$$$ |$$ |  $$ |$$ | \\$$\\    ▐█\r\n█▌   \\________|\\__|\\___" +
                "____/ \\__|  \\__| \\______/    \\_/          \\_______/  \\_______|\\__|  \\__|\\__|" +
                "  \\__|   ▐█\r\n█▌                                                                   " +
                "                                       ▐█\r\n████████████████████████████████████████" +
                "██████████████████████████████████████████████████████████████████████\n");
        }
    }
}
