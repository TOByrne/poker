using System;
using System.Collections.Generic;
using System.Linq;

namespace Poker
{
	public static class WinningHands
	{
		public static Dictionary<HandTypes.WinningHand, Func<Hand, Hand, bool>> HandCheckFuncs = new Dictionary<HandTypes.WinningHand, Func<Hand, Hand, bool>>
		{
			{HandTypes.WinningHand.RoyalFlush, RoyalFlush},
			{HandTypes.WinningHand.StraightFlush, StraightFlush},
			{HandTypes.WinningHand.FourOfAKind, FourOfAKind},
			{HandTypes.WinningHand.FullHouse, FullHouse},
			{HandTypes.WinningHand.Flush, Flush},
			{HandTypes.WinningHand.Straight, Straight},
			{HandTypes.WinningHand.ThreeOfAKind, ThreeOfAKind},
			{HandTypes.WinningHand.TwoPair, TwoPair},
			{HandTypes.WinningHand.Pair, Pair},
			{HandTypes.WinningHand.HighCard, HighCard}
		};

		//	This is the order in which we'd check
		public static HandTypes.WinningHand[] Hands = {
			HandTypes.WinningHand.RoyalFlush, HandTypes.WinningHand.StraightFlush, HandTypes.WinningHand.FourOfAKind,
			HandTypes.WinningHand.FullHouse, HandTypes.WinningHand.Flush, HandTypes.WinningHand.Straight,
			HandTypes.WinningHand.ThreeOfAKind, HandTypes.WinningHand.TwoPair, HandTypes.WinningHand.Pair, HandTypes.WinningHand.HighCard
		};

		private static bool RoyalFlush(Hand hand, Hand bestHand)
		{
			if (hand.Count < 5) return false;

			// check if flush and high straight are true
			var isFlush = Flush(hand, bestHand);
			var isHighStraight = HighStraight(hand, bestHand);
			return isFlush && isHighStraight;
		}

		private static bool StraightFlush(Hand hand, Hand bestHand)
		{
			if (hand.Count < 5) return false;

			var checkHand = hand.Copy();
			// check if flush and straight are true.
			var bestFlush = new Hand();
			var bestStraight = new Hand();

			var hasFlush = false;
			var hasStraight = false;

			while (checkHand.Count() > 4 && !(hasFlush && hasStraight))
			{
				hasFlush = Flush(checkHand, bestFlush);

				//	Make sure any ACEs are accounted for on both ends
				var ace = GetHighCard(bestFlush);
				if (ace != null && ace.Value == 14)
				{
					bestFlush.Add(new Card(1, ace.Suit));
				}

				hasStraight = Straight(bestFlush, bestStraight);

				checkHand.Remove(GetHighCard(checkHand));
			}
 
			bestHand.Clear();
			bestHand.AddRange(bestFlush);

			return hasFlush && hasStraight;
		}

		private static bool FourOfAKind(Hand hand, Hand bestHand)
		{
			if (hand.Count < 4) return false;

			//see if exactly 4 cards card the same rank.
			var groups = hand.GroupBy(card => card.Value);
			var fours = groups.Where(group => group.Count() == 4);
			var hasFourOfAKind = fours.Any();

			var orderedFours = fours.OrderByDescending(p => p.Key);
			var highestFours = orderedFours.Take(1);
			var bestCards = highestFours.SelectMany(g => g.Take(g.Count()));

			bestHand.Clear();
			bestHand.AddRange(bestCards);

			return hasFourOfAKind;
		}

		private static bool FullHouse(Hand hand, Hand bestHand)
		{
			if (hand.Count < 5) return false;

			// check if trips and pair is true
			Hand bestPair = new Hand()
				, bestTrip = new Hand();
			var hasPair = Pair(hand, bestPair);
			var hasTrip = ThreeOfAKind(hand, bestTrip);

			bestHand.Clear();
			bestHand.AddRange(bestPair);
			bestHand.AddRange(bestTrip);

			return hasPair && hasTrip;
		}

		private static bool Flush(Hand hand, Hand bestHand)
		{
			if (hand.Count < 5) return false;

			var checkHand = hand.Copy();
			var hasAce = checkHand.Count(c => c.Value == 1) > 0;

			//	Create a copy of the Ace but use the high-value
			if (hasAce)
			{
				var aces = checkHand.Where(c => c.IsAce).ToList();
				foreach (var ace in aces)
				{
					checkHand.Add(new Card(14, ace.Suit));
				}
			}

			//see if 5 or more cards card the same rank.
			var ordered = checkHand.OrderByDescending(a => a.Value).ToList();
			var suitGroups = ordered.GroupBy(card => card.Suit);
			var hasFlush = suitGroups.Count(group => group.Count() >= 5) == 1;

			var flush = suitGroups.Where(group => group.Count() >= 5);

			if (!flush.Any()) return false;

			flush = flush.OrderByDescending(group => group.OrderByDescending(c => c.Value));
			var bestFlush = flush.First().ToList();

			bestHand.Clear();
			bestHand.AddRange(bestFlush.Take(5));

			return hasFlush;
		}

		private static bool Straight(Hand hand, Hand bestHand)
		{
			if (hand.Count < 5) return false;

			var checkHand = hand.Copy();

			var bestLow = new Hand();
			var bestHigh = new Hand();

			var hasLowStraight = LowStraight(checkHand, bestLow);
			var hasHighStraight = HighStraight(checkHand, bestHigh);

			var hasStraight = HighestStraight(checkHand, bestHand);

			bestHand.Clear();
			if (hasHighStraight)
			{
				bestHand.AddRange(bestHigh);
			}
			else if (hasLowStraight)
			{
				bestHand.AddRange(bestLow);
			}

			return hasLowStraight || hasHighStraight;
		}

