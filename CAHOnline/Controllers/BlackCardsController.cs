using CAHOnline.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace CAHOnline.Controllers
{
    public class BlackCardsController : ApiController
    {
        private readonly IBlackCardSource _source;

        public BlackCardsController(IBlackCardSource source)
        {
            _source = source;
        }

        // GET api/blackcards
        public IEnumerable<IBlackCard> Get()
        {
            return _source.All();
        }

        // GET api/blackcards/1
        public IBlackCard Get(int key)
        {
            return _source.CardWithKey(key);
        }
    }
}