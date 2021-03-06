using NUnit.Framework;
using System;

namespace Poker.Tests
{
	[TestFixture]
	public class ThomasHandTests
	{
		[Test]
		public void StraightFlushTest1()
		{
			var hand = new Hand
			{
				new Card(1, Card.CardSuit.Spades),
				new Card(2, Card.CardSuit.Spades),
				new Card(3, Card.CardSuit.Spades),
				new Card(4, Card.CardSuit.Spades),
				new Card(5, Card.CardSuit.Spades),
				new Card(6, Card.CardSuit.Diamonds),
				new Card(7, Card.CardSuit.Diamonds)
			};

			Hand bestHand = new Hand();
			Assert.IsTrue(WinningHands.HandCheckFuncs[HandTypes.WinningHand.StraightFlush](hand, bestHand));
		}

		[Test]
		public void StraightFlushTest2()
		{
			var hand = new Hand
			{
				new Card(1, Card.CardSuit.Diamonds),
				new Card(13, Card.CardSuit.Diamonds),
				new Card(12, Card.CardSuit.Diamonds),
				new Card(11, Card.CardSuit.Diamonds),
				new Card(10, Card.CardSuit.Diamonds),
				new Card(9, Card.CardSuit.Diamonds),
				new Card(3, Card.CardSuit.Diamonds)
			};

			Hand bestHand = new Hand();
			var actual = WinningHands.HandCheckFuncs[HandTypes.WinningHand.RoyalFlush](hand, bestHand);
			Console.WriteLine(bestHand.ToString());
			Assert.IsTrue(actual);
		}

		[Test]
		public void StraightFlushTest3()
		{
			var hand = new Hand
			{
				new Card(1, Card.CardSuit.Spades),
				new Card(2, Card.CardSuit.Spades),
				new Card(3, Card.CardSuit.Spades),
				new Card(4, Card.CardSuit.Spades),
				new Card(5, Card.CardSuit.Spades),
				new Card(6, Card.CardSuit.Spades),
				new Card(7, Card.CardSuit.Diamonds)
			};

			Hand bestHand = new Hand();
			Assert.IsTrue(WinningHands.HandCheckFuncs[HandTypes.WinningHand.StraightFlush](hand, bestHand));
		}

		[Test]
		public void TwoPairTest1()
		{
			var hand = new Hand
			{
				new Card(1, Card.CardSuit.Diamonds),
				new Card(1, Card.CardSuit.Spades),
				new Card(12, Card.CardSuit.Diamonds),
				new Card(12, Card.CardSuit.Spades),
				new Card(10, Card.CardSuit.Diamonds),
				new Card(10, Card.CardSuit.Spades),
				new Card(11, Card.CardSuit.Diamonds),
			};

			Hand bestHand = new Hand();
			Hand expectedBest = new Hand
			{
				new Card(14, Card.CardSuit.Diamonds),
				new Card(14, Card.CardSuit.Spades),
				new Card(12, Card.CardSuit.Diamonds),
				new Card(12, Card.CardSuit.Spades)
			};
			Hand altExpectedBest = new Hand
			{
				new Card(1, Card.CardSuit.Diamonds),
				new Card(1, Card.CardSuit.Spades),
				new Card(12, Card.CardSuit.Diamonds),
				new Card(12, Card.CardSuit.Spades)
			};

			Assert.IsTrue(WinningHands.HandCheckFuncs[HandTypes.WinningHand.TwoPair](hand, bestHand));
			CollectionAssert.AreEquivalent(expectedBest, bestHand);
			CollectionAssert.AreEquivalent(altExpectedBest, bestHand);
		}

