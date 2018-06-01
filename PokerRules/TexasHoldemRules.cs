using System.Collections.Generic;
using PokerInterfaces;

namespace PokerRules
{
    public class TexasHoldemRules : PokerRules
    {
        public TexasHoldemRules()
        {
            Rules = new List<IPokerRule>();

            ((List<IPokerRule>)Rules).Add(new HoldemPersonalHandSizeRule());
            ((List<IPokerRule>)Rules).Add(new HoldemFullHandSizeRule());

            //SequenceRules = new List<>
        }
    }
}