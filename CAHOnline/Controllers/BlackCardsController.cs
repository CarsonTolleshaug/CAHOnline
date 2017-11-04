using CAHOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Runtime.Caching;

namespace CAHOnline.Controllers
{
    public class BlackCardsController : ApiController
    {
        private readonly IBlackCardsSource _source;
        private readonly IRandomOrder _randomOrder;

        public BlackCardsController(IBlackCardsSource source, IRandomOrder randomOrder)
        {
            _source = source;
            _randomOrder = randomOrder;
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

        [HttpGet, Route("api/blackcards/random")]
        public IBlackCard GetRandom()
        {
            return _source.CardWithKey(_randomOrder.NextIndex());
        }
    }
}