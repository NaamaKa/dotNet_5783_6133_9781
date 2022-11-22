using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
namespace DalApi;

public interface IOrderItem : ICrud<OrderItem>
{
    public IOrderItem GetOrderItem(int _myNumOrder, int myProductBarcode);
}
