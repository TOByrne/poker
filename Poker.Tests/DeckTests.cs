using System;
using NUnit.Framework;

namespace Poker.Tests
{
	[TestFixture]
	public class DeckTests
	{
		private Deck Deck { get; set; }
		private Deck Discards { get; set; }

		private Dealer Dealer { get; set; }

		public Card Card1 { get; set; }
		public Card Card2 { get; set; }

		public DeckTests()
		{
			Deck = new Deck();
			Discards = new Deck();

			Card1 = new Card
			{
				Suit = Card.CardSuit.Spades,
				Value = 1
			};
			Card2 = new Card
			{
				Suit = Card.CardSuit.Spades,
				Value = 2
			};

			Deck.AddCardToBottom(Card1);
			Discards.AddCardToBottom(Card2);

			Dealer = new Dealer
			{
				Deck = Deck,
				Discards = Discards
			};

			Dealer.Init();
		}

		[Test]
		public void DrawCard()
		{
			var card1 = Dealer.DrawCard();
			var card2 = Dealer.DrawCard();

			Assert.AreSame(Card1, card1);
			Assert.AreSame(Card2, card2);
		}

		[Test]
		public void Shuffle()
		{
			var deck1 = new Deck();
			var deck2 = new Deck();

			deck1.Init();
			deck2.Init();

			deck1.Shuffle();
			deck2.Shuffle();

			var sameCards = 0;

			for (var i = 0; i < 52; i++)
			{
				var card1 = deck1.CardAt(i);
				var card2 = deck2.CardAt(i);

				if (Equals(card1, card2))
				{
					sameCards++;
				}
			}

			Console.WriteLine("Number of cards in the same place in the deck: " + sameCards);
			Assert.IsTrue(sameCards < 10);
		}
	}
}