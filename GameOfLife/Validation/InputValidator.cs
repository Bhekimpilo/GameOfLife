using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    public class InputValidator : IValidation
    {
        public string ValidateResponse(string response)
        {
            bool tryAgain = true;

            do
            {
                if (response.Equals("y") || response.Equals("n"))
                {
                    tryAgain = false;
                }
                else
                {
                    Console.WriteLine("Invalid response, please try again");
                    response = Console.ReadLine();
                }

            } while (tryAgain);

            return response;
        }

        public string ValidateOption(string response)
        {
            bool tryAgain = true;

            do
            {
                if (response.Equals("a") || response.Equals("b"))
                {
                    tryAgain = false;
                }
                else
                {
                    Console.WriteLine("Invalid input, please enter an \'A\' or a \'B\'");
                    response = Console.ReadLine();
                }

            } while (tryAgain);

            return response;
        }

        public bool ValidateEnterKey(ConsoleKeyInfo key)
        {
            while (true)
            {
                if (key.Key.Equals(ConsoleKey.Enter))
                    return true;
                else
                    return false;
            }

        }

        public int ValidateGuess(string guess)
        {
            bool tryAgain = true;
            int numGuess = 0;

            do
            {
                if (!string.IsNullOrEmpty(guess))
                {
                    bool isNumber = int.TryParse(guess, out numGuess);

                    if (isNumber)
                        tryAgain = false;
                    else
                    {
                        Console.WriteLine("Invalid input, please enter a number between 1 & 8");
                        guess = Console.ReadLine();
                    }
                }
                else
                {
                    Console.WriteLine("No input detected. Please enter a number between 1 and 8");
                    guess = Console.ReadLine();
                }


            } while (tryAgain);

            return numGuess;
        }
    }
}
