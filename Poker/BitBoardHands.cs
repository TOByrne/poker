using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Net.Http;

namespace Poker
{
	public static class BitBoardHands
	{
		public static Dictionary<HandTypes.WinningHand, Func<BitHand, BitHand, bool>> HandCheckFuncs = new Dictionary<HandTypes.WinningHand, Func<BitHand, BitHand, bool>>
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

		public static Dictionary<Card.CardSuit, ulong> SuitCards = new Dictionary<Card.CardSuit, ulong>
		{
			{Card.CardSuit.Clubs,       0x0000000000003fff},
			{Card.CardSuit.Diamonds,    0x000000003fff0000},
			{Card.CardSuit.Hearts,      0x00003fff00000000},
			{Card.CardSuit.Spades,      0x3fff000000000000},
		};

		public static bool RoyalFlush(BitHand startingHand, BitHand bestHand)
		{
			//  Royal Flush:
			//  0011 1110 0000 0000     0011 1110 0000 0000     0011 1110 0000 0000     0011 1110 0000 0000
			//  3    e    0    0        3    e    0    0        3    e    0    0        3    e    0    0
			const ulong flush = 0x3e003e003e003e00;

			//  Removes all the non-royal-flush cards
			var comparison = startingHand & flush;

			//  After removing all the non-royal-flush cards, do we have a royal flush in any suit?
			return (comparison | flush) == flush;
		}

		public static bool StraightFlush(BitHand startingHand, BitHand bestHand)
		{
			var hand = startingHand.Hand;

			var clubCards = hand & SuitCards[Card.CardSuit.Clubs];
			var diamondCards = hand & SuitCards[Card.CardSuit.Diamonds];
			var heartCards = hand & SuitCards[Card.CardSuit.Hearts];
			var spadeCards = hand & SuitCards[Card.CardSuit.Spades];

			//  0011 1110 0000 0000
			ulong flushMask = 0x3e003e003e003e00;

			while (flushMask > 0x001f001f001f001f)
			{
				var possibleClubFlush = flushMask & clubCards;
				var possibleDiamondFlush = flushMask & diamondCards;
				var possibleHeartFlush = flushMask & heartCards;
				var possibleSpadeFlush = flushMask & spadeCards;

				var handFlushMask = possibleClubFlush | possibleDiamondFlush | possibleHeartFlush |
									possibleSpadeFlush;

				var handFlush = handFlushMask & startingHand;

				if (handFlush == startingHand)
				{
					bestHand.Reset(handFlush);
					return true;
				}

				flushMask = flushMask >> 1;
			}

			return false;
		}

		public static bool FourOfAKind(BitHand startingHand, BitHand bestHand)
		{
			var hand = startingHand.Hand;

			var clubCards = GetClubs(hand);
			var diamondCards = GetDiamonds(hand);
			var heartCards = GetHearts(hand);
			var spadeCards = GetSpades(hand);

			var hasFour = (clubCards & diamondCards & heartCards & spadeCards) > 0;

			return hasFour;
		}

		public static bool FullHouse(BitHand startingHand, BitHand bestHand)
		{
			var hand = startingHand.Hand;

			//  Full house = trip + pair...

			var bestTrip = new BitHand();
			//  Find the trip,
			var hasTrip = ThreeOfAKind(startingHand, bestTrip);
			//  remove it,
			var remainingHand = hand ^ bestTrip.Hand;

			var bestPair = new BitHand();
			//  find a pair
			var hasPair = Pair(new BitHand(remainingHand), bestPair);

			return hasTrip && hasPair;
		}

		public static bool Flush(BitHand startingHand, BitHand bestHand)
		{
			throw new NotImplementedException();
		}

		public static bool Straight(BitHand startingHand, BitHand bestHand)
		{
			//  Start with a royal-straight.
			ulong straight = 0x3f00;

			//  consider just the numbers - OR the hand with itself disregarding the suit...
			//  Any card we have is basically replicated across all suits
			ulong allNums = startingHand.Hand
				| startingHand.Hand >> 16
				| startingHand.Hand >> 32
				| startingHand.Hand >> 48;

			//  The 'straight' variable is just starting from the last 4 hex digits, so
			//  there's nothing too excessive going on here.
			//  right shift until it matches or until there are no more possible matches.
			do
			{
				if ((straight | allNums) == allNums)
				{
					return true;
				}
				straight = straight >> 1;
			} while (straight > 0);
			return false;
		}

