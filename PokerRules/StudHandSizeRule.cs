using System;
using System.Linq;
using PokerInterfaces;

namespace PokerRules
{
    /// <summary>
    /// In Stud Poker, the player's hand can be no more than 5 cards
    /// </summary>
    public class StudHandSizeRule : IHandRule
    {
        public string RuleDescription =>
            "You may have up to 5 cards in your hand.  Fewer cards are permitted in the earlier stages, but once each player is dealt 5 cards, you may not 'drop' cards.  You may call for new cards, but your hand size must be the same before and after the exchange.";

        public string RuleNotSatisfiedMsg =>
            "You have too many cards in your hand.";

        public Action BrokenRuleAction { get; private set; }

        public StudHandSizeRule()
        {
            BrokenRuleAction = BrokenRule;
        }

        private void BrokenRule()
        {
            throw new NotImplementedException();
        }

        public bool RuleSatisfied(IPlayerHand hand)
        {
            return hand.HandSize <= 5;
        }
    }
}
