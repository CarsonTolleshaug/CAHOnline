using Newtonsoft.Json;

namespace CAHOnline.Models
{
    public class WhiteCard : ICard
    {
        private readonly ICardText _cardText;

        [JsonConstructor]
        public WhiteCard(string text) : this(new WhiteCardText(text)) { }

        public WhiteCard(ICardText cardText)
        {
            _cardText = cardText;
        }

        public string Text => _cardText.ToString();
    }
}