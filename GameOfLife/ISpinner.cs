using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    public interface ISpinner
    {
        int Spin(IPlayer player, int rollLimitExcl);

        bool SpinToWin(int rollLimitExcl);

        int WeddingGiftSpin(int rollLimitExcl);

        IPlayer SelectorToss(List<IPlayer> players, int rollLimitExcl);
    }
}
