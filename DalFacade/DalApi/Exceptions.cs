
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
  public class NotFound : Exception
    {
        public NotFound():base("not found") { }
      
        public NotFound(string s) : base("could notb"+s+" sd")
        {

        }
    }
}
