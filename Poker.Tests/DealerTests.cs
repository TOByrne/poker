using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Poker.Tests
{
	[TestFixture]
	public class DealerTests
	{
		public Dealer Dealer { get; set; }

		public DealerTests()
		{
			Dealer = new Dealer();
		}

		[Test]
		public void Shuffle()
		{
			
		}
	}
}
