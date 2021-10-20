using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    public class Player : IPlayer
    {
        public string name { get; set; }
        public int position { get; set; }
        public int salary { get; set; }
        public int cash { get; set; }
        public int bankLoan { get; set; }
        public int house { get; set; }
        public int stock { get; set; }
        public int kids { get; set; }
        public int insurance { get; set; }
        public bool isCareer { get; set; }
        public bool isCollege { get; set; }
        public bool isMarried { get; set; }
        public bool isBridgeOwner { get; set; }
        public bool isBlackListed { get; set; }
        public bool isRetired { get; set; }

        public Player()
        {
            this.position = 0;
            this.insurance = 0;
            this.salary = 0;
            this.cash = 10000;
            this.bankLoan = 0;
            this.house = 0;
            this.stock = 0;
            this.kids = 0;
            this.isMarried = false;
            this.isBlackListed = false;
            this.isRetired = false;

        }

        public void AddPlayers(List<IPlayer> players)
        {
            bool adding = true;

            do
            {
                Console.Write("Please add player name: ");
                string playerName = Console.ReadLine();

                if (!string.IsNullOrEmpty(playerName) && playerName.Length >= 4)
                {
                    if (!players.Where(m => m.name.Equals(playerName)).Any())
                        players.Add(new Player() { name = playerName });
                    else
                    {
                        Console.WriteLine("A player with a similar name already exists. Enter a different one");
                        continue;
                    }

                }
                else
                {
                    Console.WriteLine("Please enter a name with at least 4 characters");
                    continue;
                }

                if (players.Count < 2)
                    continue;


                bool addAgain = true;

                while (addAgain)
                {
                    if (players.Count < 6)
                        Console.Write("Do you want to add another player? (Y/N): ");
                    else
                    {
                        adding = false;
                        break;
                    }

                    string response = Console.ReadLine().ToLower();
                    addAgain = false;

                    if (response.Equals("n"))
                        adding = false;
                    else
                        if (!response.Equals("y"))
                        {
                            Console.WriteLine("Invalid input, please enter \'Y\' or \'N\'");
                            addAgain = true;
                        }
                }


            } while (adding);

            Console.WriteLine("\nA total of {0} players were added:", players.Count);

            foreach (var player in players)
            {
                Console.WriteLine(player.name);
            }
            Console.WriteLine("\nEach player gets $10,000. Good Luck!\n");

        }
    }
}