		[Test]
		public void TwoPairTest2()
		{
			var hand = new Hand
			{
				new Card(1, Card.CardSuit.Diamonds),
				new Card(1, Card.CardSuit.Spades),
				new Card(12, Card.CardSuit.Diamonds),
				new Card(12, Card.CardSuit.Spades),
				new Card(11, Card.CardSuit.Diamonds),
				new Card(11, Card.CardSuit.Spades),
				new Card(10, Card.CardSuit.Diamonds),
			};

			Hand bestHand = new Hand();
			Hand expectedBest = new Hand
			{
				new Card(14, Card.CardSuit.Diamonds),
				new Card(14, Card.CardSuit.Spades),
				new Card(12, Card.CardSuit.Diamonds),
				new Card(12, Card.CardSuit.Spades)
			};
			Hand altExpectedBest = new Hand
			{
				new Card(1, Card.CardSuit.Diamonds),
				new Card(1, Card.CardSuit.Spades),
				new Card(12, Card.CardSuit.Diamonds),
				new Card(12, Card.CardSuit.Spades)
			};

			Assert.IsTrue(WinningHands.HandCheckFuncs[HandTypes.WinningHand.TwoPair](hand, bestHand));
			CollectionAssert.AreEquivalent(expectedBest, bestHand);
			CollectionAssert.AreEquivalent(altExpectedBest, bestHand);
		}

		[Test]
		public void TwoPairTest3()
		{
			var hand = new Hand
			{
				new Card(1, Card.CardSuit.Diamonds),
				new Card(1, Card.CardSuit.Spades),
				new Card(12, Card.CardSuit.Diamonds),
				new Card(12, Card.CardSuit.Spades),
				new Card(6, Card.CardSuit.Diamonds),
				new Card(11, Card.CardSuit.Spades),
				new Card(10, Card.CardSuit.Diamonds),
			};

			Hand bestHand = new Hand();
			Hand expectedBest = new Hand
			{
				new Card(14, Card.CardSuit.Diamonds),
				new Card(14, Card.CardSuit.Spades),
				new Card(12, Card.CardSuit.Diamonds),
				new Card(12, Card.CardSuit.Spades)
			};

			Assert.IsTrue(WinningHands.HandCheckFuncs[HandTypes.WinningHand.TwoPair](hand, bestHand));
			CollectionAssert.AreEquivalent(expectedBest, bestHand);
		}

		[Test]
		public void PairTest1()
		{
			//PokerHand test1 = new PokerHand("AD TS QD JS 6D 7S AS");
			//PokerHand.HandReducer(test1);
			//char[] seq = new char[] { 'A', 'A', 'Q', 'J', 'T' };

			var hand = new Hand
			{
				new Card(1, Card.CardSuit.Diamonds),
				new Card(10, Card.CardSuit.Spades),
				new Card(12, Card.CardSuit.Diamonds),
				new Card(11, Card.CardSuit.Spades),
				new Card(6, Card.CardSuit.Diamonds),
				new Card(7, Card.CardSuit.Spades),
				new Card(1, Card.CardSuit.Spades),
			};

			Hand bestHand = new Hand();
			Hand expectedBest = new Hand
			{
				new Card(14, Card.CardSuit.Diamonds),
				new Card(14, Card.CardSuit.Spades),
			};

			Assert.IsTrue(WinningHands.HandCheckFuncs[HandTypes.WinningHand.Pair](hand, bestHand));
			CollectionAssert.AreEquivalent(expectedBest, bestHand);
		}

		[Test]
		public void PairTest2()
		{
			//PokerHand test1 = new PokerHand("AD TS QD JS 6D 2H 2S");
			//PokerHand.HandReducer(test1);
			//char[] seq = new char[] { '2', '2', 'A', 'Q', 'J' };


			var hand = new Hand
			{
				new Card(1, Card.CardSuit.Diamonds),
				new Card(10, Card.CardSuit.Spades),
				new Card(12, Card.CardSuit.Diamonds),
				new Card(11, Card.CardSuit.Spades),
				new Card(6, Card.CardSuit.Diamonds),
				new Card(2, Card.CardSuit.Hearts),
				new Card(2, Card.CardSuit.Spades),
			};

			Hand bestHand = new Hand();
			Hand expectedBest = new Hand
			{
				new Card(2, Card.CardSuit.Hearts),
				new Card(2, Card.CardSuit.Spades)
			};

			Assert.IsTrue(WinningHands.HandCheckFuncs[HandTypes.WinningHand.Pair](hand, bestHand));
			CollectionAssert.AreEquivalent(expectedBest, bestHand);
		}

