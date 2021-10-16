using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    class GameBoard
    {
       
        public List<Square> squares { get; set; }

        public GameBoard()
        {
            squares = new List<Square>();            

            squares.Add(new Square("Welcome to the game of Life...", 0));
            squares.Add(new Square("Weak spin, please try again", 0, ActionCodes.STOP_AND_ROLL_AGAIN));
            squares.Add(new Square("Find circus elephant, collect $1,500. Choose college or career?", 1500, ActionCodes.PAY_COLLECT));
            squares.Add(new Square("Meet future spouse pay $500 for ring", -500,0));
            squares.Add(new Square("Business salary $5,000", 0, ActionCodes.ASSIGN_SALARY));
            squares.Add(new Square("Contest winner. collect $5,000", 5000));
            squares.Add(new Square("Take correspondence course, pay $500", -500));
            squares.Add(new Square("Buy racoon coat, pay $500", -500));
            squares.Add(new Square("Collect $1,000 scholarship ", 1000, ActionCodes.PAY_COLLECT));
            squares.Add(new Square("Pay tuition $1,500", -1500, ActionCodes.PAY_COLLECT));
            squares.Add(new Square("Put on probation, loose turn", -4));
            squares.Add(new Square("Collect $500 thesis prize", 500));
            squares.Add(new Square("Doctor (salary $20,000), move ahead six spaces", 0));
            squares.Add(new Square("Journalist (salary $15,000), move ahead five spaces", 0));
            squares.Add(new Square("Doctor (salary $20,000), move ahead four spaces", 0));
            squares.Add(new Square("Doctor (salary $20,000), move ahead three spaces", 0));
            squares.Add(new Square("Doctor (salary $20,000), move ahead two spaces", 0));
            squares.Add(new Square("Bachelor's degree (salary $20,000)", 0));
            squares.Add(new Square("Pay day, collect your salary", 0, ActionCodes.COLLECT_SALARY));
            squares.Add(new Square("Crossed a bidge.", 0, ActionCodes.PAY_BRIDGE));
            squares.Add(new Square("Collect overtime", 2500));
            squares.Add(new Square("Pay speeding ticket fine", -500));
            squares.Add(new Square("Collect tax refund...", 1500));
            squares.Add(new Square("Bankruptcy, return to start", -6));
            squares.Add(new Square("Gamble gone wrong, pay $7,000", -7000));
            squares.Add(new Square("Get Married", 0, ActionCodes.GET_MARRIED));
            squares.Add(new Square("Day of reconning", 0, ActionCodes.STOP_AND_ROLL_AGAIN));

        }

        
    }
}
