using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [Serializable]
    public class User
    {
        public string Email { get; set; }
        public int BrojPoena { get; set; }
        public int BrojPogodaka { get; set; }
        public int BrojPokusaja { get; set; }
        public string Rezultat { get; set; }
    }
}
