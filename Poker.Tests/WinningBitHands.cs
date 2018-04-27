using NUnit.Framework;

namespace Poker.Tests
{
    [TestFixture]
    public class WinningBitHands
    {
        [Test]
        public void TestRoyalFlush()
        {
            //  Hand is going to be some Royal Flush
            //  with other stuff spotted around

            //  Royal Flush:
            //  0011 1110 0000 0000
            //  3    e    0    0

            //  Royal Flush with other stuff:
            //  0011 1111 0100 0010
            //  3    f    4    2

            BitHand hand1 = new BitHand(0x1010101010103f42);
            BitHand hand2 = new BitHand(0x010101013f421111);
            BitHand hand3 = new BitHand(0x10103f4210101010);
            BitHand hand4 = new BitHand(0x3f42010101010101);

            Assert.IsTrue(BitBoardHands.RoyalFlush(hand1, null));
            Assert.IsTrue(BitBoardHands.RoyalFlush(hand2, null));
            Assert.IsTrue(BitBoardHands.RoyalFlush(hand3, null));
            Assert.IsTrue(BitBoardHands.RoyalFlush(hand4, null));
        }

        [Test]
        public void TestStraightFlush()
        {
            //  0000 0010 0101 1010     0000 0000 0000 0000     0000 0000 0000 0000     0000 0000 0000 0000
            BitHand hand1 = new BitHand(0x025a000000000000);

            //  0000 0011 1110 0000     0000 0000 0000 0000     0000 0000 0000 0000     0000 0000 0000 0000
            BitHand hand2 = new BitHand(0x03e0000000000000);

            var bestHand = new BitHand();
            Assert.IsFalse(BitBoardHands.StraightFlush(hand1, bestHand));
            Assert.IsTrue(BitBoardHands.StraightFlush(hand2, bestHand));
        }

        [Test]
        public void TestFourOfAKind()
        {
            //  0000 0010 0101 1010     0000 0011 0000 0000     0001 0010 0000 0001     0000 0010 0000 0000
            BitHand hand1 = new BitHand(0x025a030012010200);

            var bestHand = new BitHand();
            Assert.IsTrue(BitBoardHands.FourOfAKind(hand1, bestHand));
        }

        [Test]
        public void TestFullHouse()
        {
            //  0000 0010 0000 0000     0000 0010 1000 0000     0000 0010 0000 0000     0000 0000 1000 0000
            //  0x0200 0x0280 0x0200 0x0080
            BitHand hand1 = new BitHand(0x0200028002000080);

            Assert.IsTrue(BitBoardHands.FullHouse(hand1, null));
        }

        [Test]
        public void TestFlush()
        {
            //  0000 0010 0101 1010     0000 0000 0000 0000     0000 0000 0000 0000     0000 0000 0000 0000
            BitHand hand1 = new BitHand(0x025a000000000000);

            var bestHand = new BitHand();
            Assert.IsTrue(BitBoardHands.Flush(hand1, bestHand));
        }

        [Test]
        public void TestStraight()
        {
            //  Hand is going to be some Royal Flush
            //  with other stuff spotted around

            //  Royal Flush:
            //  0011 1110 0000 0000
            //  3    e    0    0
            BitHand hand1 = new BitHand(0x1010101010103f42);

            //  0000 1111 1000 0000     0000 0000 0000 0000     0000 0000 0000 0000     0000 0000 0000 0000
            BitHand hand2 = new BitHand(0x010101013f421111);

            //  0010 0000 0000 0000     0011 0000 0000 0000     0000 1000 0000 0000     0000 0100 0000 0000
            BitHand hand3 = new BitHand(0x10103f4210101010);

            //  0000 0000 0000 0000     0000 0010 0100 0000     0000 0001 1000 0000     0000 0100 0000 0000
            BitHand hand4 = new BitHand(0x3f42010101010101);

            Assert.IsTrue(BitBoardHands.RoyalFlush(hand1, null));
            Assert.IsTrue(BitBoardHands.RoyalFlush(hand2, null));
            Assert.IsTrue(BitBoardHands.RoyalFlush(hand3, null));
            Assert.IsTrue(BitBoardHands.RoyalFlush(hand4, null));
        }

