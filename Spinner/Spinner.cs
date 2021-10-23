using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    public class Spinner : ISpinner
    {
        IValidation _validator;
        IPlayer _player;

        public Spinner(IValidation validator, IPlayer player) 
        {
            _validator = validator;
            _player = player;
        }

        public int Spin(IPlayer player, int rollLimitExcl)
        {
            Random randomGenerator = new Random();
            int roll = randomGenerator.Next(1, rollLimitExcl);

            if (player.position == 0 && roll < 3)
            {
                Console.WriteLine("Spinned a {0}. Spin a 3 or higher to move from start.", roll);
                roll = -1;
            }

            return roll;
        }

        public bool SpinToWin(int rollLimitExcl)
        {
            Random randomGenerator = new Random();
            Console.WriteLine("Enter a number then spin. If you match it you win");
            int guess = _validator.ValidateGuess(Console.ReadLine());
            int spin = randomGenerator.Next(1, rollLimitExcl);

            Console.WriteLine("You rolled a {0}..", spin);

            return guess == spin;

        }      

        public int WeddingGiftSpin(int rollLimitExcl)
        {
            Random randomGenerator = new Random();
            int roll = randomGenerator.Next(1, rollLimitExcl);
            int giftContribution = 0;

            if (roll % 2 == 0)
            {
                Console.WriteLine("You spinned Black and get $5,000 from each player");
                giftContribution = 5000;
            }
            else
            {
                Console.WriteLine("You spinned Red and get $2,500 from each player");
                giftContribution = 2500;
            }

            return giftContribution;
        }

        public IPlayer SelectorToss(List<IPlayer> players, int rollLimitExcl)
        {
            Console.WriteLine("The computer will now spin for all players to choose the first player");

            int largestNumber = 0;
            Random randomGenerator = new Random();
            IPlayer first = null;

            for (int i = 0; i < players.Count(); i++)
            {
                int spin = randomGenerator.Next(1, rollLimitExcl);

                if (spin > largestNumber)
                {
                    largestNumber = spin;
                    first = players.ElementAt(i);
                }

                Console.WriteLine("{0} rolled a {1}", players.ElementAt(i).name, spin);
            }

            Console.WriteLine("{0} gets to go first!", first.name);
            _player = first;

            return first;
        }

    }
}
