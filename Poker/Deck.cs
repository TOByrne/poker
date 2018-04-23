using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Poker
{
	public class Deck
	{
		private static Random r = new Random(DateTime.Now.Millisecond);
		public int CardsLeft => Cards.Count;
		private Stack<Card> Cards { get; set; }
		private Dealer Dealer { get; set; }

		public event EventHandler OnDeckOutOfCards;

		public Deck()
		{
			Cards = new Stack<Card>();
		}

		public void Init()
		{
			for (var i = 0; i < 4; i++)
			{
				var suit = Card.CardSuits[i];

				for (var c = 1; c < 14; c++)
				{
					AddCardToBottom(new Card(c, suit));
				}
			}
		}

		public Card DealCard()
		{
			if (Cards.Count < 1)
			{
				OnDeckOutOfCards?.Invoke(this, null);
			}
			return Cards.Pop();
		}

		public void AddCardToBottom(Card card)
		{
			var tempDeck = new Stack<Card>();

			while (Cards.Count > 0)
			{
				tempDeck.Push(Cards.Pop());
			}

			tempDeck.Push(card);

			while (tempDeck.Count > 0)
			{
				Cards.Push(tempDeck.Pop());
			}
		}

		public bool HasCards()
		{
			return Cards.Count > 0;
		}

		public void Shuffle()
		{
			var values = Cards.ToArray();
			Cards.Clear();
			foreach (var card in values.OrderBy(x => r.Next()))
			{
				AddCardToBottom(card);
			}
		}

		public static void Shuffle(Deck deck)
		{
			var values = deck.Cards.ToArray();
			deck.Cards.Clear();
			foreach (var card in values.OrderBy(x => r.Next()))
			{
				deck.AddCardToBottom(card);
			}
		}

		public Card CardAt(int index)
		{
			if (index < 0 || index >= Cards.Count)
			{
				return null;
			}

			return Cards.ToArray()[index];
		}

		public static Deck operator +(Deck deck1, Deck deck2)
		{
			while (deck2.HasCards())
			{
				deck1 += deck2.DealCard();
			}

			return deck1;
		}

		public static Deck operator +(Deck deck, Card card)
		{
			deck.AddCardToBottom(card);
			return deck;
		}
	}
}