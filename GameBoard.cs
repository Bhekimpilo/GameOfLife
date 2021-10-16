using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    class GameBoard
    {
       
        public List<Tile> tiles { get; set; }

        public GameBoard()
        {
            tiles = new List<Tile>();

            tiles.Add(new Tile("Home Tile...", 0));
            tiles.Add(new Tile("Would you like to buy insurance for $4000? (Y/N)", 4000, ActionCodes.INSURANCE, Colour.AMBER));
            tiles.Add(new Tile("Find circus elephant, collect $1,500. Choose \nA. Career \nB. College", 1500, ActionCodes.CASH, Colour.AMBER
                ));
            tiles.Add(new Tile("Meet future spouse pay $500 for ring", -500, 0));
            tiles.Add(new Tile("Business salary $5,000", 5000, ActionCodes.SALARY, Colour.ORANGE));
            tiles.Add(new Tile("Contest winner. collect $5,000", 5000));
            tiles.Add(new Tile("Take correspondence course, pay $2500", -2500));
            tiles.Add(new Tile("Buy racoon coat, pay $500", -500));
            tiles.Add(new Tile("Collect $1,000 scholarship ", 1000, ActionCodes.CASH, Colour.GREEN));
            tiles.Add(new Tile("Pay tuition $1,500", -1500, ActionCodes.CASH, Colour.GREEN));
            tiles.Add(new Tile("Put on probation, loose turn", 0,ActionCodes.BLACKLIST));
            tiles.Add(new Tile("Collect $500 thesis prize", 500, ActionCodes.CASH));
            tiles.Add(new Tile("Doctor (salary $20,000), move ahead six spaces", 20000, ActionCodes.SALARY));
            tiles.Add(new Tile("Journalist (salary $10,000), move ahead five spaces", 10000, ActionCodes.SALARY));
            tiles.Add(new Tile("Lawyer (salary $15,000), move ahead four spaces", 15000, ActionCodes.SALARY));
            tiles.Add(new Tile("Teacher (salary $8,000), move ahead three spaces", 8000, ActionCodes.SALARY));
            tiles.Add(new Tile("Physicist (salary $15,000), move ahead two spaces", 15000, ActionCodes.SALARY));
            tiles.Add(new Tile("Bachelor's degree (salary $6,000)", 6000, ActionCodes.SALARY, Colour.ORANGE));
            tiles.Add(new Tile("Pay day, collect your salary", 0, ActionCodes.PAYDAY, Colour.GREEN));
            tiles.Add(new Tile("Put on probation, loose turn", 0, ActionCodes.BLACKLIST));
            tiles.Add(new Tile("Your annual bonus is here. Collect $6,500", 6500));
            tiles.Add(new Tile("Bank loan interest is due", 0, ActionCodes.BANKLOAN, Colour.GREEN));
            tiles.Add(new Tile("Crossed a bridge, pay or collect toll.", 0, ActionCodes.BRIDGE, Colour.GREEN));
            tiles.Add(new Tile("Collect overtime for working late", 2500, ActionCodes.CASH));
            tiles.Add(new Tile("Get Married...", 0, ActionCodes.GET_MARRIED, Colour.RED));
            tiles.Add(new Tile("Pay speeding ticket fine", -500));
            tiles.Add(new Tile("Collect tax refund...", 1500));
            tiles.Add(new Tile("Would you like to buy a house for $36,000? (Y/N)", 36000, ActionCodes.HOUSE, Colour.BLUE));
            tiles.Add(new Tile("Congratulations, you now have a baby", 1, ActionCodes.ADD_KIDS));
            tiles.Add(new Tile("Bank loan interest is due", 0, ActionCodes.BANKLOAN, Colour.GREEN));
            tiles.Add(new Tile("Bankrupcy, return to start", 0, ActionCodes.RESET));
            tiles.Add(new Tile("Contest winner. collect $5,000", 5000));
            tiles.Add(new Tile("Gamble gone wrong, pay $7,000", -7000));
            tiles.Add(new Tile("Pay day, collect your salary", 0, ActionCodes.PAYDAY, Colour.GREEN));
            tiles.Add(new Tile("Bank loan interest is due", 0, ActionCodes.BANKLOAN, Colour.GREEN));
            tiles.Add(new Tile("Would you like to buy stock for $8,000? (Y/N)", 8000, ActionCodes.STOCK, Colour.BLUE));
            tiles.Add(new Tile("Made $2000 profits from buying and selling", 2000, ActionCodes.CASH, Colour.GREEN));
            tiles.Add(new Tile("A family vacation to madagascar cost you $2500", -2500, ActionCodes.CASH, Colour.GREEN));
            tiles.Add(new Tile("Lucky Square, collect $20,000", 20000));
            tiles.Add(new Tile("Doctor's visit cost you $1,000", -1000));
            tiles.Add(new Tile("Would you like to buy a house for $20,000?", 20000, ActionCodes.HOUSE, Colour.BLUE));
            tiles.Add(new Tile("Put on probation, loose turn", 0, ActionCodes.BLACKLIST));
            tiles.Add(new Tile("Bank loan interest is due", 0, ActionCodes.BANKLOAN, Colour.GREEN));
            tiles.Add(new Tile("Pay day, collect your salary", 0, ActionCodes.PAYDAY, Colour.GREEN));
            tiles.Add(new Tile("Lost some property, If no insurance pay $15,000", 15000, ActionCodes.CASH, Colour.GREY));
            tiles.Add(new Tile("Day of reconning", 0, ActionCodes.END_GAME, Colour.RED));

        }

        
    }
}
