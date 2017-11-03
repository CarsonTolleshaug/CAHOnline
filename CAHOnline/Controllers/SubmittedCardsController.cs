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
        private static readonly List<WhiteCard> _whiteCards = new List<WhiteCard>();

        // GET api/submittedcards
        public IEnumerable<WhiteCard> Get()
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
            WhiteCard card = CardToUpdate(id);
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

        private WhiteCard CardToUpdate(string owner)
        {
            WhiteCard card = _whiteCards.FirstOrDefault(c => c.Owner == owner);
            if (card != null) return card;

            card = new WhiteCard(_rand) { Owner = owner };
            _whiteCards.Add(card);
            return card;
        }
    }
}
