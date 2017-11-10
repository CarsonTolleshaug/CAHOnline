using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.Caching;
using CAHOnline.Models;
using System.Collections.Generic;
using FluentAssertions;

namespace CAHOnline.Tests
{
    [TestClass]
    public class CardCacheTests
    {
        [TestCleanup]
        public void Teardown()
        {
            MemoryCache.Default.Remove("fakeCards");
        }

        [TestMethod]
        public void ShouldGetNextCardFromCache()
        {
            const string expected = "foo";
            FakeCardSource fakeCardSource = new FakeCardSource(new List<ICard> { new FakeCard("") });
            MemoryCache.Default.Add(new CacheItem("fakeCards", (new List<ICard> { new FakeCard(expected) }).GetEnumerator()), 
                new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(1) });
            CardCache cardCache = new CardCache(fakeCardSource, "fakeCards", MemoryCache.Default);

            ICard card = cardCache.NextCard();

            card.Text.Should().Be(expected);
        }

        [TestMethod]
        public void GivenNoCardsInCacheShouldRetreiveCardFromSource()
        {
            const string expected = "foo";
            FakeCardSource fakeCardSource = new FakeCardSource(new List<ICard> { new FakeCard(expected) });
            CardCache cardCache = new CardCache(fakeCardSource, "fakeCards", MemoryCache.Default);

            ICard card = cardCache.NextCard();

            card.Text.Should().Be(expected);
        }

        [TestMethod]
        public void GivenNoCardsInCacheShouldAddCardsToCache()
        {
            const string expected = "foo";
            FakeCardSource fakeCardSource = new FakeCardSource(new List<ICard> { new FakeCard(expected) });
            CardCache cardCache = new CardCache(fakeCardSource, "fakeCards", MemoryCache.Default);

            cardCache.NextCard();
            IEnumerator<ICard> cachedEnumerator = (IEnumerator<ICard>)MemoryCache.Default.Get("fakeCards");

            cachedEnumerator.Should().NotBeNull();
            cachedEnumerator.Current.Text.Should().Be(expected);
        }
    }

    public class FakeCard : ICard
    {
        private readonly string _text;

        public FakeCard(string text)
        {
            _text = text;
        }

        public string Text => _text;
    }

    public class FakeCardSource : ICardSource
    {
        private readonly ICollection<ICard> _cards;

        public FakeCardSource(ICollection<ICard> cards)
        {
            _cards = cards;
        }

        public IEnumerable<ICard> All()
        {
            return _cards;
        }

        public ICard CardWithKey(int key)
        {
            throw new NotImplementedException();
        }
    }
}
