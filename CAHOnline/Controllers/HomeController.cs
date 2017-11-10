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
        private readonly BlackCardsController _blackCards = new BlackCardsController();

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            _submittedCards.ClearExistingCards();

            ICard model = _blackCards.GetNext();

            return View(model);
        }
    }
}
