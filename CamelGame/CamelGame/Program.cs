using System;

namespace CamelGame
{

    static class Constants
    {
        public const int startingDrinks = 10;
        public const int finalDest = 100;
        public const int startingHydration = 10;
        public const int startingCamelEnergy = 25;

        public const int moderateTravelSpeed = 5;
        public const int moderateTravelThirst = 1;
        public const int moderateTravelCamelTired = 2;

        public const int fastTravelThirst = 2;
        public const int fastTravelSpeed = 10;
        public const int fastTravelCamelTired = 5;

    }

    class Program
    {
        static void Main(string[] args)
        {
            // Constants
            int numOfDrinks = Constants.startingDrinks;
            int distanceLeft = Constants.finalDest;
            int hydrationLevel = Constants.startingHydration;
            int camelEnergy = Constants.startingCamelEnergy;
            Random rand = new Random();

            // Introductory message
            Console.WriteLine("Welcome to Camel!");

            // Main game loop
            bool done = false;
            while (!done)
            {
                // Print commands
                Console.WriteLine();
                Console.WriteLine("A. Drink from your canteen.");
                Console.WriteLine("B. Ahead moderate speed.");
                Console.WriteLine("C. Ahead full speed.");
                Console.WriteLine("D. Stop and rest.");
                Console.WriteLine("E. Status check.");
                Console.WriteLine("Q. Quit.");

                // Get user command
                Console.Write("What is your command? ");
                string userCommand = Console.ReadLine();
                Console.WriteLine();

                // Drinking from flask
                if (String.Equals(userCommand, "a", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (numOfDrinks > 0)
                    {
                        Console.WriteLine("You drank from the canteen.");
                        numOfDrinks -= 1;
                        hydrationLevel = Constants.startingHydration;

                        // Also allowing the camel a short rest while u drink
                        camelEnergy += 1;
                    }
                    else
                    {
                        Console.WriteLine("You have no more water in the canteen.");
                    }
                }
                // Travelling Slow
                else if (String.Equals(userCommand, "b", StringComparison.CurrentCultureIgnoreCase))
                {
                    int randomDist = rand.Next(5) + Constants.moderateTravelSpeed;
                    distanceLeft -= randomDist;
                    Console.WriteLine("You have traveled " + randomDist);

                    hydrationLevel -= rand.Next(1) + Constants.moderateTravelThirst;

                    camelEnergy -= rand.Next(3) + Constants.moderateTravelCamelTired;

                }
                // Traveling Fast
                else if (String.Equals(userCommand, "c", StringComparison.CurrentCultureIgnoreCase))
                {
                    int randomDist = rand.Next(5) + Constants.fastTravelSpeed;
                    distanceLeft -= randomDist;
                    Console.WriteLine("You have traveled " + randomDist);

                    hydrationLevel -= rand.Next(3) + Constants.fastTravelThirst;

                    camelEnergy -= rand.Next(5) + Constants.fastTravelCamelTired;
                }
                // Resting the camel
                else if (String.Equals(userCommand, "d", StringComparison.CurrentCultureIgnoreCase))
                {
                    camelEnergy = Constants.startingCamelEnergy;
                    hydrationLevel -= rand.Next(1);
                }
                else if (String.Equals(userCommand, "e", StringComparison.CurrentCultureIgnoreCase))
                {
                    // Printing out their stats
                    Console.WriteLine("Drinks left in the Canteen: " + numOfDrinks);
                    Console.WriteLine("Hydration Level: " + hydrationLevel);
                    Console.WriteLine("Distance left to travel: " + distanceLeft);
                    Console.WriteLine("Camel Energy Levels: " + camelEnergy);
                }
                else if (String.Equals(userCommand, "q", StringComparison.CurrentCultureIgnoreCase))
                {
                    done = true;
                    Console.WriteLine("Congragulations you are a quitter. You will die of shame instead of thirst, smh.");
                }
                // Unknown Command
                else
                {
                    Console.WriteLine("Unknown command.");
                }
                
                // Checking to see if they are dead, or done
                if (hydrationLevel <= 0 || camelEnergy <= 0)
                {
                    String collapseString = "";
                    if (hydrationLevel <= 0) {
                        collapseString = "thirst.";
                    }
                    else if (camelEnergy <= 0)
                    {
                        collapseString = "an exhausted camel.";
                    }
                    Console.WriteLine("You have collapsed in the middle of the desert from " + collapseString);
                    done = true;
                }
                else if (distanceLeft <= 0)
                {
                    Console.WriteLine("You have succesfully escaped with the stolen camel. Good job your a horrible human being.");
                    done = true;
                }

                // Play again option
                if (done)
                {
                    Console.WriteLine("Do you want to play again? (y/n)");
                    userCommand = Console.ReadLine();
                    if (userCommand.Equals("y", StringComparison.CurrentCultureIgnoreCase))
                    {
                        numOfDrinks = Constants.startingDrinks;
                        distanceLeft = Constants.finalDest;
                        hydrationLevel = Constants.startingHydration;
                        camelEnergy = Constants.startingCamelEnergy;
                        done = false;
                    }
                }

                  
            }
        }

    }
}
