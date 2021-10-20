using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    public class Payments : IPayments
    {
        ILoan _loan;

        public Payments(ILoan loan) 
        {
            _loan = loan;
        }

        public void PayAmount(IPlayer player, int amount)
        {
            if (amount > player.cash)
                _loan.GetBankHelpToPay(player, amount);
            else
                player.cash -= amount;
        }
    }
}
