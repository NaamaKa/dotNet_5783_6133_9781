using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface IOrderItem : ICrud<IOrderItem>
    {
        public IOrderItem GetOrderItem(int _myNumOrder, int myProductBarcode);
    }
}
