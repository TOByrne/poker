using System;
using System.Collections.Generic;

namespace Poker
{
	public class Card
	{
		[Flags]
		public enum CardSuit
		{
			Clubs = 0x01,
			Diamonds = 0x02,
			Hearts = 0x04,
			Spades = 0x08
		}

		Dictionary<CardSuit, string> SuitCode = new Dictionary<CardSuit, string>
		{
			{CardSuit.Clubs, "(C)"},
			{CardSuit.Diamonds, "(D)"},
			{CardSuit.Hearts, "(H)"},
			{CardSuit.Spades, "(S)"}
		}; 

		public enum FaceValue
		{
			Ace = 1,
			Jack = 11,
			Queen = 12,
			King = 13
		}

		public int[] CardValues = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };
		public static CardSuit[] CardSuits = { CardSuit.Clubs, CardSuit.Diamonds, CardSuit.Hearts, CardSuit.Spades };

		public CardSuit Suit { get; set; }
		public int Value { get; set; }

		public Card() { }

		public Card(int value, CardSuit suit)
		{
			Value = value;
			Suit = suit;
		}

		public override string ToString()
		{
			return Value + SuitCode[Suit];
		}

		public Card Copy()
		{
			return new Card(this.Value, this.Suit);
		}

		public override bool Equals(object obj)
		{
			var other = obj as Card;
			return other != null && (Value == other.Value && Suit == other.Suit);
		}
	}
}