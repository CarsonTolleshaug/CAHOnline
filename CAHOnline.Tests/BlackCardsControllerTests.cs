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
            ICardCache<IBlackCard> fakeCache = new FakeCardCache<IBlackCard>(new List<IBlackCard>
            {
                new FakeBlackCard("foo"),
                new FakeBlackCard("bar"),
                new FakeBlackCard("baz")
            });
            BlackCardsController blackCardsController = new BlackCardsController(fakeCache);

            IBlackCard card = blackCardsController.GetNext();

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

    public class FakeCardCache<T> : ICardCache<T> where T : ICard
    {
        private readonly IEnumerator<T> _cards;

        public FakeCardCache(IEnumerable<T> cards)
        {
            _cards = cards.GetEnumerator();
        }

        public T NextCard()
        {
            _cards.MoveNext();
            return _cards.Current;
        }
    }

    public class FakeBlackCardsSource : ICardSource<IBlackCard>
    {
        private readonly IList<IBlackCard> _cards;

        public FakeBlackCardsSource(IList<IBlackCard> cards)
        {
            _cards = cards;
        }

        public IEnumerable<IBlackCard> All()
        {
            return _cards;
        }

        public IBlackCard CardWithKey(int key)
        {
            return _cards[key];
        }
    }
}
