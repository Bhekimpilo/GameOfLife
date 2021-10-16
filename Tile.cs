using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    class Tile 
    {
        public string description { get; set; }
        public int amount { get; set; }
        public ActionCodes actionCode { get; set; }
        public Colour colour { get; set; }

        public Tile(string desc, int amount, ActionCodes code = ActionCodes.CASH, Colour tileColour = Colour.BLACK)
        {
            description = desc;
            this.amount = amount;
            actionCode = code;
            colour = tileColour;

        }
    }
    
}
