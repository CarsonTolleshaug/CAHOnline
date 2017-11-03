using Newtonsoft.Json;

namespace CAHOnline.Models
{    
    public class BlackCard : IBlackCard
    {
        private readonly ICardText _cardText;

        [JsonConstructor]
        public BlackCard(string text) : this(new BlackCardText(text)) { }

        public BlackCard(ICardText cardText)
        {
            _cardText = cardText;
        }

        public string Text => _cardText.ToString();
    }
}