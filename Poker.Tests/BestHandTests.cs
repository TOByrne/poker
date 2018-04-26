using NUnit.Framework;

namespace Poker.Tests
{
	[TestFixture]
	public class BestHandTests
	{
		[Test]
		public void HighCard()
		{
			var hand = new Hand
			{
				new Card(1, Card.CardSuit.Clubs),
				new Card(2, Card.CardSuit.Diamonds),
				new Card(4, Card.CardSuit.Clubs),
				new Card(10, Card.CardSuit.Clubs),
			};

			var bestHand = new Hand();
			var expectedWin = WinningHands.WinningHand.HighCard;
			var expectedBest = new Hand
			{
				new Card(1, Card.CardSuit.Clubs),
			};

			var actualWin = WinningHands.FindWinningHand(hand, bestHand);

			Assert.AreEqual(expectedWin, actualWin);
			CollectionAssert.AreEquivalent(expectedBest, bestHand);
		}

		[Test]
		public void Pair()
		{
			var hand = new Hand
			{
				new Card(2, Card.CardSuit.Diamonds),
				new Card(4, Card.CardSuit.Clubs),
				new Card(4, Card.CardSuit.Diamonds),
				new Card(10, Card.CardSuit.Clubs),
			};

			var bestHand = new Hand();
			var expectedWin = WinningHands.WinningHand.Pair;
			var expectedBest = new Hand
			{
				new Card(4, Card.CardSuit.Clubs),
				new Card(4, Card.CardSuit.Diamonds),
			};

			var actualWin = WinningHands.FindWinningHand(hand, bestHand);

			Assert.AreEqual(expectedWin, actualWin);
			CollectionAssert.AreEquivalent(expectedBest, bestHand);
		}

		[Test]
		public void TwoPair()
		{
			var hand = new Hand
			{
				new Card(2, Card.CardSuit.Diamonds),
				new Card(2, Card.CardSuit.Hearts),
				new Card(4, Card.CardSuit.Clubs),
				new Card(4, Card.CardSuit.Diamonds),
				new Card(10, Card.CardSuit.Hearts),
				new Card(10, Card.CardSuit.Clubs),
			};

			var bestHand = new Hand();
			var expectedWin = WinningHands.WinningHand.TwoPair;
			var expectedBest = new Hand
			{
				new Card(4, Card.CardSuit.Clubs),
				new Card(4, Card.CardSuit.Diamonds),
				new Card(10, Card.CardSuit.Hearts),
				new Card(10, Card.CardSuit.Clubs),
			};

			var actualWin = WinningHands.FindWinningHand(hand, bestHand);

			Assert.AreEqual(expectedWin, actualWin);
			CollectionAssert.AreEquivalent(expectedBest, bestHand);
		}

		[Test]
		public void ThreeOfAKind()
		{
			var hand = new Hand
			{
				new Card(2, Card.CardSuit.Diamonds),
				new Card(4, Card.CardSuit.Diamonds),
				new Card(10, Card.CardSuit.Clubs),
				new Card(10, Card.CardSuit.Hearts),
				new Card(10, Card.CardSuit.Clubs),
			};

			var bestHand = new Hand();
			var expectedWin = WinningHands.WinningHand.ThreeOfAKind;
			var expectedBest = new Hand
			{
				new Card(10, Card.CardSuit.Clubs),
				new Card(10, Card.CardSuit.Hearts),
				new Card(10, Card.CardSuit.Clubs),
			};

			var actualWin = WinningHands.FindWinningHand(hand, bestHand);

			Assert.AreEqual(expectedWin, actualWin);
			CollectionAssert.AreEquivalent(expectedBest, bestHand);
		}

