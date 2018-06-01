using System.Collections.Generic;
using PokerInterfaces;

namespace PokerRules
{
    public abstract class PokerRules : IGameRules
    {
        public IEnumerable<IPokerRule> Rules { get; set; }
    }
}