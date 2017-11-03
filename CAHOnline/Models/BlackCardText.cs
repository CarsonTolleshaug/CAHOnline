namespace CAHOnline.Models
{
    public class BlackCardText : ICardText
    {
        private readonly string _text;
        private readonly string _blankReplacement;
        private readonly string _newLine;

        public BlackCardText(string text) : this(text, "<span class=\"blank\"></span>") { }

        public BlackCardText(string text, string blankReplacement) : this(text, blankReplacement, "<br/><br/>") { }

        public BlackCardText(string text, string blankReplacement, string newLine)
        {
            _text = text;
            _blankReplacement = blankReplacement;
            _newLine = newLine;
        }

        public override string ToString()
        {
            string replacedText = _text.Replace("_", _blankReplacement);

            if (replacedText != _text) return replacedText;

            return $"{ _text }{ _newLine }{ _blankReplacement }";
        }
    }
}