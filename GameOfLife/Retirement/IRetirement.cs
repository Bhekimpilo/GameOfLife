using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    public interface IRetirement
    {
        void RetireToPoorFarm(IPlayer player, IBridgeModel bridge);

        void RetireToMillionnaireAcres(IPlayer player);
    }
}
