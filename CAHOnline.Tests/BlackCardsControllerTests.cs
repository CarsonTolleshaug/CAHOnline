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
    public class BlackCardsControllerTests
    {
        [TestMethod]
        public void ShouldProvideBlackCard()
        {
            ICardCache fakeCache = new FakeCardCache(new List<ICard>
            {
                new FakeBlackCard("foo"),
                new FakeBlackCard("bar"),
                new FakeBlackCard("baz")
            });
            BlackCardsController blackCardsController = new BlackCardsController(fakeCache);

            ICard card = blackCardsController.GetNext();

            card.Text.Should().Be("foo");
        }        
    }

    public class FakeBlackCard : IBlackCard
    {
        private readonly string _text;

        public FakeBlackCard(string text)
        {
            _text = text;
        }

        public string Text => _text;
    }

    public class FakeCardCache : ICardCache
    {
        private readonly IEnumerator<ICard> _cards;

        public FakeCardCache(IEnumerable<ICard> cards)
        {
            _cards = cards.GetEnumerator();
        }

        public ICard NextCard()
        {
            _cards.MoveNext();
            return _cards.Current;
        }
    }
}
