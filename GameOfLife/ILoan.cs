using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    public interface ILoan
    {
        void GetBankHelpToPay(IPlayer player, int amount);
    }
}
