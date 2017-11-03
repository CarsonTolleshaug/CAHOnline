using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CAHOnline.Models
{
    [DataContract]
    public class WhiteCard
    {
        public WhiteCard(Random rand)
        {
            RandomOrder = rand.Next();
        }

        [DataMember]
        public string Owner { get; set; }

        [DataMember]
        public string Answer { get; set; }

        internal int RandomOrder { get; set; }
    }
}