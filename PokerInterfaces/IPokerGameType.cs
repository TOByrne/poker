using System;
using System.Collections.Generic;

namespace PokerInterfaces
{
    public interface IPokerGameType
    {
        IGameHand GameHand { get; set; }
        bool Over { get; set; }

        void Init(ITable table, List<IPokerPlayer> players, IDealer dealer);
    }

    public interface IGameHand
    {
        bool Over { get; set; }
    }

    public interface IGameRules
    {
        IEnumerable<IPokerRule> Rules { get; set; }

        ////  Are all rules satisfied?  If not, these are the rules that were broken.
        //bool RulesSatisfied(out IEnumerable<IPokerRule> brokenRules);
    }

    public interface IPokerRule
    {
        string RuleDescription { get; }
        string RuleNotSatisfiedMsg { get; }

        Action BrokenRuleAction { get; }
    }

    public interface IHandRule : IPokerRule
    {
        bool RuleSatisfied(IPlayerHand hand);
    }

    public interface IPokerBettingRule : IPokerRule
    {
        bool RuleSatisfied(IPlayerBet bet);
    }

    public interface IPlayerHand
    {
        int HandSize { get; set; }
    }

    public interface IPlayerBet
    {
        
    }
}
