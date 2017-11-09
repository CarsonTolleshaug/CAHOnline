using System.Collections.Generic;
using System.Linq;

namespace CAHOnline.Models
{
    public static class EnumerableExtensions
    {
        public static List<T> Randomize<T>(this List<T> list)
        {
            return list.Randomize(new RandomWrapper());
        }

        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Randomize(new RandomWrapper());
        }

        public static List<T> Randomize<T>(this List<T> list, IRandom random)
        {
            List<T> randomizedItems = new List<T>();

            for (int itemsLeft = list.Count; itemsLeft > 0; --itemsLeft)
            {
                int index = random.Next(itemsLeft);
                randomizedItems.Add(list[index]);
                list.RemoveAt(index);
            }

            return randomizedItems;
        }

        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> enumerable, IRandom random)
        {
            return enumerable.ToList().Randomize(random);
        }
    }
}