		public static bool ThreeOfAKind(BitHand startingHand, BitHand bestHand)
		{
			var hand = startingHand.Hand;

			var clubCards = GetClubs(hand);
			var diamondCards = GetDiamonds(hand);
			var heartCards = GetHearts(hand);
			var spadeCards = GetSpades(hand);

			var cdh = (clubCards & diamondCards & heartCards);
			var cds = (clubCards & diamondCards & spadeCards);
			var chs = (clubCards & heartCards & spadeCards);
			var dhs = (diamondCards & heartCards & spadeCards);

			var trip = (cdh | cds | chs | dhs);
			var hasTrip = trip > 0;

			if (hasTrip)
			{
				//  getting the max of the various cdh, cds, etc isn't going to work since they're basically just
				//  numbers without suits.  They've got no real context.  maxTrip just gives me a place to start.
				//  the Trip var tells us which card is triplicated, so we use that to create a mask to AND against
				//  the original hand to give us the three of a kind in the hand.
				var tripMask = (trip) | (trip << 16) | (trip << 32) | (trip << 48);

				var tripCards = tripMask & startingHand;
				bestHand.Reset(tripCards);
			}
			return hasTrip;
		}

		public static bool TwoPair(BitHand startingHand, BitHand bestHand)
		{
			//  Start with the high ACE, in all suits.  Shift right and see if there's a Pair.
			//  If there is, then add it to a list and keep shifting until the next pair is found

			var pairs = new List<BitHand>();
			ulong mask = 0x2000200020002000;

			while (pairs.Count < 2 && mask > 0x0001000100010001)
			{
				var testHand = new BitHand(startingHand & mask);
				var isPair = Pair(testHand, bestHand);

				if (isPair)
				{
					pairs.Add(bestHand);
				}

				mask = mask >> 1;
			}

			return pairs.Count > 1;
		}

		public static bool Pair(BitHand startingHand, BitHand bestHand)
		{
			var hand = startingHand.Hand;

			var clubCards = GetClubs(hand);
			var diamondCards = GetDiamonds(hand);
			var heartCards = GetHearts(hand);
			var spadeCards = GetSpades(hand);

			ulong cd = clubCards & diamondCards,
				ch = clubCards & heartCards,
				cs = clubCards & spadeCards,
				dh = diamondCards & heartCards,
				ds = diamondCards & spadeCards,
				hs = heartCards & spadeCards;

			var pair = cd | ch | cs | dh | ds | hs;
			var hasPair = pair > 0;

			if (hasPair)
			{
				var pairMask = (pair) | (pair << 16) | (pair << 32) | (pair << 48);

				var pairCards = pairMask & startingHand;
				bestHand.Reset(pairCards);
			}

			return hasPair;
		}

		public static bool HighCard(BitHand startingHand, BitHand bestHand)
		{
			throw new NotImplementedException();
		}

		private static ulong GetSpades(ulong hand)
		{
			return (hand & SuitCards[Card.CardSuit.Spades]) >> 48;
		}

		private static ulong GetHearts(ulong hand)
		{
			return (hand & SuitCards[Card.CardSuit.Hearts]) >> 32;
		}

		private static ulong GetDiamonds(ulong hand)
		{
			return (hand & SuitCards[Card.CardSuit.Diamonds]) >> 16;
		}

		private static ulong GetClubs(ulong hand)
		{
			return (hand & SuitCards[Card.CardSuit.Clubs]);
		}

		//private ulong getMax(ulong a, ulong b, ulong c)
		//{
		//    int temp = a ^ ((a ^ b) & -(a < b));
		//    int r = c ^ ((c ^ temp) & -(c < temp));
		//    return r;
		//}
	}
}