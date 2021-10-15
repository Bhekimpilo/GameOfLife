using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    class Tile
    {
        public string description { get; set; }
        public int cashAmount { get; set; }
        public ActionCodes actionCode { get; set; }
        public TileColour colour { get; set; }

        public Tile(string desc, int amount, TileColour tileColour = TileColour.BLACK, ActionCodes code = ActionCodes.NEUTRAL)
        {
            description = desc;
            cashAmount = amount;
            actionCode = code;
            colour = tileColour;

        }
    }
    
}
