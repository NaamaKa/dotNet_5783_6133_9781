using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
namespace DalApi;

public interface IOrderItem : ICrud<OrderItem>
{
    public OrderItem GetOrderItem(int _myNumOrder, int myProductBarcode);
    public List<OrderItem> GetOrderItemsFromOrder(int _orderId);
    public List<OrderItem> GetOrdersOforderItems(int _itemId);
}