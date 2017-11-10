using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CAHOnline.Models
{
    public class FileCardSource<T> : ICardSource<T> where T : ICard
    {
        private readonly string _file;

        public FileCardSource(string file)
        {
            _file = file;
        }

        public IEnumerable<T> All()
        {
            string json = File.ReadAllText(_file);

            return JsonConvert.DeserializeObject<T[]>(json);
        }

        public T CardWithKey(int key)
        {
            return All().ToArray()[key];
        }
    }
}