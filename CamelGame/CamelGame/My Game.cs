using System;

// Use another class to create instances of game variables such as thirst?
// Add oasis and a native who is chasing me

namespace CamelGame
{

    static class Constants
    {
        // Starting values (used for restarting the game)
        public const int startingDrinks = 10;
        public const int finalDest = 100;
        public const int startingHydration = 10;
        public const int startingCamelEnergy = 25;
        public const int nativeStartingDistance = -20;
       
    }

    class Program
    {
        // Native Traveling
        private static int GetNativeTravel ()
        {
            Random rand = new Random();
            int distTraveled = rand.Next(2, 12);

            Console.WriteLine("The native traveled " + distTraveled + " miles.");

            return distTraveled;
        }
        // Convert thirst numbers to human feelings
        private static void ShowThirst (int thirst)
        {
            if (thirst < 2)
            {
                Console.WriteLine("You are on the verge of death");
            }
            else if (thirst < 4)
            {
                Console.WriteLine("You are very thirsty");
            }
            else if (thirst < 7)
            {
                Console.WriteLine("You are getting thirsty");
            }
        }

        // Convert Camel energy to human terms
        private static void ShowCamelEnergy(int camelEnergy) 
        {
            if (camelEnergy < 3)
            {
                Console.WriteLine("Your camel is on the verge of death");
            }
            else if (camelEnergy < 8)
            {
                Console.WriteLine("Your camel is very tired");
            }
            else if (camelEnergy < 13)
            {
                Console.WriteLine("Your camel is getting tired");
            }
        }

        static void Main(string[] args)
        {
            // Constants
            int numOfDrinks = Constants.startingDrinks;
            int distanceLeft = Constants.finalDest;
            int hydrationLevel = Constants.startingHydration;
            int camelEnergy = Constants.startingCamelEnergy;
            int nativeLocation = Constants.nativeStartingDistance;
            int nativeDistTraveled;
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
                string userCommand = Console.ReadLine().ToLower();
                Console.WriteLine();

                // Drinking from flask
                if (userCommand.Equals("a"))
                {
                    if (numOfDrinks > 0)
                    {
                        Console.WriteLine("You drank from the canteen.");
                        numOfDrinks -= 1;
                        hydrationLevel = Constants.startingHydration;

                        // Also allowing the camel a short rest while u drink
                        camelEnergy += 2;

                        nativeDistTraveled = GetNativeTravel();
                        nativeLocation += nativeDistTraveled;
                    }
                    else
                    {
                        Console.WriteLine("You have no more water in the canteen.");
                    }
                }
                // Travelling Slow
                else if (userCommand.Equals("b"))
                {
                    int randomDist = rand.Next(5, 10);
                    distanceLeft -= randomDist;
                    Console.WriteLine("You have traveled " + randomDist + " miles.");

                    hydrationLevel -= rand.Next(1, 3);
                    nativeDistTraveled = GetNativeTravel();
                    nativeLocation += nativeDistTraveled;
                    camelEnergy -= rand.Next(4, 6);

                }
                // Traveling Fast
                else if (userCommand.Equals("c"))
                {
                    int randomDist = rand.Next(15, 20);
                    distanceLeft -= randomDist;
                    Console.WriteLine("You have traveled " + randomDist + " miles.");

                    hydrationLevel -= rand.Next(5, 8);
                    nativeDistTraveled = GetNativeTravel();
                    nativeLocation += nativeDistTraveled;
                    camelEnergy -= rand.Next(5, 10);
                }
                // Resting the camel
                else if (userCommand.Equals("d"))
                {
                    camelEnergy = Constants.startingCamelEnergy;
                    hydrationLevel --;
                    nativeDistTraveled = GetNativeTravel();
                    nativeLocation += nativeDistTraveled;
                }
                // Showing stats
                else if (userCommand.Equals("e"))
                {
                    Console.WriteLine("Drinks left in the Canteen: " + numOfDrinks);
                    Console.WriteLine("Distance left to travel: " + distanceLeft);
                    // Calculated native distance a little weird because i didnt have player location, i only had distance left to the end
                    Console.WriteLine("The native is " + (Constants.finalDest - distanceLeft - nativeLocation) + " miles behind you.");
                }
                else if (userCommand.Equals("q"))
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
                    // Checking if the native has caught up (done a little weird because I am not keeping tack of the player, but instead the distance left)
                    else if (nativeLocation >= Constants.finalDest - distanceLeft)
                    {
                        collapseString = "an angry native catching up to you.";
                    }
                    Console.WriteLine("You have collapsed in the middle of the desert from " + collapseString);
                    done = true;
                }
                else if (distanceLeft <= 0)
                {
                    Console.WriteLine("You have succesfully escaped with the stolen camel. Good job your a horrible human being.");
                    done = true;
                }
                // Showing they are thirsty, and how far the native travelled (if they are and havn't won or died)
                else
                {
                    ShowThirst(hydrationLevel);
                    ShowCamelEnergy(camelEnergy);
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
                        nativeLocation = Constants.nativeStartingDistance;
                        done = false;
                    }
                }

                  
            }
        }

    }
}
