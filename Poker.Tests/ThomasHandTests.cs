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
			Assert.IsTrue(WinningHands.HandCheckFuncs[WinningHands.WinningHand.StraightFlush](hand, bestHand));
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
			var actual = WinningHands.HandCheckFuncs[WinningHands.WinningHand.RoyalFlush](hand, bestHand);
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
			Assert.IsTrue(WinningHands.HandCheckFuncs[WinningHands.WinningHand.StraightFlush](hand, bestHand));
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
			Assert.IsTrue(WinningHands.HandCheckFuncs[WinningHands.WinningHand.TwoPair](hand, bestHand));
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
			Assert.IsTrue(WinningHands.HandCheckFuncs[WinningHands.WinningHand.TwoPair](hand, bestHand));
		}

		/*
		[Test]
		public void TwoPairTest3()
		{
			PokerHand test1 = new PokerHand("AD AS QD QS 6D JS TD");
			PokerHand.HandReducer(test1);
			char[] seq = new char[] { 'A', 'A', 'Q', 'Q', 'J' };
			for (int u = 0; u < test1.straightArr.Length; u++)
			{

				Assert.AreEqual(test1.straightArr[u], seq.ToArray()[u]);
			}

		}

		[Test]
		public void PairTest1()
		{
			PokerHand test1 = new PokerHand("AD TS QD JS 6D 7S AS");
			PokerHand.HandReducer(test1);
			char[] seq = new char[] { 'A', 'A', 'Q', 'J', 'T' };
			for (int u = 0; u < test1.straightArr.Length; u++)
			{

				Assert.AreEqual(test1.straightArr[u], seq.ToArray()[u]);
			}

		}

		[Test]
		public void PairTest2()
		{
			PokerHand test1 = new PokerHand("AD TS QD JS 6D 2H 2S");
			PokerHand.HandReducer(test1);
			char[] seq = new char[] { '2', '2', 'A', 'Q', 'J' };
			for (int u = 0; u < test1.straightArr.Length; u++)
			{

				Assert.AreEqual(test1.straightArr[u], seq.ToArray()[u]);
			}

		}

		[Test]
		public void FullTest1()
		{
			PokerHand test1 = new PokerHand("AD AS AH 2S 2D JS JD");
			PokerHand.HandReducer(test1);
			char[] seq = new char[] { 'A', 'A', 'A', 'J', 'J' };
			for (int u = 0; u < test1.straightArr.Length; u++)
			{

				Assert.AreEqual(test1.straightArr[u], seq.ToArray()[u]);
			}

		}

		[Test]
		public void FullTest2()
		{
			PokerHand test1 = new PokerHand("AD TS AH TD 2D 2S AS");
			PokerHand.HandReducer(test1);
			char[] seq = new char[] { 'A', 'A', 'A', 'T', 'T' };
			for (int u = 0; u < test1.straightArr.Length; u++)
			{

				Assert.AreEqual(test1.straightArr[u], seq.ToArray()[u]);
			}

		}

		[Test]
		public void FullTest3()
		{
			PokerHand test1 = new PokerHand("TD TS 6D 2S 6D 2H 6S");
			PokerHand.HandReducer(test1);
			char[] seq = new char[] { '6', '6', '6', 'T', 'T' };
			for (int u = 0; u < test1.straightArr.Length; u++)
			{

				Assert.AreEqual(test1.straightArr[u], seq.ToArray()[u]);
			}

		}
		*/
	}
}