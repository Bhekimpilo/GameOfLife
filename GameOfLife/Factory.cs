using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;

namespace GameOfLife
{
    public static class Factory
    {
        public static IValidation CreateValidator()
        {
            return new InputValidator();
        }

        public static IPlayer CreatePlayer()
        {
            return new Player();
        }

        public static ISpinner CreateSpinner()
        {
            return new Spinner(CreateValidator(), CreatePlayer());
        }

        public static IRetirement InitRetirement()
        {
            return new Retirement();
        }

        public static IBridgeModel CreateBridge()
        {
            return new BridgeModel();
        }

        public static IGameboard CreateGameBoard()
        {
            return new GameBoard();
        }

        public static ITile CreateTile()
        {
            return new Tile();
        }

        public static ILoan GiveLoan()
        {
            return new BankLoan();
        }

        public static IPayments PayNow()
        {
            return new Payments(GiveLoan());
        }

    }
}
