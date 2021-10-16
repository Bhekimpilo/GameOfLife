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
        Bridge bridge = new Bridge();

        Player currentPlayer = null;
        
        int currentIndex = 0;
        int rollLimitExcl = 9;
        //bool isRunning = true;
        bool stopAndSpin = false;


        public void Init()
        {
            Console.WriteLine("Welcome. Add two to six players to get started.");

            AddPlayers(players);
            SelectorToss();
            
            while(players.Where(p => p.isRetired == false).Any())
                PlayGame(currentPlayer);
        }

        public void PlayGame(Player player)
        {
            currentPlayer = player;

            Console.WriteLine("\nCurrent player is {0}. Hit Enter to roll the dice", currentPlayer.name);
            Console.ReadKey();

            if (currentPlayer.position == board.tiles.Count - 1)
                SpinToWin();
            else 
            {
                int roll = Spin();

                if(roll != -1)
                    Move(currentPlayer.position, roll);
            }

            Console.WriteLine();

            if (players.Any(p => p.isRetired == false))
                SelectPlayer();
            else
                DeclareVictor();
        }        

        private int Spin()
        {
            int roll = randomGenerator.Next(1, rollLimitExcl);

            if (currentPlayer.position == 0 && roll < 3)
            {
                Console.WriteLine("Spin a 3 or higher to move from start.");
                roll = -1;
            }                

            return roll;
        }        

        private Player SelectorToss()
        {
            Console.WriteLine("The computer will now spin for all players to choose the first player");

            int largestNumber = 0;
            Player first = null;

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
            currentPlayer = first;

            return first;
        }

        private void Move(int start, int roll)
        {            
            int destination = start + roll;
            Tile currentTile;

            if (destination >= board.tiles.Count)
                destination = board.tiles.Count - 1;

           
            Console.WriteLine("{0} moves {1} steps from {2}...", currentPlayer.name, roll, currentPlayer.position);
            
            for (int i = start + 1; i < destination; i++)
            {
                currentTile = board.tiles.ElementAt(i);
                 
                if (currentPlayer.isCareer && (i == 6 || start == 6))
                {
                    i += 11;
                    destination += 11;
                    currentTile = board.tiles.ElementAt(i);
                    currentPlayer.isCareer = false;

                    if (start != 6)
                        continue;
                }
                              

                if (currentTile.colour == Colour.AMBER)
                {

                    Console.WriteLine("[{0}] {1}", i, currentTile.description);

                    string response = Console.ReadLine().ToLower().Trim();
                    

                    if (currentTile.actionCode == ActionCodes.INSURANCE)
                    {
                        response = ValidateResponse(response);

                        if (response.Equals("y"))
                        {
                            currentPlayer.cash -= currentTile.amount;
                            currentPlayer.insurance += currentTile.amount;
                            Console.WriteLine("[{0}] Wise choice buying insurance. New balance is {1}", i, currentPlayer.cash);

                        }
                        else
                            Console.WriteLine("[{0}] You are not covered in case of disaster", i);
                    }

                    else
                    {
                        response = ValidateOption(response);

                        if (response.Equals("a"))
                        {

                            currentPlayer.isCareer = true;

                            int dest = 0;

                            if (destination <= 6)
                                dest = destination;
                            else
                                dest = destination + 11;

                            Console.WriteLine("Chose to start a carrer and moves to {0}", dest);
                        }
                        else if (response.Equals("b"))
                        {
                            i += 4;
                            destination += 4;
                            currentPlayer.isCareer = false;
                            Console.WriteLine("Chose to go to college and moves to {0}", destination);
                            continue;
                        }
                    }
                    
                }
                else
                {
                    ColourFilter(currentTile, i);
                }

                if (stopAndSpin)
                {
                    destination = board.tiles.IndexOf(currentTile);
                    stopAndSpin = false;
                    break;
                }

            }

            currentTile = board.tiles.ElementAt(destination);

            destination = ActionFilter(currentTile, destination);
            
            UpdatePlayerPosition(destination);
        }
        
        private void AddPlayers(List<Player> list)
        {
            int count = players.Count;
            bool adding = true;
            
            do
            {
                Console.Write("Please add player name: ");
                string name = Console.ReadLine();                

                if (!string.IsNullOrEmpty(name) && name.Length >= 4)
                {
                    if (!players.Where(m => m.name.Equals(name)).Any())
                    {
                         list.Add(new Player(name));
                        count++;
                    }
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

                if (count < 2)
                    continue;
                

                bool addAgain = true;                

                while (addAgain)
                {
                    if (count < 6)
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

            Console.WriteLine("\nA total of {0} players were added:", count);

            foreach (var player in players)
            {
                Console.WriteLine(player.name);
            }
            Console.WriteLine("\nEach player gets $10,000. Good Luck!\n" );
        }

        private Player SelectPlayer()
        {
            currentIndex = players.IndexOf(currentPlayer);
            
            if (currentIndex == players.Count - 1)
                currentIndex = 0;
            else
                currentIndex++;

            currentPlayer = players.ElementAt(currentIndex);

            if (currentPlayer.isBlackListed || currentPlayer.isRetired)
            {
                if (currentPlayer.isBlackListed)
                {
                    Console.WriteLine("{0} missed a turn", currentPlayer.name);
                    currentPlayer.isBlackListed = false;
                }
                
                currentPlayer = SelectPlayer();
            }

            return currentPlayer;
        }

        private int ActionFilter(Tile currentTile, int index)
        {
            int destination = index;

            switch (currentTile.actionCode)
            {                
                case ActionCodes.PAYDAY:
                    currentPlayer.cash += currentPlayer.salary + 10000;
                    Console.WriteLine("[{0}] {1} plus $10,000 bonus. New balance is {2}", index, currentTile.description, currentPlayer.cash);
                    break;
              
                case ActionCodes.SALARY:
                    currentPlayer.salary = currentTile.amount;
                    if(!currentPlayer.isCareer)
                        destination = 17;
                    Console.WriteLine("[{0}] {1}", index, currentTile.description);
                    break;

                case ActionCodes.ADD_KIDS:
                    Console.WriteLine("[{0}] {1}", index, currentTile.description);
                    currentPlayer.kids += currentTile.amount;
                    break;

                case ActionCodes.HOUSE:
                    Console.WriteLine("[{0}] {1}", index, currentTile.description);
                    string buyHouse = Console.ReadLine();
                    buyHouse = ValidateResponse(buyHouse.ToLower().Trim());

                    if (buyHouse.Equals("y"))
                    {
                        currentPlayer.house += currentTile.amount;
                        Pay(currentPlayer, currentTile.amount);
                        Console.WriteLine("Congrats on buying a house. Your new balance is ${0}", currentPlayer.cash);
                    }else
                        Console.WriteLine("Ok then Hobo, keep going...");

                    break;
                
                case ActionCodes.STOCK:
                    Console.WriteLine("[{0}] {1}", index, currentTile.description);
                    string buyStock = Console.ReadLine();
                    buyStock = ValidateResponse(buyStock.ToLower().Trim());

                    if (buyStock.Equals("y"))
                    {
                        currentPlayer.stock += currentTile.amount;
                        Pay(currentPlayer, currentTile.amount);
                        Console.WriteLine("Good choice, you now have stock worth {0}. Balance is {1}", currentPlayer.stock, currentPlayer.cash);
                    }else
                        Console.WriteLine("The market is really volatile...");
                    break;

                case ActionCodes.BLACKLIST:
                    currentPlayer.isBlackListed = true;
                    Console.WriteLine("[{0}] {1}", index, currentTile.description);
                    break;

                case ActionCodes.BRIDGE:    
                    BridgeToll(currentTile, index);
                    break;
                
                case ActionCodes.RESET:
                    destination = 0;
                    currentPlayer.insurance = 0;
                    currentPlayer.house = 0;
                    currentPlayer.stock = 0;
                    currentPlayer.cash = 5000;
                    if (currentPlayer == bridge.owner)
                    {
                        bridge.owner = null;
                    }
                    Console.WriteLine("[{0}] {1}", index, currentTile.description);
                    break;

                case ActionCodes.GET_MARRIED:
                    currentPlayer.isMarried = true;
                    Console.WriteLine("[{0}] {1}. Hit Enter to spin for your gift amount", index, currentTile.description);
                    Console.ReadKey();
                    WeddingGiftSpin();
                    break;

                case ActionCodes.BANKLOAN:
                    int interest = (int) (currentPlayer.bankLoan * 0.1);
                    if (currentPlayer.bankLoan == 0)
                    {
                        Console.WriteLine("[{0}] You have no bank loan so no interest to pay", index);
                    }else
                    {
                    currentPlayer.cash -= interest;
                    Console.WriteLine("[{0}] {1}. Paid {2} and balance is {3}", index, currentTile.description, interest, currentPlayer.cash);
                    }

                    break;                        

                case ActionCodes.END_GAME:
                    SpinToWin();
                    break;

                default:

                    if (currentTile.colour == Colour.GREY)
                    {                        
                        if (currentPlayer.insurance == 0)
                        {
                            Pay(currentPlayer, currentTile.amount);
                            Console.WriteLine("[{0}] {1}", index, currentTile.description);
                        }
                        else
                            Console.WriteLine("[{0}] Lucky you, your insurance saved you {1}", index, currentTile.amount);
                    }
                    else
                    {
                        currentPlayer.cash += currentTile.amount;
                        Console.WriteLine("[{0}] {1}. New balance is {2}", index, currentTile.description, currentPlayer.cash);      
                    }
              
                    break;
            }

            return destination;
        }

        private void ColourFilter(Tile currentTile, int index)
        {
            switch (currentTile.colour)
            {
                case Colour.GREEN:
                    if (currentTile.actionCode == ActionCodes.PAYDAY)
                    {
                        currentPlayer.cash += currentPlayer.salary;
                        Console.WriteLine("[{0}] {1}. You now have {2}", index, currentTile.description, currentPlayer.cash);
                    }
                    else if (currentTile.actionCode == ActionCodes.CASH)
                    {
                        currentPlayer.cash += currentTile.amount;
                        Console.WriteLine("[{0}] {1}. You now have {2}", index, currentTile.description, currentPlayer.cash);
                    }
                    else if (currentTile.actionCode == ActionCodes.BRIDGE)
                    {
                        BridgeToll(currentTile, index);
                    }
                    else if(currentTile.actionCode == ActionCodes.BANKLOAN)
                    {
                        int interest = (int) (currentPlayer.bankLoan * 0.1);
                        if (currentPlayer.bankLoan != 0)
                        {
                            currentPlayer.cash -= interest;
                            Console.WriteLine("[{0}] {1}. Paid {2} and balance is {3}", index, 
                                currentTile.description, interest, currentPlayer.cash);
                        }                        
                    }                      

                    break;

                case Colour.ORANGE:

                    if (currentPlayer.salary == 0)
                    {
                        currentPlayer.salary = currentTile.amount;
                        Console.WriteLine("[{0}] Your new salary rate is {1}/month", index, currentPlayer.salary);
                    }

                    break;
                    
                case Colour.RED:

                    stopAndSpin = true;
                   
                    break;

                default:
                    break;
            }            
            
        }
       
        private void BridgeToll(Tile currentTile, int index)
        {
            if (bridge.owner == null)
            {
                Console.WriteLine("[{0}] You are the first past the bridge and you own it. Congrats!", index);
                bridge.owner = currentPlayer;
            }
            else
            {
                if (currentPlayer != bridge.owner)
                {
                    Console.WriteLine("[{0}] {1}", index, currentTile.description);
                    Pay(currentPlayer, bridge.tollfee);
                    bridge.owner.cash += bridge.tollfee;
                    Console.WriteLine("[{0}] You paid {1} toll fee to {2}. New balance is {3}", index, bridge.tollfee, bridge.owner.name, currentPlayer.cash);
                    Console.WriteLine("[{0}]\'s balance is now {1}", bridge.owner.name, bridge.owner.cash);
                }
                else
                    Console.WriteLine("[{0}] You own the bridge so you get a free pass", index);
            }
        }

        private void UpdatePlayerPosition(int destination)
        {
            currentPlayer.position = destination;
        }

        private void SpinToWin()
        {
            Console.WriteLine("Enter a number then spin. If you match it you win");
            int guess = ValidateGuess(Console.ReadLine());
            int spin = randomGenerator.Next(1, rollLimitExcl);

            Console.WriteLine("You rolled a {0}..", spin);

            if (guess == spin)
                RetireToMillionnaireAcres();
            else
                RetireToPoorFarm();
            
        }

        private void LuckySpin(Tile currentTile)
        {
            Console.WriteLine("Choose two numbers then spin. If one you turn up one of them you get $50,000");
            Console.Write("Enter first number: ");
            int firstNumber = ValidateGuess(Console.ReadLine());
            Console.Write("\nEnter second number: ");
            int secondNumber = ValidateGuess(Console.ReadLine());

            Console.WriteLine("\nPress Enter to roll...");
            int roll = Spin();

            Console.WriteLine("You spun a {0}", roll);

            if(roll == firstNumber || roll == secondNumber)
                currentPlayer.cash += currentTile.amount;
        }

        private void RetireToPoorFarm()
        {
            Console.WriteLine("Sorry, landed on poor farm. You lost everything but your cash");

            if (currentPlayer.isBridgeOwner)
                bridge.owner = null;

            currentPlayer.house = 0;
            currentPlayer.stock = 0;
            currentPlayer.insurance = 0;
            currentPlayer.cash -= currentPlayer.bankLoan;
            currentPlayer.bankLoan = 0;

            currentPlayer.isRetired = true;
        }

        private void RetireToMillionnaireAcres()
        {
            //Liquidate assets
            int assetValue = (currentPlayer.house * 3) + (currentPlayer.stock * 10) + (currentPlayer.insurance * 5);
            int bonuses = currentPlayer.kids * 25000;

            Console.WriteLine("Congrats!, you landed on millionnaire acres. Your assets amount to {0}", assetValue);

            currentPlayer.cash += assetValue + bonuses;

            if (currentPlayer.isMarried)
                currentPlayer.cash += 50000;

            currentPlayer.cash -= currentPlayer.bankLoan;
            currentPlayer.bankLoan = 0;

            currentPlayer.isRetired = true;
        }

        private void Pay(Player player, int amount)
        {
            if (amount > player.cash)
                GetBankHelpToPay(amount);
            else
                currentPlayer.cash -= amount;
        }

        private string ValidateResponse(string response)
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

        private string ValidateOption(string response)
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

        private int ValidateGuess(string guess)
        {
            bool tryAgain = true;
            int numGuess = 0;

            do
            {
                if (!string.IsNullOrEmpty(guess))
                {
                    bool isNumber = int.TryParse(guess, out numGuess);

                    if(isNumber)
                        tryAgain = false;
                    else
                    {
                        Console.WriteLine("Invalid input, please enter a number between 1 & {0}", rollLimitExcl - 1);
                        guess = Console.ReadLine();
                    }
                }
                else
                {
                    Console.WriteLine("No input detected. Please enter a number between 1 and {0}", rollLimitExcl - 1);
                    guess = Console.ReadLine();
                }
               
                
            } while (tryAgain);

            return numGuess;
        }

        private void WeddingGiftSpin()
        {
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

            ContributeGift(currentPlayer, giftContribution);
        }

        private void ContributeGift(Player currentPlayer, int giftContribution)
        {
            int totalContribution = 0;
           
            foreach (Player player in players.Where(p => p.isRetired == false))
            {
                if (player != currentPlayer)
                {
                    Pay(player, giftContribution);
                    totalContribution += giftContribution;
                    Console.WriteLine("{0}\'s new balance is {1}", player.name, player.cash);
                }
                
            }
            currentPlayer.cash += totalContribution;
            Console.WriteLine("{0} received a total of {1} in wedding presents. New Balance is {2}", 
                currentPlayer.name, totalContribution, currentPlayer.cash);
        }

        private void DeclareVictor()
        {
            foreach (Player player in players)
            {
                Console.WriteLine("{0} has a cash total of {1}", player.name, player.cash);
            }

            Console.WriteLine("{0} won this game. Game Over...\nPress Enter to Exit\n\n", players.OrderByDescending(p => p.cash).First().name);
            Console.ReadKey();
            Environment.Exit(0);
        }

        private void GetBankHelpToPay(int amount)
        {
            //Bank gives 10k, 20k, 50k and 100k loans
            int loan = 0;

            if (amount - currentPlayer.cash < 10000)
                loan = 10000;
            else if (amount - currentPlayer.cash < 20000 && amount - currentPlayer.cash > 10000)
                loan = 20000;
            else if (amount - currentPlayer.cash < 50000 && amount - currentPlayer.cash > 20000)
                loan = 50000;
            else
                loan = 100000;

            Console.WriteLine("Received a loan of {0} from the bank", loan);

            currentPlayer.bankLoan += loan;
            currentPlayer.cash += loan;
            currentPlayer.cash -= amount;
        }

    }

    
}
