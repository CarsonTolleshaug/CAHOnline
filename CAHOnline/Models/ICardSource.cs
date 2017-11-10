using System.Collections.Generic;

namespace CAHOnline.Models
{
    public interface ICardSource
    {
        IEnumerable<ICard> All();
        ICard CardWithKey(int key);
    }
}
