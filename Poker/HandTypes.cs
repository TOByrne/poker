namespace Poker
{
    public class HandTypes
    {
        //	These are in order.  Lower down the list is a better hand

        public enum WinningHand
        {
            HighCard,
            Pair,
            TwoPair,
            ThreeOfAKind,
            Straight,
            Flush,
            FullHouse,
            FourOfAKind,
            StraightFlush,
            RoyalFlush
        }
    }
}