using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamelGame
{
    class Program
    {
        private static readonly int MILES_TO_HIDEOUT = 200;

        private static bool done;
        private static bool win;
        private static bool quit;
        private static int milesTraveled;
        private static int fillupsLeft;
        private static int policeMilesTraveled;
        private static int gasTankLeft;
        private static char userInput;
        private static bool validInput;
        private static readonly Random rand = new Random();
        // what is this readonly thing?
        // Also they have no comments

        static bool FoundOasis(int findingNumber)
        {
            if (findingNumber == 15)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static void Main()
        {
            bool playAgain = true;

            while (playAgain)
            {
                Console.WriteLine("Welcome to Bank Heist!\n" +
                "You have stolen one-million dollars from a bank and must escape to your secret hide out.\n" +
                "The police are hot on your tail and will stop at nothing to catch you!\n" +
                "Out run the cops and escape to your hideout to keep your freedom.\n");

                done = false;
                win = false;
                validInput = true;

                milesTraveled = 0;
                gasTankLeft = 0;
                fillupsLeft = 3;

                policeMilesTraveled = -20;


                while (!done)
                {
                    Console.WriteLine();
                    Console.WriteLine("A. Ahead moderate speed.\n" +
                        "B. Ahead full speed.\n" +
                        "C. Stop to fill up the gas tank.\n" +
                        "D. Status check.\n" +
                        "Q. Quit.\n");

                    Console.Write("What is your choice? ");
                    userInput = Console.ReadKey().KeyChar;
                    Console.WriteLine("\n");

                    validInput = true;

                    // The user chooses to quit the game.
                    if (char.ToUpper(userInput) == 'Q')
                    {
                        QuitGame();
                    }
                    // The user chooses to check their status.
                    else if (char.ToUpper(userInput) == 'D')
                    {
                        CheckStatus();
                    }
                    // The user chooses to hide for the night. (Uh is this comment accurate what the heck)
                    else if (char.ToUpper(userInput) == 'C')
                    {
                        StopToFillGas();
                    }
                    // The user chooses to move ahead full speed.
                    else if (char.ToUpper(userInput) == 'B')
                    {
                        MoveAhead(false);
                    }
                    // The user chooses to move ahead slowly.
                    else if (char.ToUpper(userInput) == 'A')
                    {
                        MoveAhead(true);
                    }
                    // The user input was invalid.
                    else
                    {
                        Console.WriteLine("You input was invalid.");
                        validInput = false;
                    }

                    CheckIfCaught();

                }

                if (win)
                {
                    Console.WriteLine("\nCongratulations! You've escaped the police and won the game!");
                }
                else if (!win && !quit)
                {
                    Console.WriteLine("\nYou have lost the game.");
                }
                else
                {
                    Console.WriteLine("\nThanks for playing.");
                }

                Console.Write("Would you like to play again? (Y/N) ");
                userInput = Console.ReadKey().KeyChar;
                Console.WriteLine("\n");

                if (char.ToUpper(userInput) == 'Y')
                {
                    playAgain = true;
                }
                else if (char.ToUpper(userInput) == 'N')
                {
                    playAgain = false;
                }
                else
                {
                    Console.WriteLine("You have entered an invalid value and the game will now close.\n");
                    playAgain = false;
                }
            }

            Console.WriteLine("Thank you for playing. Press any key to exit.");
            _ = Console.ReadKey();
            // This discard thing is cool
        }

        private static void CheckIfCaught()
        {
            if (char.ToUpper(userInput) != 'Q' && validInput)
            {
                if (gasTankLeft > 8 && !done)
                {
                    Console.WriteLine("Your car ran out of gas and you got caught.");
                    done = true;
                }
                else if (gasTankLeft > 5)
                {
                    Console.WriteLine("Your gas is getting low.");
                }

                if ((milesTraveled - policeMilesTraveled) <= 0 && !done)
                {
                    Console.WriteLine("The police caught you.");
                    done = true;
                }
                else if ((milesTraveled - policeMilesTraveled) <= 15)
                {
                    Console.WriteLine("The police are getting close!");
                }

                if (milesTraveled >= MILES_TO_HIDEOUT && !done)
                {
                    done = true;
                    win = true;
                }
                // Uh They might be able to get caught and win at the same time
            }
        }

        static void QuitGame()
        {
            done = true;
            quit = true;
        }

        static void CheckStatus()
        {
            Console.WriteLine("Miles traveled: " + milesTraveled);
            Console.WriteLine("Gas fill-ups remaining: " + fillupsLeft);
            Console.WriteLine("The police are " + (milesTraveled - policeMilesTraveled) +
                " miles behind you.");
        }

        static void StopToFillGas()
        {
            gasTankLeft = 0;
            fillupsLeft -= 1;
            policeMilesTraveled += rand.Next(7, 15);

            if (policeMilesTraveled < milesTraveled)
            {
                Console.WriteLine("Your gas tank is full.");
            }
        }

        static void MoveAhead(bool slow)
        {
            int currentMilesTraveled;
            if (!slow)
            {
                currentMilesTraveled = rand.Next(10, 21);
                gasTankLeft += rand.Next(1, 4);
            }
            else
            {
                currentMilesTraveled = rand.Next(5, 13);
                gasTankLeft++;
            }
            milesTraveled += currentMilesTraveled;
            policeMilesTraveled += rand.Next(7, 15);
            Console.WriteLine("You traveled " + currentMilesTraveled + " miles.");

            int findingAHideout = rand.Next(19);

            // Should rename finding Oasis to finding hideout or finding escape or something like that
            if (FoundOasis(findingAHideout) && milesTraveled < MILES_TO_HIDEOUT)
            {
                Console.WriteLine("You found an abandoned hideout!");
                fillupsLeft = 3;
                gasTankLeft = 0;
            }
        }

    }
}