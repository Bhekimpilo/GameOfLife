using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    public class BankLoan : ILoan
    {
        public void GetBankHelpToPay(IPlayer player, int amount)
        {
            int loan = 0;

            if (amount - player.cash <= 10000)
                loan = 10000;
            else if (amount - player.cash <= 20000 && amount - player.cash > 10000)
                loan = 20000;
            else if (amount - player.cash <= 50000 && amount - player.cash > 20000)
                loan = 50000;
            else
                loan = 100000;

            Console.WriteLine("{0} received a loan of {1} from the bank", player.name, loan);

            player.bankLoan += loan;
            player.cash += loan;
            player.cash -= amount;
        }
    }
}
