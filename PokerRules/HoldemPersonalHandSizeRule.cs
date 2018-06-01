using System;
using PokerInterfaces;

namespace PokerRules
{
    /// <summary>
    /// The rule to determine whether the player has a valid personal hand
    /// ie, this doesn't include the cards on the table
    /// </summary>
    public class HoldemPersonalHandSizeRule : IHandRule
    {
        public string RuleDescription =>
            "Your hand size includes the cards on the table.  The total number of cards in your hand and on the table must be between 5 and 7.";

        public string RuleNotSatisfiedMsg =>
            "You do not have the right number of cards in your hand.  You must have between 5 and 7 cards.";

        public Action BrokenRuleAction { get; private set; }

        public HoldemPersonalHandSizeRule()
        {
            BrokenRuleAction = BrokenRule;
        }

        private void BrokenRule()
        {
            throw new NotImplementedException();
        }

        //  The rule to determine how many cards the player has
        public bool RuleSatisfied(IPlayerHand hand)
        {
            return hand.HandSize == 2;
        }
    }
}