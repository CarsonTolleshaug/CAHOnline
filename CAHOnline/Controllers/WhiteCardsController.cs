using CAHOnline.Models;
using System.Web;
using System.Web.Http;

namespace CAHOnline.Controllers
{
    public class WhiteCardsController : ApiController
    {
        private readonly ICardCache _cache;
        public WhiteCardsController() : this(HttpContext.Current.Request.MapPath("~\\Data\\whitecards.json")) { }

        public WhiteCardsController(string file) : this(new FileCardSource<WhiteCard>(file)) { }

        public WhiteCardsController(ICardSource source) : this(new CardCache(new RandomizedCardSource(source), "WhiteCards")) { }

        public WhiteCardsController(ICardCache cache)
        {
            _cache = cache;
        }

        [HttpGet, Route("api/whitecards/next")]
        public ICard GetNext()
        {
            return _cache.NextCard();
        }
    }
}