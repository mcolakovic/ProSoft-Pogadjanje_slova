using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [Serializable]
    public class Slova
    {
        public char Slovo { get; set; }
        public int Poeni { get; set; }
        public bool Pogodio { get; set; }
    }
}
