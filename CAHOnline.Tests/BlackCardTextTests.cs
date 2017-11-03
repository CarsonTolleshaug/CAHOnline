using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CAHOnline.Models;

namespace CAHOnline.Tests
{
    [TestClass]
    public class BlackCardTextTexts
    {
        [TestMethod]
        public void ShouldProvideToStringText()
        {
            string expected = "foo";
            ICardText cardText = new BlackCardText(expected, string.Empty, string.Empty);
            string actual = cardText.ToString();
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ShouldReplaceUnderscores()
        {
            ICardText cardText = new BlackCardText("foo _ bar", "fizzbuzz", string.Empty);
            string actual = cardText.ToString();
            actual.Should().Be("foo fizzbuzz bar");
        }

        [TestMethod]
        public void ShouldAppendBlankGivenNoUnderscores()
        {
            string expected = "foo bar";
            ICardText cardText = new BlackCardText(expected, "fizzbuzz", "<br>");
            string actual = cardText.ToString();
            actual.Should().Be("foo bar<br>fizzbuzz");
        }
    }
}
