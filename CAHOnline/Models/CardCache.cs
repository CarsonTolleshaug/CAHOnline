using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;

namespace CAHOnline.Models
{
    public interface ICardCache
    {
        ICard NextCard();
    }

    public class CardCache : ICardCache
    {
        private readonly string _key;
        private readonly MemoryCache _cache;
        private readonly ICardSource _source;

        public CardCache(ICardSource source, string key) : this(source, key, MemoryCache.Default) { }

        public CardCache(ICardSource source, string key, MemoryCache cache)
        {
            _key = key;
            _cache = cache;
            _source = source;
        }

        public ICard NextCard()
        {
            IEnumerator<ICard> enumerator = CardsEnumerator();

            if (enumerator.MoveNext()) return enumerator.Current;

            enumerator = CardsEnumeratorFromSource();
            enumerator.MoveNext();
            return enumerator.Current;
        }

        private IEnumerator<ICard> CardsEnumerator()
        {
            if (_cache.Contains(_key)) return (IEnumerator<ICard>)_cache.Get(_key);

            return CardsEnumeratorFromSource();
        }

        private IEnumerator<ICard> CardsEnumeratorFromSource()
        {
            IEnumerator<ICard> enumerator = _source.All().GetEnumerator();
            _cache.Add(new CacheItem(_key, enumerator), new CacheItemPolicy
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddHours(1)
            });
            return enumerator;
        }
    }
}