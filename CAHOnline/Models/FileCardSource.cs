using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CAHOnline.Models
{
    public class FileCardSource<T> : ICardSource where T : ICard
    {
        private readonly string _file;

        public FileCardSource(string file)
        {
            _file = file;
        }

        public IEnumerable<ICard> All()
        {
            string json = File.ReadAllText(_file);

            T[] cards = JsonConvert.DeserializeObject<T[]>(json);
            return cards.Cast<ICard>();
        }

        public ICard CardWithKey(int key)
        {
            return All().ToArray()[key];
        }
    }
}