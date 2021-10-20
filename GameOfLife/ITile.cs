using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    public interface ITile
    {
        string description { get; set; }
        int amount { get; set; }
        ActionCodes actionCode { get; set; }
        Colour colour { get; set; }
    }
}
