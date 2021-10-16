using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    class Player
    {

        public string name { get; set; }
        public int position { get; set; }
        public int salary { get; set; }
        public int cash { get; set; }
        public int bankLoan { get; set; }
        public string career { get; set; }
        public bool house { get; set; }
        public int stock { get; set; }
        public int kids { get; set; }
        public bool isMarried { get; set; }
        public bool isBlackListed { get; set; }
        public bool isRetired { get; set; }
        public string retirementHome { get; set; }

        public Player(string name)
        {
            this.name = name;
            this.position = 0;
            this.salary = 0;
            this.cash = 5000;
            this.bankLoan = 0;
            this.career = null;
            this.house = false;
            this.stock = 0;
            this.kids = 0;
            this.isMarried = false;
            this.isBlackListed = false;
            this.isRetired = false;
            this.retirementHome = null;

        }
    }
}
