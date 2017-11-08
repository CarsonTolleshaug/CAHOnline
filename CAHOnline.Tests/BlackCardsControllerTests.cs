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
        public void ShouldProvideBlackCards()
        {
            IBlackCardSource source = new FakeBlackCardsSource(new List<IBlackCard>
            {
                new FakeBlackCard("foo")
            });

            BlackCardsController blackCardsController = new BlackCardsController(source);
            IEnumerable<IBlackCard> blackCards = blackCardsController.Get();
            blackCards.Single().Text.Should().Be("foo");
        }

        [TestMethod]
        public void ShouldProvideBlackCard()
        {
            IBlackCardSource source = new FakeBlackCardsSource(new List<IBlackCard>
            {
                new FakeBlackCard("foo"),
                new FakeBlackCard("bar")
            });

            BlackCardsController blackCardsController = new BlackCardsController(source);
            IBlackCard blackCard = blackCardsController.Get(1);
            blackCard.Text.Should().Be("bar");
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

    public class FakeBlackCardsSource : IBlackCardSource
    {
        private readonly IList<IBlackCard> _cards;

        public FakeBlackCardsSource(IList<IBlackCard> cards)
        {
            _cards = cards;
        }

        public ICollection<IBlackCard> All()
        {
            return _cards;
        }

        public IBlackCard CardWithKey(int key)
        {
            return _cards[key];
        }
    }
}
