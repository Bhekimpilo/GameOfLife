using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    public interface IPayments
    {
        void PayAmount(IPlayer player, int amount);
    }
}
