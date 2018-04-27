namespace Poker
{
    public class BitHand
    {
        public ulong Hand { get; protected set; }

        public BitHand()
        {
            Hand = 0x0;
        }

        public BitHand(ulong startingHand)
        {
            Hand = startingHand;
        }

        public void Reset(ulong newValue)
        {
            Hand = newValue;
        }

        public static ulong operator &(BitHand hand, ulong test)
        {
            return hand.Hand & test;
        }

        public static ulong operator &(ulong test, BitHand hand)
        {
            return test & hand.Hand;
        }

        public static ulong operator |(BitHand hand, ulong test)
        {
            return hand.Hand | test;
        }

        public static ulong operator |(ulong test, BitHand hand)
        {
            return test | hand.Hand;
        }

        public static bool operator ==(ulong test, BitHand hand)
        {
            return test == hand.Hand;
        }

        public static bool operator ==(BitHand hand, ulong test)
        {
            return hand.Hand == test;
        }

        public static bool operator !=(ulong test, BitHand hand)
        {
            return test != hand.Hand;
        }

        public static bool operator !=(BitHand hand, ulong test)
        {
            return hand.Hand != test;
        }

        public override bool Equals(object obj)
        {
            if (obj is BitHand)
            {
                return ((BitHand) obj).Hand == Hand;
            }
            else if (obj is ulong)
            {
                return ((ulong) obj) == Hand;
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Hand.GetHashCode();
        }
    }
}