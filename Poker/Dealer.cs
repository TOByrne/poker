using System;

namespace Poker
{
	public class Dealer
	{
		public Deck Deck { get; set; }
		public Deck Discards { get; set; }

		public Dealer()
		{
			Deck = new Deck();
		}

		public void Init()
		{
			Deck.OnDeckOutOfCards += DeckOnOnDeckOutOfCards;
		}

		public void Shuffle(Deck deck)
		{
			deck.Shuffle();
		}

		public Card DrawCard()
		{
			return Deck.DealCard();
		}

		private void DeckOnOnDeckOutOfCards(object sender, EventArgs e)
		{
			Deck = Deck + Discards;
			Shuffle(Deck);
		}

	}
}
