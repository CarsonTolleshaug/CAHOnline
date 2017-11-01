using DigialCAH.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DigialCAH.Controllers
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
            string text = FixBlackCardText(RandomBlackCardText(json));
            return new BlackCard { Text = text };
        }

        private string FixBlackCardText(string text)
        {
            const string blankSpan = "<span class=\"blank\"></span>";
            string newText = text.Replace("_", blankSpan);
            if (text != newText) return newText;
            return $"{text}<br/><br/>{blankSpan}";
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
