using System;
using PokerInterfaces;

namespace PokerRules
{
    /// <summary>
    /// The rule to determine whether the player's hand, including the cards
    /// on the table, is the right size.
    /// </summary>
    public class HoldemFullHandSizeRule : IHandRule
    {
        public string RuleDescription =>
            "Your hand size includes the cards on the table.  The total number of cards in your hand and on the table must be between 5 and 7.";

        public string RuleNotSatisfiedMsg =>
            "You do not have the right number of cards in your hand.  You must have between 5 and 7 cards.";

        public Action BrokenRuleAction { get; private set; }

        public HoldemFullHandSizeRule()
        {
            BrokenRuleAction = BrokenRule;
        }

        private void BrokenRule()
        {
            throw new NotImplementedException();
        }

        public bool RuleSatisfied(IPlayerHand hand)
        {
            return hand.HandSize >= 5 && hand.HandSize <= 7;
        }
    }
}