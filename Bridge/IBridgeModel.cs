using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    public interface IBridgeModel
    {
        IPlayer owner { get; set; }
        int tollfee { get; set; }
        bool PayBridgeToll(IPlayer player, IPayments payments);
    }
}
