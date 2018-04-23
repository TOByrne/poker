using NUnit.Framework;

namespace Poker.Tests
{
	[TestFixture]
	public class WinningHandTests
	{
		[Test]
		public void TestHighCard()
		{
			var hand = new Hand()
			{
				new Card(1, Card.CardSuit.Clubs),
				new Card(10, Card.CardSuit.Diamonds),
				new Card(5, Card.CardSuit.Diamonds),
				new Card(4, Card.CardSuit.Hearts),
				new Card(3, Card.CardSuit.Spades)
			};

			var isHighCard = WinningHands.HighCard(hand);
			var highCard = new Card(10, Card.CardSuit.Diamonds);
			var actualHighCard = WinningHands.GetHighCard(hand);

			Assert.IsTrue(isHighCard);
			Assert.AreEqual(highCard, actualHighCard);
		}

		[Test]
		public void TestPair()
		{
			var hand = new Hand()
			{
				new Card(2, Card.CardSuit.Clubs),
				new Card(2, Card.CardSuit.Diamonds),
				new Card(5, Card.CardSuit.Diamonds),
				new Card(4, Card.CardSuit.Hearts),
				new Card(3, Card.CardSuit.Spades)
			};

			Assert.IsTrue(WinningHands.HandCheckFuncs[WinningHands.WinningHand.Pair](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.TwoPair](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.ThreeOfAKind](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.Straight](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.Flush](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.FullHouse](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.FourOfAKind](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.StraightFlush](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.RoyalFlush](hand));
		}

		[Test]
		public void TestTwoPair()
		{
			var hand = new Hand()
			{
				new Card(2, Card.CardSuit.Clubs),
				new Card(2, Card.CardSuit.Diamonds),
				new Card(5, Card.CardSuit.Diamonds),
				new Card(5, Card.CardSuit.Hearts),
				new Card(3, Card.CardSuit.Spades)
			};

			Assert.IsTrue(WinningHands.HandCheckFuncs[WinningHands.WinningHand.TwoPair](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.Pair](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.ThreeOfAKind](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.Straight](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.Flush](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.FullHouse](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.FourOfAKind](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.StraightFlush](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.RoyalFlush](hand));
		}

		[Test]
		public void TestThreeOfAKind()
		{
			var hand = new Hand()
			{
				new Card(2, Card.CardSuit.Clubs),
				new Card(2, Card.CardSuit.Diamonds),
				new Card(2, Card.CardSuit.Hearts),
				new Card(5, Card.CardSuit.Hearts),
				new Card(3, Card.CardSuit.Spades)
			};

			Assert.IsTrue(WinningHands.HandCheckFuncs[WinningHands.WinningHand.ThreeOfAKind](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.Pair](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.TwoPair](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.Straight](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.Flush](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.FullHouse](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.FourOfAKind](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.StraightFlush](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.RoyalFlush](hand));
		}

		[Test]
		public void TestStraight()
		{
			var hand = new Hand()
			{
				new Card(2, Card.CardSuit.Clubs),
				new Card(3, Card.CardSuit.Diamonds),
				new Card(5, Card.CardSuit.Hearts),
				new Card(6, Card.CardSuit.Hearts),
				new Card(4, Card.CardSuit.Spades)
			};

			Assert.IsTrue(WinningHands.HandCheckFuncs[WinningHands.WinningHand.Straight](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.Pair](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.TwoPair](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.ThreeOfAKind](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.Flush](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.FullHouse](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.FourOfAKind](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.StraightFlush](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.RoyalFlush](hand));
		}

		[Test]
		public void TestHighStraight()
		{
			var hand = new Hand()
			{
				new Card(1, Card.CardSuit.Clubs),
				new Card(10, Card.CardSuit.Diamonds),
				new Card(12, Card.CardSuit.Hearts),
				new Card(13, Card.CardSuit.Hearts),
				new Card(11, Card.CardSuit.Spades)
			};

			Assert.IsTrue(WinningHands.HandCheckFuncs[WinningHands.WinningHand.Straight](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.Pair](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.TwoPair](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.ThreeOfAKind](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.Flush](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.FullHouse](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.FourOfAKind](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.StraightFlush](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.RoyalFlush](hand));
		}

