using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using CAHOnline.Models;

namespace CAHOnline.Tests
{
    [TestClass]
    public class EnumerableExtensionsTests
    {
        [TestMethod]
        public void ShouldRandomizeEnumerable()
        {
            List<string> items = new List<string>
            {
                "foo",
                "bar",
                "baz"
            };
            IRandom fakeRandom = new FakeRandom(new List<int> { 2, 1, 0 });

            IList<string> randomizedItems = items.Randomize(fakeRandom);

            randomizedItems[0].Should().Be("baz");
            randomizedItems[1].Should().Be("bar");
            randomizedItems[2].Should().Be("foo");
        }

        [TestMethod]
        public void ShouldNotRepeat()
        {
            List<string> items = new List<string>
            {
                "foo",
                "bar",
                "baz"
            };
            IRandom fakeRandom = new FakeRandom(new List<int> { 1, 1, 0 });

            IList<string> randomizedItems = items.Randomize(fakeRandom);

            randomizedItems[0].Should().Be("bar");
            randomizedItems[1].Should().Be("baz");
            randomizedItems[2].Should().Be("foo");
        }

        [TestMethod]
        public void ShouldWorkWithNonListEnumerable()
        {
            string[] items =
            {
                "foo",
                "bar",
                "baz"
            };
            IRandom fakeRandom = new FakeRandom(new List<int> { 2, 1, 0 });

            IList<string> randomizedItems = items.Randomize(fakeRandom).ToList();

            randomizedItems[0].Should().Be("baz");
            randomizedItems[1].Should().Be("bar");
            randomizedItems[2].Should().Be("foo");
        }
    }

    public class FakeRandom : IRandom
    {
        private readonly IEnumerator<int> _order;

        public FakeRandom(IEnumerable<int> order)
        {
            _order = order.GetEnumerator();
        }

        public int Next(int max)
        {
            _order.MoveNext();
            int next = _order.Current;
            return next;
        }
    }
}
