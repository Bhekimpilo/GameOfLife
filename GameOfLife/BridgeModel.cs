using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    public class BridgeModel : IBridgeModel
    {        
        public IPlayer owner { get; set; }
        public int tollfee { get; set; }

        public BridgeModel()
        {
            owner = null;
            tollfee = 5000;
        }
        
        public bool PayBridgeToll(IPlayer player, IPayments payments)
        {
            if (owner == null)
            {
                owner = player;
                return false;
            }
            else
            {
                payments.PayAmount(player, tollfee);
                owner.cash += tollfee;
                return true;
                   
                                 
            }
        }
      
    }
}
