using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [Serializable]
    public class Igra
    {
        public User PrviIgrac { get; set; }
        public User DrugiIgrac { get; set; }
        public Slova Slova { get; set; }
    }
}
