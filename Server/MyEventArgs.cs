using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class MyEventArgs : EventArgs
    {
        public Slova slova { get; set; }

        public MyEventArgs(Slova slova)
        {
            this.slova = slova;
        }
    }
}
