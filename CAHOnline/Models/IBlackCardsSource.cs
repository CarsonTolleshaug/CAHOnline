using System.Collections.Generic;

namespace CAHOnline.Models
{
    public interface IBlackCardsSource
    {
        ICollection<IBlackCard> All();
        IBlackCard CardWithKey(int key);
    }
}
