using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAHOnline.Models
{
    public interface IRandom
    {
        int Next(int max);
    }

    public class RandomWrapper : IRandom
    {
        private readonly Random _random = new Random();

        public int Next(int max)
        {
            return _random.Next(max);
        }
    }
}
