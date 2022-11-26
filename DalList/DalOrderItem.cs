using DO;
using DalApi;
namespace Dal;
using static Dal.DataSource;

public class DalOrderItem : IOrderItem
{
    public void Add(OrderItem _newOrderItem)
    {
        orderItems.Add(_newOrderItem);
    }
    #region get functions
    public OrderItem GetOrderItem(int _myNumOrder, int myProductBarcode)
    {
        foreach (OrderItem item in orderItems)
        {
            if (item.orderId == _myNumOrder && item.itemId == myProductBarcode)
                return item;
        }
        throw new Exception("item not found in order");
    }
    public List<OrderItem> GetAll()
    {
        List<OrderItem> tempOrderItems = new List<OrderItem>();
        foreach (OrderItem item in orderItems)
        {
            tempOrderItems.Add(item);
        }
        if (tempOrderItems.Count > 0)
            return tempOrderItems;
        throw new Exception("no items found");

    }
    public OrderItem Get(int _id)
    {
        foreach (OrderItem item in orderItems)
        {
            if (item.id == _id)
            {
                return item;
            }
        }
        throw new Exception("no items found in order");
    }
    public List<OrderItem> GetOrderItemsFromOrder(int _myNumOrder)
    {
        List<OrderItem> tempOrderItems = new List<OrderItem>();
        foreach (OrderItem item in orderItems)
        {
            if (item.orderId == _myNumOrder)
            {
                tempOrderItems.Add((OrderItem)item);
            }
        }
        if (tempOrderItems.Count > 0)
            return tempOrderItems;
        throw new Exception("no items found in order");
    }
    public List<OrderItem> GetOrdersOfOrderItems(int _myItemId)
    {
        List<OrderItem> tempOrders = new List<OrderItem>();
        foreach (OrderItem item in orderItems)
        {
            if (item.itemId == _myItemId)
            {
                tempOrders.Add((OrderItem)item);
            }
        }
        if (tempOrders.Count > 0)
            return tempOrders;
        throw new Exception("item doesnt axist in any order");
    }
    #endregion

    public void Delete(int _myNumOrder)
    {
        foreach (OrderItem item in orderItems)
        {
            if (item.orderId == _myNumOrder)
            {
                orderItems.Remove(item);
            }
        }
    }
    public void Delete(int _myNumOrder, int _myProductBarcode)
    {
        foreach (OrderItem item in orderItems)
        {
            if (item.orderId == _myNumOrder && item.itemId == _myProductBarcode)
            {
                orderItems.Remove(item);
                break;
            }
        }
        throw new Exception("product not found");
    }
    public void Update(OrderItem _newOrderItem)
    {
        foreach (OrderItem item in orderItems)
        {
            if (item.orderId == _newOrderItem.orderId && item.itemId == _newOrderItem.itemId)
            {
                orderItems.Remove(item);
                orderItems.Add(_newOrderItem);
                break;
            }
        }
        throw new Exception("order item not found");
    }
}
