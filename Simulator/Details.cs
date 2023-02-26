using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator
{
    public class Details : EventArgs
    {
        public BO.Order order;
        public int seconds;
        public Details(BO.Order ord, int sec)
        {
            order = ord;
            seconds = sec;
        }
    }
}
