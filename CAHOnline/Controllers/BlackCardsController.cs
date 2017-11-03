using CAHOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CAHOnline.Controllers
{
    public class BlackCardsController : ApiController
    {
        private IBlackCardsSource _source;

        public BlackCardsController(IBlackCardsSource source)
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