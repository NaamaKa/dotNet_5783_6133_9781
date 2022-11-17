using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    internal interface IDal
    {
        IProduct public product {get;}
        IOrder public order {get;}
        IOrderItem public orderitem{ get; }
    }
}
