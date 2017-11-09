using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;

namespace CAHOnline.Models
{
    public interface ICardCache<T> where T : ICard
    {
        T NextCard();
    }

    public class CardCache<T> : ICardCache<T> where T : ICard
    {
        private readonly string _key;
        private readonly MemoryCache _cache;
        private readonly ICardSource<T> _source;

        public CardCache(ICardSource<T> source) : this(source, typeof(T).ToString(), MemoryCache.Default) { }

        public CardCache(ICardSource<T> source, string key, MemoryCache cache)
        {
            _key = key;
            _cache = cache;
            _source = source;
        }

        public T NextCard()
        {
            IEnumerator<T> enumerator = CardsEnumerator();
            enumerator.MoveNext();
            return enumerator.Current;
        }

        private IEnumerator<T> CardsEnumerator()
        {
            if (_cache.Contains(_key)) return (IEnumerator<T>)_cache.Get(_key);

            IEnumerator<T> enumerator = _source.All().GetEnumerator();
            _cache.Add(new CacheItem(_key, enumerator), new CacheItemPolicy
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddHours(1)
            });
            return enumerator;
        }
    }
}