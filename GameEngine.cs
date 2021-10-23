using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    class GameEngine
    {
        private IPlayer _player = Factory.CreatePlayer();
        private IValidation _validator = Factory.CreateValidator();
        private IGameboard _board = Factory.CreateGameBoard();
        private ISpinner _spinner = Factory.CreateSpinner();
        private IRetirement _retirement = Factory.InitRetirement();
        private IBridgeModel _bridge = Factory.CreateBridge();
        private ILoan _loans = Factory.GiveLoan();
        private IPayments _payments = Factory.PayNow();

        private List<IPlayer> players = new List<IPlayer>();
        private Random randomGenerator = new Random();
        
        private int currentIndex = 0;
        private const int maxSpin = 8;
        private const  int rollLimitExcl = maxSpin + 1;
        private bool stopAndSpin = false;
        

        public void Init()
        {
            Console.WriteLine("Welcome. Add two to six players to get started.");

            _player.AddPlayers(players);
            _player = _spinner.SelectorToss(players, rollLimitExcl);
            
            while(players.Any(p => p.isRetired == false)) 
                PlayGame();
        }        

        private void PlayGame()
        {
          
            Console.WriteLine("\nCurrent player is {0}. Hit Enter to roll the dice", _player.name);
            var key = Console.ReadKey();

            if (_validator.ValidateEnterKey(key))
            {
                if (_player.position == _board.tiles.Count - 1)
                {
                    if (_spinner.SpinToWin(rollLimitExcl))
                        _retirement.RetireToMillionnaireAcres(_player);
                    else
                        _retirement.RetireToPoorFarm(_player, _bridge);
                }
                else
                {
                    int roll = _spinner.Spin(_player, rollLimitExcl);

                    if (roll != -1)
                        Move(_player.position, roll);
                }

                Console.WriteLine();

                if (players.Any(p => p.isRetired == false))
                    SelectPlayer();
                else
                    DeclareVictor();
            }
            else
            {
                Console.Write("\b\r");
            }
        }
        
        private void Move(int start, int roll)
        {            
            int destination = start + roll;
            ITile currentTile;

            if (destination >= _board.tiles.Count)
                destination = _board.tiles.Count - 1;

            Console.WriteLine("{0} moves {1} steps from {2}...", _player.name, roll, _player.position);
            
            for (int i = start + 1; i < destination; i++)
            {
                currentTile = _board.tiles.ElementAt(i);

                if (_player.isCareer && i == 7)
                {
                    i += 11;
                    destination += 11;
                    currentTile = _board.tiles.ElementAt(i);
                    _player.isCareer = false;                    
                    
                }
               
                i = ColourFilter(currentTile, i);

                if (_player.isCollege && i == 6)
                {
                    destination += 4;
                    continue;
                }

                if (stopAndSpin)
                {
                    destination = _board.tiles.IndexOf(currentTile);
                    stopAndSpin = false;
                    break;
                }

                UpdatePlayerPosition(i);
            }

            currentTile = _board.tiles.ElementAt(destination);

            destination = ActionFilter(currentTile, destination);
            
            UpdatePlayerPosition(destination);
        }

        private int PathSelection(ITile currentTile, int index)
        {
            Console.WriteLine("[{0}] {1}", index, currentTile.description);

            string response = Console.ReadLine().ToLower().Trim();

            if (currentTile.actionCode == ActionCodes.INSURANCE)
            {
                response = _validator.ValidateResponse(response);

                if (response.Equals("y"))
                {
                    _player.cash -= currentTile.amount;
                    _player.insurance += currentTile.amount;
                    Console.WriteLine("[{0}] Wise choice buying insurance. New balance is {1}", index, _player.cash);
                }
                else
                    Console.WriteLine("[{0}] You are not covered in case of disaster", index);
            }

            else
            {
                response = _validator.ValidateOption(response);

                if (response.Equals("a"))
                    _player.isCareer = true;
                else if (response.Equals("b"))
                {
                    index += 4;
                    _player.isCollege = true;
                }
            }

            return index;
        }

        private IPlayer SelectPlayer()
        {
            currentIndex = players.IndexOf(_player);
            
            if (currentIndex == players.Count - 1)
                currentIndex = 0;
            else
                currentIndex++;

            _player = players.ElementAt(currentIndex);

            if (_player.isBlackListed || _player.isRetired)
            {
                if (_player.isBlackListed)
                {
                    Console.WriteLine("{0} missed a turn", _player.name);
                    _player.isBlackListed = false;
                }

                _player = SelectPlayer();
            }

            return _player;
        }

        private int ActionFilter(ITile currentTile, int index)
        {
            int destination = index;

            switch (currentTile.actionCode)
            {                
                case ActionCodes.PAYDAY:
                    _player.cash += _player.salary + 10000;
                    Console.WriteLine("[{0}] {1} plus $10,000 bonus. New balance is {2}", index, currentTile.description, _player.cash);
                    break;
              
                case ActionCodes.SALARY:
                    _player.salary = currentTile.amount;
                    if (!_player.isCareer)
                        destination = 17;
                    Console.WriteLine("[{0}] {1}", index, currentTile.description);
                    break;

                case ActionCodes.ADD_KIDS:
                    Console.WriteLine("[{0}] {1}", index, currentTile.description);
                    _player.kids += currentTile.amount;
                    break;

                case ActionCodes.HOUSE:
                    Console.WriteLine("[{0}] {1}", index, currentTile.description);
                    string buyHouse = Console.ReadLine();
                    buyHouse = _validator.ValidateResponse(buyHouse.ToLower().Trim());

                    if (buyHouse.Equals("y"))
                    {
                        _player.house += currentTile.amount;
                        _payments.PayAmount(_player, currentTile.amount);
                        Console.WriteLine("Congrats on buying a house. Your new balance is ${0}", _player.cash);
                    }else
                        Console.WriteLine("Ok then Hobo, keep going...");

                    break;
                
                case ActionCodes.STOCK:
                    Console.WriteLine("[{0}] {1}", index, currentTile.description);
                    string buyStock = Console.ReadLine();
                    buyStock = _validator.ValidateResponse(buyStock.ToLower().Trim());

                    if (buyStock.Equals("y"))
                    {
                        _player.stock += currentTile.amount;
                        _payments.PayAmount(_player, currentTile.amount);
                        Console.WriteLine("Good choice, you now have stock worth {0}. Balance is {1}", _player.stock, _player.cash);
                    }else
                        Console.WriteLine("The market is really volatile...");
                    break;

                case ActionCodes.BLACKLIST:
                    _player.isBlackListed = true;
                    Console.WriteLine("[{0}] {1}", index, currentTile.description);
                    break;

                case ActionCodes.BRIDGE:    
                    if(_bridge.PayBridgeToll(_player, _payments))
                    {
                        Console.WriteLine("[{0}] Paid {1} to {2}. Your new balance is {3}", index, _bridge.tollfee, _bridge.owner.name, _player.cash);
                        Console.WriteLine("[{0}] {1}\'s new balance is {2}", index, _bridge.owner.name, _bridge.owner.cash);
                    }else
                        Console.WriteLine("[{0}] Congratulations, your are the first across the bridge and you own it!", index);

                    break;
                
                case ActionCodes.RESET:
                    destination = 0;
                    _player.insurance = 0;
                    _player.house = 0;
                    _player.stock = 0;
                    _player.cash = 5000;
                    _player.isCollege = false;
                    _player.isCareer = false;

                    if (_player == _bridge.owner)
                    {
                        _bridge.owner = null;
                    }
                    Console.WriteLine("[{0}] {1}", index, currentTile.description);
                    break;

                case ActionCodes.GET_MARRIED:
                    _player.isMarried = true;
                    Console.WriteLine("[{0}] {1}. Hit Enter to spin for your gift amount", index, currentTile.description);
                    Console.ReadKey();
                    //TODO: Check if more than one player is NOT retired
                    int contribution = _spinner.WeddingGiftSpin(rollLimitExcl);
                    ContributeGift(_player, contribution);
                    break;

                case ActionCodes.BANKLOAN:
                    int interest = (int)(_player.bankLoan * 0.1);
                    if (_player.bankLoan == 0)
                    {
                        Console.WriteLine("[{0}] You have no bank loan so no interest to pay", index);
                    }else
                    {
                        _player.cash -= interest;
                        Console.WriteLine("[{0}] {1}. Paid {2} and balance is {3}", index, currentTile.description, interest, _player.cash);
                    }

                    break;                        

                case ActionCodes.END_GAME:
                    if (_spinner.SpinToWin(rollLimitExcl))
                        _retirement.RetireToMillionnaireAcres(_player);
                    else
                        _retirement.RetireToPoorFarm(_player, _bridge);
                    break;

                default:

                    if (currentTile.colour == Colour.GREY)
                    {
                        if (_player.insurance == 0)
                        {
                            _payments.PayAmount(_player, currentTile.amount);
                            Console.WriteLine("[{0}] {1}. New balance is {2}", index, currentTile.description, _player.cash);
                        }
                        else
                            Console.WriteLine("[{0}] Lucky you, your insurance saved you {1}", index, currentTile.amount);
                    }
                    else
                    {
                        _player.cash += currentTile.amount;
                        Console.WriteLine("[{0}] {1}. New balance is {2}", index, currentTile.description, _player.cash);      
                    }
              
                    break;
            }

            return destination;
        }

        private int ColourFilter(ITile currentTile, int index)
        {
            switch (currentTile.colour)
            {
                case Colour.GREEN:
                    if (currentTile.actionCode == ActionCodes.PAYDAY)
                    {
                        _player.cash += _player.salary;
                        Console.WriteLine("[{0}] {1}. You now have {2}", index, currentTile.description, _player.cash);
                    }
                    else if (currentTile.actionCode == ActionCodes.CASH)
                    {
                        _player.cash += currentTile.amount;
                        Console.WriteLine("[{0}] {1}. You now have {2}", index, currentTile.description, _player.cash);
                    }
                    else if (currentTile.actionCode == ActionCodes.BRIDGE)
                    {
                        if (_bridge.PayBridgeToll(_player, _payments))
                        {
                            Console.WriteLine("[{0}] Paid {1} to {2}. Your new balance is {3}", index, _bridge.tollfee, _bridge.owner.name, _player.cash);
                            Console.WriteLine("[{0}] {1}\'s new balance is {2}", index, _bridge.owner.name, _bridge.owner.cash);
                        }
                        else
                            Console.WriteLine("[{0}] Congratulations, your are the first across the bridge and you own it!", index);
                    }
                    else if(currentTile.actionCode == ActionCodes.BANKLOAN)
                    {
                        int interest = (int)(_player.bankLoan * 0.1);
                        if (_player.bankLoan != 0)
                        {
                            _player.cash -= interest;
                            Console.WriteLine("[{0}] {1}. Paid {2} and balance is {3}", index,
                                currentTile.description, interest, _player.cash);
                        }
                        else
                            Console.WriteLine("[{0}] No bank loan so no interest to pay", index);
                    }                      

                    break;

                case Colour.AMBER:
                    index = PathSelection(currentTile, index);
                    break;

                case Colour.ORANGE:

                    if (_player.salary == 0)
                    {
                        _player.salary = currentTile.amount;
                        Console.WriteLine("[{0}] Your new salary rate is {1}/month", index, _player.salary);
                    }

                    break;
                    
                case Colour.RED:

                    stopAndSpin = true;
                   
                    break;

                default:
                    break;
            }

            return index;
            
        }

        private void UpdatePlayerPosition(int destination)
        {
            _player.position = destination;
        }               

        private void ContributeGift(IPlayer weddingPlayer, int giftContribution)
        {
            int totalContribution = 0;
           
            foreach (IPlayer player in players.Where(p => p.isRetired == false))
            {
                if (player != weddingPlayer)
                {
                    _payments.PayAmount(player, giftContribution);
                    totalContribution += giftContribution;
                    Console.WriteLine("{0}\'s new balance is {1}", player.name, player.cash);
                }
                
            }
            weddingPlayer.cash += totalContribution;
            Console.WriteLine("{0} received a total of {1} in wedding presents. New Balance is {2}", 
                weddingPlayer.name, totalContribution, weddingPlayer.cash);
        }

        private void DeclareVictor()
        {
            foreach (Player player in players)
            {
                Console.WriteLine("{0} has a cash total of {1}\n", player.name, player.cash);
            }

            Console.WriteLine("Game over! {0} won this game....\n", players.OrderByDescending(p => p.cash).First().name);
            Console.WriteLine("Press Enter to Exit");
            Console.ReadKey();
            Environment.Exit(0);
        }

        
    }

    
}
