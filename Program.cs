using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {            
            GameEngine engine = new GameEngine();
            engine.Init();
        }
    }
}
