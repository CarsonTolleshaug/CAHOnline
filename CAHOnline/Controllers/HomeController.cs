using CAHOnline.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Web.Mvc;

namespace CAHOnline.Controllers
{
    public class HomeController : Controller
    {
        private readonly Random _rand = new Random();
        private readonly SubmittedCardsController _submittedCards = new SubmittedCardsController();

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            _submittedCards.ClearExistingCards();

            BlackCard model = RandomBlackCard();

            return View(model);
        }

        private BlackCard RandomBlackCard()
        {
            string json = System.IO.File.ReadAllText(Server.MapPath("~/Data/cards.json"));
            string text = RandomBlackCardText(json);
            return new BlackCard(text);
        }

        private string RandomBlackCardText(string json)
        {
            JObject cardsJson = JObject.Parse(json);
            var cards = cardsJson["blackCards"].Values<JObject>().ToList();

            int index = _rand.Next(cards.Count());
            return cards[index]["text"].Value<string>();
        }
    }
}
