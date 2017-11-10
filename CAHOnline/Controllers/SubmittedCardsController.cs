using CAHOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CAHOnline.Controllers
{
    public class SubmittedCardsController : ApiController
    {
        private static readonly Random _rand = new Random();
        private static readonly List<SubmittedCard> _whiteCards = new List<SubmittedCard>();

        // GET api/submittedcards
        public IEnumerable<SubmittedCard> Get()
        {
            return _whiteCards.OrderBy(c => c.RandomOrder);
        }

        [Route("api/submittedcards/players")]
        public IEnumerable<string> GetSubmittedPlayers()
        {
            return _whiteCards.Select(c => c.Owner);
        }

        // PUT api/submittedcards/5
        public void Put(string id, [FromBody]string answer)
        {
            SubmittedCard card = CardToUpdate(id);
            card.Answer = answer;
        }

        [HttpPost]
        [Route("api/submittedcards/clear")]
        public void ClearExistingCards()
        {
            _whiteCards.Clear();
        }

        // DELETE api/submittedcards/5
        public void Delete(int id)
        {
        }

        private SubmittedCard CardToUpdate(string owner)
        {
            SubmittedCard card = _whiteCards.FirstOrDefault(c => c.Owner == owner);
            if (card != null) return card;

            card = new SubmittedCard(_rand) { Owner = owner };
            _whiteCards.Add(card);
            return card;
        }
    }
}
