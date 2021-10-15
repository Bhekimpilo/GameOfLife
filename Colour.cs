using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    public enum Colour
    {        
        GREEN, //PayDay
        ORANGE, //Salary
        AMBER, //Pause and make decision but don't lose count
        RED, // Stop and spin to win
        GREY, //Bridge
        BLACK, //normal
        BLUE // Optionalpurchase
    }
}
