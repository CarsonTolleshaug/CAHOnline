using CAHOnline.Controllers;
using CAHOnline.Models;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System.Linq;
using System.Runtime.Caching;
using System;

namespace CAHOnline.Tests
{
    [TestClass]
    public class WhiteCardsControllerTests
    {
        [TestMethod]
        public void ShouldProvideWhiteCard()
        {
            ICardCache fakeCache = new FakeCardCache(new List<ICard>
            {
                new FakeCard("foo"),
                new FakeCard("bar"),
                new FakeCard("baz")
            });
            WhiteCardsController whiteCardsController = new WhiteCardsController(fakeCache);

            ICard card = whiteCardsController.GetNext();

            card.Text.Should().Be("foo");
        }        
    }
}
