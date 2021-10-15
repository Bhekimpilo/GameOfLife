using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    class Bridge
    {        
        public Player owner { get; set; }
        public int tollfee { get; set; }

        public Bridge()
        {            
            this.owner = owner;
            this.tollfee = 5000;
        }
        
    }
}
