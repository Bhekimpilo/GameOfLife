using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    class GameEngine
    {
        List<Player> players = new List<Player>();
        Random randomGenerator = new Random();
        GameBoard board = new GameBoard();
        Player currentPlayer = null;
        
        int currentIndex = 0;
        bool isRunning = true;


        public void Init()
        {            
            AddPlayers(players);
            PlayGame(SelectorToss());
        }

        public void PlayGame(Player firstPlayer)
        {
            currentPlayer = firstPlayer;

            while (isRunning)
            {               
               SelectPlayer(players);
               Console.WriteLine("Current player is {0}. Hit Enter to roll the dice", currentPlayer);

               int roll = randomGenerator.Next(1, 11);

               Move(currentPlayer.position, roll);
                                
            }
            
        }

        public Player SelectorToss()
        {
            int largestNumber = 0;
            Player first = null;

            for (int i = 0; i < players.Count(); i++)
            {
                Random random = new Random();
                int spin = random.Next(1, 11);

                if (spin > largestNumber)
                {
                    largestNumber = spin;
                    first = players.ElementAt(i);
                }
                
            }

            Console.WriteLine("{0} gets to go first!", first.name);

            return first;
        }

        public void Move(int start, int roll)
        {            
            int destination = start + roll;

            if (destination >= board.squares.Count)
                destination = board.squares.Count - 1;
            
            for (int i = start; i < destination; i++)
            {
                if ((int)board.squares.ElementAt(i).actionCode < 5)
                {
                    Console.Write(board.squares.ElementAt(i).description + ". Balance was " + currentPlayer.cash);
                    currentPlayer.cash += board.squares.ElementAt(i).cashAmount;
                    Console.WriteLine(" and now is {0}", currentPlayer.cash);
                }
            }

            Console.WriteLine("{0} rolled a {1} and is moving to {2}", currentPlayer.name, roll, currentPlayer.position + roll);

            UpdatePlayerPosition(destination);
        }
        
            
               

        public void AddPlayers(List<Player> list)
        {
            bool adding = true;
            int count = 1;

            list.Add(new Player("Computer"));

            do
            {
                Console.Write("Please add player name: ");
                string name = Console.ReadLine();

                if(!name.Trim().Equals("") && name != null)
                    list.Add(new Player(name));

                bool tryAgain = true;

                while (tryAgain)
                {
                    if (count < 5)
                        Console.Write("Do you want to add another player? (Y/N): ");
                    else
                    {
                        adding = false;
                        break;
                    }

                    string response = Console.ReadLine().ToLower();
                    tryAgain = false;

                    if (response.Equals("n"))
                        adding = false;
                    else
                        if (!response.Equals("y"))
                        {
                            Console.WriteLine("Invalid input, please enter \'Y\' or \'N\'");
                            tryAgain = true;
                        }
                }

                count++;
                
            } while (adding);

            Console.WriteLine("A total of {0} players were added", count);
            Console.Read();
        }

        public Player SelectPlayer(List<Player> players)
        {
            
            int index = currentIndex + 1;
            
            if (currentIndex < (players.Count - 2))
                currentIndex = index;
            else
                currentIndex = -1;

            Console.WriteLine("Current player is {0}", players.ElementAt(index).name);

            currentPlayer = players.ElementAt(index);

            return currentPlayer;
        }

        private void UpdatePlayerPosition(int destination)
        {
            currentPlayer.position = destination;
        }
    }

    
}
