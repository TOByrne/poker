using System.Collections.Generic;
using System.Text;

namespace Poker
{
	public class Hand : List<Card>
	{
		public Hand() { }

		public Hand(IEnumerable<Card> collection) : base(collection)
		{
		}

		public Hand Copy()
		{
			var copiedHand = new Hand();
			copiedHand.AddRange(this);
			return copiedHand;
		}

		public override string ToString()
		{
			var sb = new StringBuilder();
			foreach (var card in this)
			{
				sb.Append(card.ToString() + " ");
			}
			return sb.ToString();
		}
	}
}