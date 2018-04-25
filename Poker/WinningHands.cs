using System;
using System.Collections.Generic;
using System.Linq;

namespace Poker
{
	public static class WinningHands
	{
		public static Dictionary<WinningHand, Func<Hand, Hand, bool>> HandCheckFuncs = new Dictionary<WinningHand, Func<Hand, Hand, bool>>
		{
			{WinningHand.RoyalFlush, RoyalFlush},
			{WinningHand.StraightFlush, StraightFlush},
			{WinningHand.FourOfAKind, FourOfAKind},
			{WinningHand.FullHouse, FullHouse},
			{WinningHand.Flush, Flush},
			{WinningHand.Straight, Straight},
			{WinningHand.ThreeOfAKind, ThreeOfAKind},
			{WinningHand.TwoPair, TwoPair},
			{WinningHand.Pair, Pair}
		};

		//	These are in order.  Lower down the list is a better hand
		public enum WinningHand
		{
			//HighCard,
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

		public static WinningHand[] Hands = {
			WinningHand.RoyalFlush, WinningHand.StraightFlush, WinningHand.FourOfAKind,
			WinningHand.FullHouse, WinningHand.Flush, WinningHand.Straight,
			WinningHand.ThreeOfAKind, WinningHand.TwoPair, WinningHand.Pair
		};

		private static bool RoyalFlush(Hand hand, Hand bestHand)
		{
			// check if flush and high straight are true
			return Flush(hand, bestHand) && HighStraight(hand, bestHand);
		}

		private static bool StraightFlush(Hand hand, Hand bestHand)
		{
			// check if flush and straight are true.
			return Flush(hand, bestHand) && Straight(hand, bestHand);
		}

		private static bool FourOfAKind(Hand hand, Hand bestHand)
		{
			//see if exactly 4 cards card the same rank.
			return hand.GroupBy(card => card.Value).Any(group => group.Count() == 4);
		}

		private static bool FullHouse(Hand hand, Hand bestHand)
		{
			// check if trips and pair is true
			return Pair(hand, bestHand) && ThreeOfAKind(hand, bestHand);
		}

		private static bool Flush(Hand hand, Hand bestHand)
		{
			//see if 5 or more cards card the same rank.
			return hand.GroupBy(card => card.Suit).Count(group => group.Count() >= 5) == 1;
		}

		private static bool Straight(Hand hand, Hand bestHand)
		{
			return LowStraight(hand, bestHand) || HighStraight(hand, bestHand);
		}

		//	Anything where the Ace is not > King
		private static bool LowStraight(Hand hand, Hand bestHand)
		{
			var ordered = hand.OrderByDescending(a => a.Value).ToList();

			return ordered[0].Value == ordered[4].Value + 4;
		}

		//	Anything where the Ace is > King
		private static bool HighStraight(Hand hand, Hand bestHand)
		{
			//  Explicitly checking for a high-straight, so look for an ACE and set the value to 14
			var checkHand = hand.Copy();
			var hasAce = checkHand.Count(c => c.Value == 1) > 0;

			if (!hasAce) return false;

			foreach (var card in checkHand)
			{
				if (card.Value == 1)
				{
					card.Value = 14;
				}
			}
			var ordered = checkHand.OrderByDescending(a => a.Value).ToList();

			if (ordered[0].Value == ordered[4].Value + 4)
			{
				bestHand = checkHand;
				return true;
			}
			return false;
		}

		public static bool IsStraight(IEnumerable<Card> cards)
		{
			var hand = (List<Card>) cards;
			var doubles = hand.GroupBy(card => card.Value).Count(group => group.Count() > 1) > 1;
			var inARow = hand[4].Value - hand[0].Value == 5; //Ace is 0

			return !doubles && inARow;
		}

		private static bool ThreeOfAKind(Hand hand, Hand bestHand)
		{
			//see if exactly 3 cards card the same rank.
			return hand.GroupBy(card => card.Value).Any(group => group.Count() == 3);
		}

		private static bool TwoPair(Hand hand, Hand bestHand)
		{
			//see if there are 2 lots of exactly 2 cards card the same rank.
			var groups = hand.GroupBy(card => card.Value);
			var pairs = groups.Where(group => group.Count() > 1);
			var hasTwoPair = pairs.Count() >= 2;

			//	High-ify any Aces
			foreach (var grouping in pairs)
			{
				if (grouping.Key == 1)
				{
					grouping.ToList().ForEach(c => c.Value = 14);
				}
			}
			var orderedPairs = pairs.OrderByDescending(p => p.Key);
			var highestPairs = orderedPairs.Take(2); //.SelectMany<Card>(c => c.GetEnumerator().Current);
			var bestCards = highestPairs.SelectMany(g => g.Take(g.Count()));

			bestHand.Clear();
			bestHand.AddRange(bestCards);

			return hasTwoPair;
		}

		private static bool Pair(Hand hand, Hand bestHand)
		{
			//see if exactly 2 cards card the same rank.
			return hand.GroupBy(card => card.Value).Count(group => group.Count() == 2) == 1;
		}

		public static bool HighCard(Hand hand)
		{
			var thisIsHighestHand = false;
			Hand bestHand = new Hand();
			foreach (var check in Hands)
			{
				thisIsHighestHand |= HandCheckFuncs[check](hand, bestHand);
			}

			return !thisIsHighestHand;
		}

		public static Card GetHighCard(Hand hand)
		{
			Card tempHighCard = null;
			foreach (var card in hand)
			{
				if (tempHighCard == null || card.Value > tempHighCard.Value)
				{
					tempHighCard = card;
				}
			}

			return tempHighCard;
		}
	}
}