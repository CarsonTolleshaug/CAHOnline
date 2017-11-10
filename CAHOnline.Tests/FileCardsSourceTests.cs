using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Collections.Generic;
using CAHOnline.Models;
using Newtonsoft.Json;
using FluentAssertions;
using System.Linq;

namespace CAHOnline.Tests
{
    [TestClass]
    public class FileCardsSourceTests
    {
        private string _tempFile;

        private string SetUpFile(string filename, IEnumerable<ICard> cards)
        {
            _tempFile = Path.Combine(Path.GetTempPath(), filename);
            File.WriteAllText(_tempFile, JsonConvert.SerializeObject(cards));
            return _tempFile;
        }

        [TestCleanup]
        private void RemoveTempFile()
        {
            if (string.IsNullOrEmpty(_tempFile) || !File.Exists(_tempFile)) return;
            File.Delete(_tempFile);
            _tempFile = string.Empty;
        }

        [TestMethod]
        public void ShouldReadCardsFromFile()
        {
            string file = SetUpFile("cards.json", new List<FakeCard>
            {
                new FakeCard("foo"),
                new FakeCard("bar"),
                new FakeCard("baz")
            });
            ICardSource fileSource = new FileCardSource<FakeCard>(file);

            List<ICard> cards = fileSource.All().ToList();

            cards.Count.Should().Be(3);
            cards[0].Text.Should().Be("foo");
            cards[1].Text.Should().Be("bar");
            cards[2].Text.Should().Be("baz");
        }

        [TestMethod, TestCategory("functional")]
        public void ShouldReadRealCards()
        {
            string file = "Data\\cards.json";
            ICardSource fileSource = new FileCardSource<BlackCard>(file);

            List<ICard> cards = fileSource.All().ToList();

            cards.Should().NotBeNullOrEmpty();
        }

        [TestMethod]
        public void ShouldReadCardWithKeyFromFile()
        {
            string file = SetUpFile("cards.json", new List<FakeCard>
            {
                new FakeCard("foo"),
                new FakeCard("bar"),
                new FakeCard("baz")
            });
            ICardSource fileSource = new FileCardSource<FakeCard>(file);

            ICard card = fileSource.CardWithKey(1);

            card.Text.Should().Be("bar");
        }
    }    
}
