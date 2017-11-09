using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CAHOnline.Models
{
    public class RandomizedCardSource<T> : ICardSource<T> where T : ICard
    {
        private readonly ICardSource<T> _source;
        private readonly IRandom _random;

        public RandomizedCardSource(ICardSource<T> source) : this(source, new RandomWrapper()) { }

        public RandomizedCardSource(ICardSource<T> source, IRandom random)
        {
            _source = source;
            _random = random;
        }

        public IEnumerable<T> All()
        {
            return _source.All().Randomize(_random);
        }

        public T CardWithKey(int key)
        {
            return _source.CardWithKey(key);
        }
    }
}