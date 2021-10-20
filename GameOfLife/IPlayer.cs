using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    public interface IPlayer
    {
        string name { get; set; }
        int position { get; set; }
        int salary { get; set; }
        int cash { get; set; }
        int bankLoan { get; set; }
        int house { get; set; }
        int stock { get; set; }
        int kids { get; set; }
        int insurance { get; set; }
        bool isCareer { get; set; }
        bool isCollege { get; set; }
        bool isMarried { get; set; }
        bool isBridgeOwner { get; set; }
        bool isBlackListed { get; set; }
        bool isRetired { get; set; }

        void AddPlayers(List<IPlayer> players);
    }
}
