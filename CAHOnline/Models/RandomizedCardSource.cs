using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CAHOnline.Models
{
    public class RandomizedCardSource : ICardSource
    {
        private readonly ICardSource _source;
        private readonly IRandom _random;

        public RandomizedCardSource(ICardSource source) : this(source, new RandomWrapper()) { }

        public RandomizedCardSource(ICardSource source, IRandom random)
        {
            _source = source;
            _random = random;
        }

        public IEnumerable<ICard> All()
        {
            return _source.All().Randomize(_random);
        }

        public ICard CardWithKey(int key)
        {
            return _source.CardWithKey(key);
        }
    }
}