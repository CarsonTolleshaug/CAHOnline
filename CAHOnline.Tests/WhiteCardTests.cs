using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CAHOnline.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CAHOnline.Tests
{
    [TestClass]
    public class WhiteCardTests
    {        
        [TestMethod]
        public void ShouldBeSerializable()
        {
            ICard whiteCard = new WhiteCard(new FakeCardText("foo"));
            string actual = JsonConvert.SerializeObject(whiteCard);
            JObject.Parse(actual)["Text"].Value<string>().Should().Be("foo");
        }

        [TestMethod]
        public void ShouldBeDeserializable()
        {
            string json = "{ 'text' : 'foo' }";
            ICard WhiteCard = JsonConvert.DeserializeObject<WhiteCard>(json);
            WhiteCard.Text.Should().Contain("foo");
        }
    }
}