		[Test]
		public void Straight()
		{
			var hand = new Hand
			{
				new Card(2, Card.CardSuit.Diamonds),
				new Card(3, Card.CardSuit.Diamonds),
				new Card(6, Card.CardSuit.Spades),
				new Card(7, Card.CardSuit.Clubs),
				new Card(8, Card.CardSuit.Clubs),
				new Card(9, Card.CardSuit.Diamonds),
				new Card(10, Card.CardSuit.Clubs),
				new Card(12, Card.CardSuit.Hearts),
				new Card(13, Card.CardSuit.Clubs),
			};

			var bestHand = new Hand();
			var expectedWin = WinningHands.WinningHand.Straight;
			var expectedBest = new Hand
			{
				new Card(6, Card.CardSuit.Spades),
				new Card(7, Card.CardSuit.Clubs),
				new Card(8, Card.CardSuit.Clubs),
				new Card(9, Card.CardSuit.Diamonds),
				new Card(10, Card.CardSuit.Clubs),
			};

			var actualWin = WinningHands.FindWinningHand(hand, bestHand);

			Assert.AreEqual(expectedWin, actualWin);
			CollectionAssert.AreEquivalent(expectedBest, bestHand);
		}

		[Test]
		public void Flush()
		{
			var hand = new Hand
			{
				new Card(2, Card.CardSuit.Clubs),
				new Card(3, Card.CardSuit.Clubs),
				new Card(6, Card.CardSuit.Diamonds),
				new Card(6, Card.CardSuit.Hearts),
				new Card(6, Card.CardSuit.Spades),
				new Card(7, Card.CardSuit.Clubs),
				new Card(8, Card.CardSuit.Clubs),
				new Card(9, Card.CardSuit.Diamonds),
				new Card(10, Card.CardSuit.Clubs),
				new Card(12, Card.CardSuit.Hearts),
				new Card(13, Card.CardSuit.Clubs),
			};

			var bestHand = new Hand();
			var expectedWin = WinningHands.WinningHand.Flush;
			var expectedBest = new Hand
			{
				new Card(3, Card.CardSuit.Clubs),
				new Card(7, Card.CardSuit.Clubs),
				new Card(8, Card.CardSuit.Clubs),
				new Card(10, Card.CardSuit.Clubs),
				new Card(13, Card.CardSuit.Clubs),
			};

			var actualWin = WinningHands.FindWinningHand(hand, bestHand);

			Assert.AreEqual(expectedWin, actualWin);
			CollectionAssert.AreEquivalent(expectedBest, bestHand);
		}

		[Test]
		public void FullHouse()
		{
			var hand = new Hand
			{
				new Card(2, Card.CardSuit.Clubs),
				new Card(2, Card.CardSuit.Hearts),
				new Card(6, Card.CardSuit.Diamonds),
				new Card(6, Card.CardSuit.Hearts),
				new Card(6, Card.CardSuit.Spades),
				new Card(7, Card.CardSuit.Clubs),
				new Card(8, Card.CardSuit.Clubs),
				new Card(9, Card.CardSuit.Diamonds),
				new Card(10, Card.CardSuit.Clubs),
				new Card(12, Card.CardSuit.Hearts),
				new Card(13, Card.CardSuit.Clubs),
			};

			var bestHand = new Hand();
			var expectedWin = WinningHands.WinningHand.FullHouse;
			var expectedBest = new Hand
			{
				new Card(6, Card.CardSuit.Spades),
				new Card(6, Card.CardSuit.Diamonds),
				new Card(6, Card.CardSuit.Hearts),
				new Card(2, Card.CardSuit.Hearts),
				new Card(2, Card.CardSuit.Clubs),
			};

			var actualWin = WinningHands.FindWinningHand(hand, bestHand);

			Assert.AreEqual(expectedWin, actualWin);
			CollectionAssert.AreEquivalent(expectedBest, bestHand);
		}

