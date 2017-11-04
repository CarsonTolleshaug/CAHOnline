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
            IBlackCardsSource source = new FakeBlackCardsSource(new List<IBlackCard>
            {
                new FakeBlackCard("foo")
            });

            BlackCardsController blackCardsController = new BlackCardsController(source, new FakeRandomOrder(0));
            IEnumerable<IBlackCard> blackCards = blackCardsController.Get();
            blackCards.Single().Text.Should().Be("foo");
        }

        [TestMethod]
        public void ShouldProvideBlackCard()
        {
            IBlackCardsSource source = new FakeBlackCardsSource(new List<IBlackCard>
            {
                new FakeBlackCard("foo"),
                new FakeBlackCard("bar")
            });

            BlackCardsController blackCardsController = new BlackCardsController(source, new FakeRandomOrder(0));
            IBlackCard blackCard = blackCardsController.Get(1);
            blackCard.Text.Should().Be("bar");
        }

        [TestMethod]
        public void ShouldProvideRandomBlackCard()
        {
            IBlackCardsSource source = new FakeBlackCardsSource(new List<IBlackCard>
            {
                new FakeBlackCard("foo"),
                new FakeBlackCard("bar"),
                new FakeBlackCard("baz")
            });

            BlackCardsController blackCardsController = new BlackCardsController(source, new FakeRandomOrder(1));
            IBlackCard blackCard = blackCardsController.GetRandom();
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

    public class FakeBlackCardsSource : IBlackCardsSource
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

    public class FakeRandomOrder : IRandomOrder
    {
        private readonly int _returnValue;

        public FakeRandomOrder(int returnValue)
        {
            _returnValue = returnValue;
        }

        public int NextIndex()
        {
            return _returnValue;
        }
    }
}
