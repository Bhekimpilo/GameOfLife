using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    public class Tile : ITile
    {
        public string description { get; set; }
        public int amount { get; set; }
        public ActionCodes actionCode { get; set; }
        public Colour colour { get; set; }

        public Tile(string desc = "", int amount = 0, ActionCodes code = ActionCodes.CASH, Colour tileColour = Colour.BLACK)
        {
            description = desc;
            this.amount = amount;
            actionCode = code;
            colour = tileColour;

        }
    }
    
}