		[Test]
		public void FourOfAKind()
		{
			var hand = new Hand
			{
				new Card(2, Card.CardSuit.Clubs),
				new Card(2, Card.CardSuit.Diamonds),
				new Card(2, Card.CardSuit.Hearts),
				new Card(2, Card.CardSuit.Spades),
				new Card(3, Card.CardSuit.Clubs),
				new Card(4, Card.CardSuit.Clubs),
				new Card(5, Card.CardSuit.Diamonds),
				new Card(6, Card.CardSuit.Clubs),
				new Card(6, Card.CardSuit.Diamonds),
				new Card(6, Card.CardSuit.Hearts),
				new Card(6, Card.CardSuit.Spades),
				new Card(7, Card.CardSuit.Clubs),
				new Card(8, Card.CardSuit.Clubs),
				new Card(9, Card.CardSuit.Diamonds),
				new Card(10, Card.CardSuit.Clubs),
				new Card(12, Card.CardSuit.Hearts),
				new Card(13, Card.CardSuit.Clubs),
			};

			var bestHand = new Hand();
			var expectedWin = WinningHands.WinningHand.FourOfAKind;
			var expectedBest = new Hand
			{
				new Card(6, Card.CardSuit.Clubs),
				new Card(6, Card.CardSuit.Diamonds),
				new Card(6, Card.CardSuit.Hearts),
				new Card(6, Card.CardSuit.Spades),
			};

			var actualWin = WinningHands.FindWinningHand(hand, bestHand);

			Assert.AreEqual(expectedWin, actualWin);
			CollectionAssert.AreEquivalent(expectedBest, bestHand);
		}

		[Test]
		public void StraightFlush()
		{
			var hand = new Hand
			{
				//	No ACE, but the best hand should be a Straight Flush.
				//	It also has the option to go with a Straight or a Flush
				//	but it should come back with the Straight Flush.  There's
				//	obviously the opportunity to go with a low straight flush
				//	but it should return the highest.  There are a few other
				//	things in there to try and trip it up
				new Card(2, Card.CardSuit.Clubs),
				new Card(3, Card.CardSuit.Clubs),
				new Card(4, Card.CardSuit.Clubs),
				new Card(5, Card.CardSuit.Clubs),
				new Card(6, Card.CardSuit.Clubs),
				new Card(7, Card.CardSuit.Clubs),
				new Card(8, Card.CardSuit.Clubs),
				new Card(9, Card.CardSuit.Clubs),
				new Card(10, Card.CardSuit.Clubs),
				new Card(10, Card.CardSuit.Diamonds),
				new Card(11, Card.CardSuit.Hearts),
				new Card(11, Card.CardSuit.Clubs),
				new Card(11, Card.CardSuit.Diamonds),
				new Card(12, Card.CardSuit.Hearts),
				new Card(13, Card.CardSuit.Clubs),
			};

			var bestHand = new Hand();
			var expectedWin = WinningHands.WinningHand.StraightFlush;
			var expectedBest = new Hand
			{
				new Card(11, Card.CardSuit.Clubs),
				new Card(10, Card.CardSuit.Clubs),
				new Card(9, Card.CardSuit.Clubs),
				new Card(8, Card.CardSuit.Clubs),
				new Card(7, Card.CardSuit.Clubs)
			};

			var actualWin = WinningHands.FindWinningHand(hand, bestHand);

			Assert.AreEqual(expectedWin, actualWin);
			CollectionAssert.AreEquivalent(expectedBest, bestHand);
		}

		[Test]
		public void RoyalFlush()
		{
			var hand = new Hand
			{
				new Card(1, Card.CardSuit.Clubs),
				new Card(2, Card.CardSuit.Clubs),
				new Card(3, Card.CardSuit.Clubs),
				new Card(4, Card.CardSuit.Clubs),
				new Card(5, Card.CardSuit.Clubs),
				new Card(6, Card.CardSuit.Clubs),
				new Card(7, Card.CardSuit.Clubs),
				new Card(8, Card.CardSuit.Clubs),
				new Card(9, Card.CardSuit.Clubs),
				new Card(10, Card.CardSuit.Clubs),
				new Card(11, Card.CardSuit.Clubs),
				new Card(12, Card.CardSuit.Clubs),
				new Card(13, Card.CardSuit.Clubs),
			};

			var bestHand = new Hand();
			var expectedWin = WinningHands.WinningHand.RoyalFlush;
			var expectedBest = new Hand
			{
				new Card(14, Card.CardSuit.Clubs),
				new Card(13, Card.CardSuit.Clubs),
				new Card(12, Card.CardSuit.Clubs),
				new Card(11, Card.CardSuit.Clubs),
				new Card(10, Card.CardSuit.Clubs)
			};

			var actualWin = WinningHands.FindWinningHand(hand, bestHand);

			Assert.AreEqual(expectedWin, actualWin);
			CollectionAssert.AreEquivalent(expectedBest, bestHand);
		}
	}
}