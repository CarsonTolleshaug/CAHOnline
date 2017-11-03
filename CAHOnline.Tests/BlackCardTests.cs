using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CAHOnline.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CAHOnline.Tests
{
    [TestClass]
    public class BlackCardTests
    {        
        [TestMethod]
        public void ShouldBeSerializable()
        {
            IBlackCard blackCard = new BlackCard(new FakeCardText("foo"));
            string actual = JsonConvert.SerializeObject(blackCard);
            JObject.Parse(actual)["Text"].Value<string>().Should().Be("foo");
        }

        [TestMethod]
        public void ShouldBeDeserializable()
        {
            string json = "{ 'text' : 'foo' }";
            IBlackCard blackCard = JsonConvert.DeserializeObject<BlackCard>(json);
            blackCard.Text.Should().Contain("foo");
        }
    }

    public class FakeCardText : ICardText
    {
        private readonly string _text;

        public FakeCardText(string text)
        {
            _text = text;
        }

        public override string ToString()
        {
            return _text;
        }
    }
}