        [Test]
        public void TestThreeOfAKind()
        {
            //  Trips
            //  0011 0000 0000 0000     0011 0000 0000 0000     0000 0000 0000 0000     0010 0000 0000 0000
            var trip1 = new BitHand(0x3000300000002000);
            //  0001 0000 0000 0000     0011 0000 0010 0000     0001 0000 0000 0100     0000 0000 0000 0000
            var trip2 = new BitHand(0x1000302010080000);

            //  Pairs
            //  0011 0000 0000 0000     0011 0000 0000 0000     0000 0000 0000 0000     0000 0000 0000 0000
            var pair1 = new BitHand(0x3000300000000000);    //  Actually a 2-pair
            //  0001 0000 0000 0000     0011 0000 0010 0000     0000 0000 0000 0100     0000 0000 0000 0000
            var pair2 = new BitHand(0x1000302000080000);    //  Not a 2-pair

            var bestHand = new BitHand();
            Assert.IsTrue(BitBoardHands.ThreeOfAKind(trip1, bestHand));
            Assert.IsTrue(BitBoardHands.ThreeOfAKind(trip2, bestHand));

            Assert.IsFalse(BitBoardHands.ThreeOfAKind(pair1, bestHand));
            Assert.IsFalse(BitBoardHands.ThreeOfAKind(pair2, bestHand));
        }

        [Test]
        public void TestTwoPair()
        {
            //  Pairs
            //  0011 0000 0000 0000     0011 0000 0000 0000     0000 0000 0000 0000     0000 0000 0000 0000
            var pair1 = new BitHand(0x3000300000000000);    //  A 2-pair
            //  0001 0000 0000 0000     0011 0000 0010 0000     0000 0000 0000 0100     0000 0000 0000 0000
            var pair2 = new BitHand(0x1000302000080000);    //  Not a 2-pair

            //  Trips
            //  0011 0000 0000 0000     0010 1000 0000 0000     0000 0000 0000 0000     0010 0000 0000 0000
            var trip1 = new BitHand(0x3000280000002000);
            //  0001 0000 0000 0000     0011 0000 0010 0000     0001 0000 0000 0100     0000 0000 0000 0000
            var trip2 = new BitHand(0x1000302000080000);

            var bestHand = new BitHand();
            Assert.IsTrue(BitBoardHands.TwoPair(pair1, bestHand));
            Assert.IsFalse(BitBoardHands.TwoPair(pair2, bestHand));

            Assert.IsFalse(BitBoardHands.TwoPair(trip1, bestHand));
            Assert.IsFalse(BitBoardHands.TwoPair(trip2, bestHand));
        }

        [Test]
        public void TestPair()
        {
            //  Pairs
            //  0011 0000 0000 0000     0011 0000 0000 0000     0000 0000 0000 0000     0000 0000 0000 0000
            var pair1 = new BitHand(0x3000300000000000);    //  Actually a 2-pair
            //  0001 0000 0000 0000     0011 0000 0010 0000     0000 0000 0000 0100     0000 0000 0000 0000
            var pair2 = new BitHand(0x1000302000080000);    //  Not a 2-pair

            //  Trips
            //  0011 0000 0000 0000     0011 0000 0000 0000     0000 0000 0000 0000     0010 0000 0000 0000
            var trip1 = new BitHand(0x3000300000000000);
            //  0001 0000 0000 0000     0011 0000 0010 0000     0001 0000 0000 0100     0000 0000 0000 0000
            var trip2 = new BitHand(0x1000302000080000);

            var bestHand = new BitHand();
            Assert.IsTrue(BitBoardHands.Pair(pair1, bestHand));
            Assert.IsTrue(BitBoardHands.Pair(pair2, bestHand));

            Assert.IsTrue(BitBoardHands.Pair(trip1, bestHand));
            Assert.IsTrue(BitBoardHands.Pair(trip2, bestHand));
        }
    }
}