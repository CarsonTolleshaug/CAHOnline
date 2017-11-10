using CAHOnline.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace CAHOnline.Controllers
{
    public class BlackCardsController : ApiController
    {
        private readonly ICardCache _cache;

        public BlackCardsController() : this(System.Web.HttpContext.Current.Request.MapPath("~\\Data\\blackcards.json")) { }

        public BlackCardsController(string file) : this(new FileCardSource<BlackCard>(file)) { }

        public BlackCardsController(ICardSource source) : this(new CardCache(new RandomizedCardSource(source), "BlackCards")) { }

        public BlackCardsController(ICardCache cache)
        {
            _cache = cache;
        }
        
        [HttpGet, Route("api/blackcards/next")]
        public ICard GetNext()
        {
            return _cache.NextCard();
        }
    }
}