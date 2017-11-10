using CAHOnline.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace CAHOnline.Controllers
{
    public class BlackCardsController : ApiController
    {
        private readonly ICardCache<IBlackCard> _cache;
        
        public BlackCardsController(string file) : this(new FileCardSource<IBlackCard>(file)) { }

        public BlackCardsController(ICardSource<IBlackCard> source) : this(new CardCache<IBlackCard>(new RandomizedCardSource<IBlackCard>(source))) { }

        public BlackCardsController(ICardCache<IBlackCard> cache)
        {
            _cache = cache;
        }
        
        [HttpGet, Route("api/blackcards/next")]
        public IBlackCard GetNext()
        {
            return _cache.NextCard();
        }
    }
}