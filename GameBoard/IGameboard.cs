using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    public interface IGameboard
    {
        List<ITile> tiles { get; set; }
    }
}