		[Test]
		public void TestFlush()
		{
			var hand = new Hand()
			{
				new Card(2, Card.CardSuit.Clubs),
				new Card(3, Card.CardSuit.Clubs),
				new Card(10, Card.CardSuit.Clubs),
				new Card(6, Card.CardSuit.Clubs),
				new Card(8, Card.CardSuit.Clubs)
			};

			Assert.IsTrue(WinningHands.HandCheckFuncs[WinningHands.WinningHand.Flush](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.Pair](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.TwoPair](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.ThreeOfAKind](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.Straight](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.FullHouse](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.FourOfAKind](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.StraightFlush](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.RoyalFlush](hand));
		}

		[Test]
		public void TestFullHouse()
		{
			var hand = new Hand()
			{
				new Card(2, Card.CardSuit.Clubs),
				new Card(3, Card.CardSuit.Diamonds),
				new Card(2, Card.CardSuit.Diamonds),
				new Card(3, Card.CardSuit.Hearts),
				new Card(2, Card.CardSuit.Hearts)
			};

			Assert.IsTrue(WinningHands.HandCheckFuncs[WinningHands.WinningHand.FullHouse](hand));
			Assert.IsTrue(WinningHands.HandCheckFuncs[WinningHands.WinningHand.Pair](hand));
			Assert.IsTrue(WinningHands.HandCheckFuncs[WinningHands.WinningHand.TwoPair](hand));
			Assert.IsTrue(WinningHands.HandCheckFuncs[WinningHands.WinningHand.ThreeOfAKind](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.Straight](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.Flush](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.FourOfAKind](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.StraightFlush](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.RoyalFlush](hand));
		}

		[Test]
		public void TestFourOfAKind()
		{
			var hand = new Hand()
			{
				new Card(2, Card.CardSuit.Clubs),
				new Card(2, Card.CardSuit.Diamonds),
				new Card(2, Card.CardSuit.Spades),
				new Card(3, Card.CardSuit.Hearts),
				new Card(2, Card.CardSuit.Hearts)
			};

			Assert.IsTrue(WinningHands.HandCheckFuncs[WinningHands.WinningHand.FourOfAKind](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.Pair](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.TwoPair](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.ThreeOfAKind](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.Straight](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.Flush](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.FullHouse](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.StraightFlush](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.RoyalFlush](hand));
		}

		[Test]
		public void TestStraightFlush()
		{
			var hand = new Hand()
			{
				new Card(2, Card.CardSuit.Clubs),
				new Card(3, Card.CardSuit.Clubs),
				new Card(4, Card.CardSuit.Clubs),
				new Card(5, Card.CardSuit.Clubs),
				new Card(6, Card.CardSuit.Clubs)
			};

			Assert.IsTrue(WinningHands.HandCheckFuncs[WinningHands.WinningHand.StraightFlush](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.Pair](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.TwoPair](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.ThreeOfAKind](hand));
			Assert.IsTrue(WinningHands.HandCheckFuncs[WinningHands.WinningHand.Straight](hand));
			Assert.IsTrue(WinningHands.HandCheckFuncs[WinningHands.WinningHand.Flush](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.FullHouse](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.FourOfAKind](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.RoyalFlush](hand));
		}

		[Test]
		public void TestRoyalFlush()
		{
			var hand = new Hand()
			{
				new Card(1, Card.CardSuit.Hearts),
				new Card(13, Card.CardSuit.Hearts),
				new Card(11, Card.CardSuit.Hearts),
				new Card(10, Card.CardSuit.Hearts),
				new Card(12, Card.CardSuit.Hearts)
			};

			Assert.IsTrue(WinningHands.HandCheckFuncs[WinningHands.WinningHand.RoyalFlush](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.Pair](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.TwoPair](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.ThreeOfAKind](hand));
			Assert.IsTrue(WinningHands.HandCheckFuncs[WinningHands.WinningHand.Straight](hand));
			Assert.IsTrue(WinningHands.HandCheckFuncs[WinningHands.WinningHand.Flush](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.FullHouse](hand));
			Assert.IsFalse(WinningHands.HandCheckFuncs[WinningHands.WinningHand.FourOfAKind](hand));
			Assert.IsTrue(WinningHands.HandCheckFuncs[WinningHands.WinningHand.StraightFlush](hand));
		}
	}
}