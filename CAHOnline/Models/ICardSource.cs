using System.Collections.Generic;

namespace CAHOnline.Models
{
    public interface ICardSource<T> where T : ICard
    {
        IEnumerable<T> All();
        T CardWithKey(int key);
    }
}
