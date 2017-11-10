namespace CAHOnline.Models
{
    public class WhiteCardText : ICardText
    {
        private readonly string _text;

        public WhiteCardText(string text)
        {
            _text = text;
        }

        public override string ToString()
        {
            return _text;
        }
    }
}