		[Test]
		public void FullTest1()
		{
			//PokerHand test1 = new PokerHand("AD AS AH 2S 2D JS JD");
			//PokerHand.HandReducer(test1);
			//char[] seq = new char[] { 'A', 'A', 'A', 'J', 'J' };


			var hand = new Hand
			{
				new Card(1, Card.CardSuit.Diamonds),
				new Card(1, Card.CardSuit.Spades),
				new Card(1, Card.CardSuit.Hearts),
				new Card(2, Card.CardSuit.Spades),
				new Card(2, Card.CardSuit.Diamonds),
				new Card(11, Card.CardSuit.Spades),
				new Card(11, Card.CardSuit.Diamonds),
			};

			Hand bestHand = new Hand();
			Hand expectedBest = new Hand
			{
				new Card(14, Card.CardSuit.Diamonds),
				new Card(14, Card.CardSuit.Hearts),
				new Card(14, Card.CardSuit.Spades),
				new Card(11, Card.CardSuit.Diamonds),
				new Card(11, Card.CardSuit.Spades)
			};

			Assert.IsTrue(WinningHands.HandCheckFuncs[HandTypes.WinningHand.FullHouse](hand, bestHand));
			CollectionAssert.AreEquivalent(expectedBest, bestHand);
		}

		[Test]
		public void FullTest2()
		{
			//PokerHand test1 = new PokerHand("AD TS AH TD 2D 2S AS");
			//PokerHand.HandReducer(test1);
			//char[] seq = new char[] { 'A', 'A', 'A', 'T', 'T' };

			var hand = new Hand
			{
				new Card(1, Card.CardSuit.Diamonds),
				new Card(10, Card.CardSuit.Spades),
				new Card(1, Card.CardSuit.Hearts),
				new Card(10, Card.CardSuit.Diamonds),
				new Card(2, Card.CardSuit.Diamonds),
				new Card(2, Card.CardSuit.Spades),
				new Card(1, Card.CardSuit.Spades),
			};

			Hand bestHand = new Hand();
			Hand expectedBest = new Hand
			{
				new Card(14, Card.CardSuit.Diamonds),
				new Card(14, Card.CardSuit.Hearts),
				new Card(14, Card.CardSuit.Spades),
				new Card(10, Card.CardSuit.Diamonds),
				new Card(10, Card.CardSuit.Spades)
			};

			Assert.IsTrue(WinningHands.HandCheckFuncs[HandTypes.WinningHand.FullHouse](hand, bestHand));
			CollectionAssert.AreEquivalent(expectedBest, bestHand);
		}

		[Test]
		public void FullTest3()
		{
			//PokerHand test1 = new PokerHand("TD TS 6D 2S 6D 2H 6S");
			//PokerHand.HandReducer(test1);
			//char[] seq = new char[] { '6', '6', '6', 'T', 'T' };

			var hand = new Hand
			{
				new Card(10, Card.CardSuit.Diamonds),
				new Card(10, Card.CardSuit.Spades),
				new Card(6, Card.CardSuit.Diamonds),
				new Card(2, Card.CardSuit.Spades),
				new Card(6, Card.CardSuit.Clubs),
				new Card(2, Card.CardSuit.Hearts),
				new Card(6, Card.CardSuit.Spades),
			};

			Hand bestHand = new Hand();
			Hand expectedBest = new Hand
			{
				new Card(10, Card.CardSuit.Diamonds),
				new Card(10, Card.CardSuit.Spades),
				new Card(6, Card.CardSuit.Clubs),
				new Card(6, Card.CardSuit.Diamonds),
				new Card(6, Card.CardSuit.Spades)
			};

			Assert.IsTrue(WinningHands.HandCheckFuncs[HandTypes.WinningHand.FullHouse](hand, bestHand));
			CollectionAssert.AreEquivalent(expectedBest, bestHand);
		}
	}
}