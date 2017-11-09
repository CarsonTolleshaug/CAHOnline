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
            FakeCardSource fakeCardSource = new FakeCardSource(new List<FakeCard> { new FakeCard("") });
            MemoryCache.Default.Add(new CacheItem("fakeCards", (new List<FakeCard> { new FakeCard(expected) }).GetEnumerator()), 
                new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(1) });
            CardCache<FakeCard> cardCache = new CardCache<FakeCard>(fakeCardSource, "fakeCards", MemoryCache.Default);

            FakeCard card = cardCache.NextCard();

            card.Text.Should().Be(expected);
        }

        [TestMethod]
        public void GivenNoCardsInCacheShouldRetreiveCardFromSource()
        {
            const string expected = "foo";
            FakeCardSource fakeCardSource = new FakeCardSource(new List<FakeCard> { new FakeCard(expected) });
            CardCache<FakeCard> cardCache = new CardCache<FakeCard>(fakeCardSource, "fakeCards", MemoryCache.Default);

            FakeCard card = cardCache.NextCard();

            card.Text.Should().Be(expected);
        }

        [TestMethod]
        public void GivenNoCardsInCacheShouldAddCardsToCache()
        {
            const string expected = "foo";
            FakeCardSource fakeCardSource = new FakeCardSource(new List<FakeCard> { new FakeCard(expected) });
            CardCache<FakeCard> cardCache = new CardCache<FakeCard>(fakeCardSource, "fakeCards", MemoryCache.Default);

            cardCache.NextCard();
            IEnumerator<FakeCard> cachedEnumerator = (IEnumerator<FakeCard>)MemoryCache.Default.Get("fakeCards");

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

    public class FakeCardSource : ICardSource<FakeCard>
    {
        private readonly ICollection<FakeCard> _cards;

        public FakeCardSource(ICollection<FakeCard> cards)
        {
            _cards = cards;
        }

        public IEnumerable<FakeCard> All()
        {
            return _cards;
        }

        public FakeCard CardWithKey(int key)
        {
            throw new NotImplementedException();
        }
    }
}
