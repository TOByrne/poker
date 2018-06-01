using PokerInterfaces;
using System.Collections.Generic;

namespace Poker
{
    public class Table : ITable
    {
        private List<IPokerPlayer> Players { get; set; }
        private IDealer Dealer { get; set; }
        private BitHand Cards { get; set; }

        private IPokerGameType Game { get; set; }

        public void Play()
        {
            while (!Game.Over)
            {
                Game.Init(this, Players, Dealer);

                while (!Game.GameHand.Over)
                {
                    
                }
            }
        }
    }
}
