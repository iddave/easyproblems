using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sweeper
{
    internal class ChangeArgs : EventArgs
    {
        public int I, J;
        public string MinesArround;
    }
}
