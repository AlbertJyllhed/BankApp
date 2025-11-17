using BankApp.Users;

namespace BankApp
{
    internal class Bank
    {
        User? loggedInUser;

        //Will run the program need more here later
        internal static void Run()
        {
            Console.WriteLine("-- Welcome to Liskov Bank --");


        }

        // Log in method with 3 attempts
        internal void LogIn()
        {
            int maxAttempts = 3;
            int attempts = 0;

            // Loop until the user is logged in or max attempts reached
            while (attempts < maxAttempts)
            {
                Console.WriteLine("Please enter username.")
                var username = Input.GetString();
                Console.WriteLine("Please enter password");
                var password = Input.GetString();

                // Check if the username and password are correct
                var user = Data.GetUser(username)
                if (user != null && user.Password == password)
                {
                    loggedInUser = user;
                }
                else
                {
                    loggedInUser = null;
                    attempts++;
                    Console.WriteLine($"Wrong username or password\n" +
                        $"Attempts left {maxAttempts - attempts}");
                }
            }
        }


        // Log out method
        internal void LogOut()
        {
            Console.WriteLine("You have been logged out.");
            loggedInUser = null;
        }
        
    }
}