		private static bool HighestStraight(Hand hand, Hand bestHand)
		{
			if (hand.Count < 5) return false;

			var ordered = hand.Copy().OrderByDescending(a => a.Value).ToList();

			if (IsStraight(ordered))
			{
				return true;
			}

			for (var i = 0; i < ordered.Count - 5; i++)
			{
				var skipped = ordered.Skip(i);
				var possibleStraight = skipped.Take(5);
				if (IsStraight(possibleStraight))
				{
					bestHand.Clear();
					bestHand.AddRange(possibleStraight);

					return true;
				}
			}
			return false;
		}

		//	Anything where the Ace is not > King
		private static bool LowStraight(Hand hand, Hand bestHand)
		{
			if (hand.Count < 5) return false;

			//	order by descending ensures that the highest value is at the beginning
			//	of the list, making it easier to take the highest low straight.
			var ordered = hand.Copy().OrderByDescending(a => a.Value).ToList();

			//	remove duplicates from the ordered list.
			ordered = ordered.Distinct(new CardValueComparer()).ToList();
			if (ordered.Count > 5)
			{
				for (int i = 0; i < ordered.Count - 4; i++)
				{
					if (ordered[i].Value == ordered[i + 4].Value + 4)
					{
						bestHand.AddRange(ordered.Skip(i).Take(5));
						return true;
					}
				}
			}
			else if (ordered.Count == 5 && ordered[0].Value == ordered[4].Value + 4)
			{
				bestHand.AddRange(ordered.Take(5));
				return true;
			}
			return false;
		}

		//	Anything where the Ace is > King
		private static bool HighStraight(Hand hand, Hand bestHand)
		{
			if (hand.Count < 5) return false;

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
				bestHand.Clear();
				bestHand.AddRange(ordered.Take(5));
				return true;
			}
			return false;
		}

		public static bool IsStraight(IEnumerable<Card> cards)
		{
			if (cards.Count() < 5) return false;

			var hand = cards.ToList();
			var doubles = hand.GroupBy(card => card.Value).Count(group => group.Count() > 1) > 1;
			var inARow = hand.Count > 4 && hand[0].Value - hand[4].Value == 4;

			return !doubles && inARow;
		}

		private static bool ThreeOfAKind(Hand hand, Hand bestHand)
		{
			if (hand.Count < 3) return false;

			//see if there are 2 lots of exactly 2 cards card the same rank.
			var groups = hand.GroupBy(card => card.Value);
			var trip = groups.Where(group => group.Count() == 3);
			var hasTrip = trip.Any();

			//	High-ify any Aces
			foreach (var grouping in trip)
			{
				if (grouping.Key == 1)
				{
					grouping.ToList().ForEach(c => c.Value = 14);
				}
			}
			var orderedPairs = trip.OrderByDescending(p => p.Key);
			var highestPairs = orderedPairs.Take(2); //.SelectMany<Card>(c => c.GetEnumerator().Current);
			var bestCards = highestPairs.SelectMany(g => g.Take(g.Count()));

			bestHand.Clear();
			bestHand.AddRange(bestCards);

			return hasTrip;
		}

		private static bool TwoPair(Hand hand, Hand bestHand)
		{
			if (hand.Count < 4) return false;

			//see if there are 2 lots of exactly 2 cards card the same rank.
			var groups = hand.GroupBy(card => card.Value);
			var pairs = groups.Where(group => group.Count() == 2);
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
			if (hand.Count < 2) return false;

			//see if there are 2 lots of exactly 2 cards card the same rank.
			var groups = hand.GroupBy(card => card.Value);
			var pairs = groups.Where(group => group.Count() == 2);
			var hasPair = pairs.Any();

			//	High-ify any Aces
			foreach (var grouping in pairs)
			{
				if (grouping.Key == 1)
				{
					grouping.ToList().ForEach(c => c.Value = 14);
				}
			}
			var orderedPairs = pairs.OrderByDescending(p => p.Key);
			var highestPairs = orderedPairs.Take(1); //.SelectMany<Card>(c => c.GetEnumerator().Current);
			var bestCards = highestPairs.SelectMany(g => g.Take(g.Count()));

			bestHand.Clear();
			bestHand.AddRange(bestCards);

			return hasPair;
		}

		public static HandTypes.WinningHand FindWinningHand(Hand hand, Hand bestHand)
		{
			foreach (var winningHand in Hands)
			{
				if (HandCheckFuncs[winningHand](hand, bestHand))
					return winningHand;
			}

			return HandTypes.WinningHand.HighCard;
		}

		public static bool HighCard(Hand hand, Hand bestHand)
		{
			var thisIsHighestHand = false;

			foreach (var check in Hands)
			{
				if (check != HandTypes.WinningHand.HighCard)
				{
					thisIsHighestHand |= HandCheckFuncs[check](hand, bestHand);
				}
			}

			bestHand.Clear();
			bestHand.Add(GetHighCard(hand));

			return !thisIsHighestHand;
		}

		public static Card GetHighCard(Hand hand)
		{
			Card tempHighCard = null;
			foreach (var card in hand)
			{
				if (card.IsAce)
				{
					tempHighCard = card;
					break;
				}

				if (tempHighCard == null || card.Value > tempHighCard.Value)
				{
					tempHighCard = card;
				}
			}

			return tempHighCard;
		}
	}
}