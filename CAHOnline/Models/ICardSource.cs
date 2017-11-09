using System.Collections.Generic;

namespace CAHOnline.Models
{
    public interface ICardSource<T> where T : ICard
    {
        IEnumerable<T> All();
        T CardWithKey(int key);
    }

    public interface IBlackCardSource : ICardSource<IBlackCard> { }
}
