using System;
using System.Collections.Generic;
using System.Linq;

namespace Poker
{
	public static class WinningHands
	{
		public static Dictionary<WinningHand, Func<Hand, bool>> HandCheckFuncs = new Dictionary<WinningHand, Func<Hand, bool>>
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

		private static bool RoyalFlush(Hand hand)
		{
			// check if flush and high straight are true
			return Flush(hand) && HighStraight(hand);
		}

		private static bool StraightFlush(Hand hand)
		{
			// check if flush and straight are true.
			return Flush(hand) && Straight(hand);
		}

		private static bool FourOfAKind(Hand hand)
		{
			//see if exactly 4 cards card the same rank.
			return hand.GroupBy(card => card.Value).Any(group => group.Count() == 4);
		}

		private static bool FullHouse(Hand hand)
		{
			// check if trips and pair is true
			return Pair(hand) && ThreeOfAKind(hand);
		}

		private static bool Flush(Hand hand)
		{
			//see if 5 or more cards card the same rank.
			return hand.GroupBy(card => card.Suit).Count(group => group.Count() >= 5) == 1;
		}

		private static bool Straight(Hand hand)
		{
			return LowStraight(hand) || HighStraight(hand);
		}

		//	Anything where the Ace is not > King
		private static bool LowStraight(Hand hand)
		{
			var ordered = hand.OrderByDescending(a => a.Value).ToList();

			//	Any duplicates?
			//	if so, then it can't possibly be a straight.
			if (Pair(hand) || ThreeOfAKind(hand) || FourOfAKind(hand))
			{
				return false;
			}

			return ordered[0].Value == ordered[4].Value + 4;
		}

		//	Anything where the Ace is > King
		private static bool HighStraight(Hand hand)
		{
			var ordered = hand.OrderByDescending(a => a.Value).ToList();

			//	Any duplicates?
			//	if so, then it can't possibly be a straight.
			if (Pair(hand) || ThreeOfAKind(hand) || FourOfAKind(hand))
			{
				return false;
			}

			return ordered[0].Value == ordered[4].Value + 12;
		}

		public static bool IsStraight(IEnumerable<Card> cards)
		{
			var hand = (List<Card>) cards;
			var doubles = hand.GroupBy(card => card.Value).Count(group => group.Count() > 1) > 1;
			var inARow = hand[4].Value - hand[0].Value == 5; //Ace is 0

			return !doubles && inARow;
		}

		private static bool ThreeOfAKind(Hand hand)
		{
			//see if exactly 3 cards card the same rank.
			return hand.GroupBy(card => card.Value).Any(group => group.Count() == 3);
		}

		private static bool TwoPair(Hand hand)
		{
			//see if there are 2 lots of exactly 2 cards card the same rank.
			return hand.GroupBy(card => card.Value).Count(group => group.Count() >= 2) == 2;
		}

		private static bool Pair(Hand hand)
		{
			//see if exactly 2 cards card the same rank.
			return hand.GroupBy(card => card.Value).Count(group => group.Count() == 2) == 1;
		}

		public static bool HighCard(Hand hand)
		{
			var thisIsHighestHand = false;
			foreach (var check in Hands)
			{
				thisIsHighestHand |= HandCheckFuncs[check](hand);
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