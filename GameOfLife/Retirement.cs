using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    public class Retirement : IRetirement
    {
        public void RetireToPoorFarm(IPlayer player, IBridgeModel bridge)
        {
            Console.WriteLine("Sorry, landed on poor farm. You lost everything but your cash");

            if (player.isBridgeOwner)
                bridge.owner = null;

            player.house = 0;
            player.stock = 0;
            player.insurance = 0;
            player.cash -= player.bankLoan;
            player.bankLoan = 0;

            player.isRetired = true;
        }

        public void RetireToMillionnaireAcres(IPlayer player)
        {
            //Liquidate assets
            int assetValue = (player.house * 3) + (player.stock * 10) + (player.insurance * 5);
            int bonuses = player.kids * 25000;

            Console.WriteLine("Congrats!, you landed on millionnaire acres. Your assets amount to {0}", assetValue);

            player.cash += assetValue + bonuses;

            if (player.isMarried)
                player.cash += 50000;

            player.cash -= player.bankLoan;
            player.bankLoan = 0;

            player.isRetired = true;
        }

    }
}
