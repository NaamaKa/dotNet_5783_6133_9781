

using DO;

namespace Dal;
using static Dal.DataSource.Config;
using static Dal.DataSource;

public class DalOrderItem
{
    public void AddNewOrderItem(OrderItem _newOrderItem)
    {
       orderItems.Add(_newOrderItem) ;
    }
    public OrderItem GetOrderItem(int _myNumOrder, int myProductBarcode)
    {
        foreach (OrderItem item in orderItems)
        {
            if (item.orderId == _myNumOrder && item.itemId == myProductBarcode)
                return item;
        }
        throw new Exception("item not found in order");
    }
    public List<OrderItem> GetOrderItem()
    {
        List<OrderItem> tempOrderItems = new List<OrderItem>();
        foreach(OrderItem item in orderItems)
        {
            tempOrderItems.Add(item);
        }
        if(tempOrderItems.Count > 0)
            return tempOrderItems;
        throw new Exception("no items found");

    }
    public List<OrderItem> GetOrderItems(int _myNumOrder)
    {
        List<OrderItem> tempOrderItems = new List<OrderItem>();
        foreach(OrderItem item in orderItems)
        {
            if(item.orderId== _myNumOrder)
            {
                tempOrderItems.Add((OrderItem)item);
            }
        }
        if( tempOrderItems.Count > 0)
            return tempOrderItems;
        throw new Exception("no items found in order");
    }
    public void DeleteOrderItem(int _myNumOrder, int _myProductBarcode)
    {
        foreach(OrderItem item in orderItems)
        {
            if(item.orderId== _myNumOrder&&item.itemId== _myProductBarcode)
            {
                orderItems.Remove(item);
                break;
            }
        }
        throw new Exception("product not found");
    }
    public void UpdateOrderItem(OrderItem _newOrderItem)
    {
        foreach(OrderItem item in orderItems)
        {
            if(item.orderId== _newOrderItem.orderId&&item.itemId== _newOrderItem.itemId)
            {
                orderItems.Remove(item);
                orderItems.Add(_newOrderItem);
                break;
            }
        }
        throw new Exception("order item not found");
    }
}
