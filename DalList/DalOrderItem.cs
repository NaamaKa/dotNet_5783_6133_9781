using DO;
using DalApi;
namespace Dal;
using static Dal.DataSource.Config;
using static Dal.DataSource;

public class DalOrderItem : IOrderItem
{
    /// <summary>
    /// return specific item by id and throw exception if it does not exist
    /// </summary>
    /// <param name="orderItemID"></param>
    /// <returns>order item</returns>
    /// <exception cref="Exception"></exception>
    public OrderItem Get(Func<OrderItem?, bool>? predict)
    {

        if (orderItems == null)
        {
            throw new RequestedItemNotFoundException("orderItem not exists,can not do get") { RequestedItemNotFound = predict?.ToString() };
        }
        if (predict == null)
        {
            throw new GetPredictNullException("the predict is empty") { GetPredictNull = null };
        }
        OrderItem? _newOrderItem = new OrderItem();
        _newOrderItem = orderItems.Find(e => predict(e));
        if (_newOrderItem.HasValue)
            return (OrderItem)_newOrderItem;
        throw new RequestedItemNotFoundException("orderItem not exists,can not do get") { RequestedItemNotFound = predict.ToString() };


    }
    /// <summary>
    /// return all order items and throw exception if it does not exist
    /// </summary>
    /// <returns>order item arr</returns>
    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? predict = null)
    {
        if (orderItems == null)
        {
            throw new RequestedItemNotFoundException("order not exists,can not get") { RequestedItemNotFound = predict?.ToString() };
        }
        if (predict == null)
        {
            return (IEnumerable<OrderItem?>)orderItems;

        }

        List<OrderItem?> _OrderItems = new List<OrderItem?>();
        _OrderItems = orderItems.FindAll(e => predict(e));
        return _OrderItems;

    }
    public int Add(OrderItem _newOrderItem)
    {
        _newOrderItem.id = IdOrderItem;
        orderItems.Add(_newOrderItem);
        return _newOrderItem.id;
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
    public List<OrderItem?> GetAll()
    {
        List<OrderItem?> tempOrderItems = new List<OrderItem?>();
        foreach (var item in orderItems)
        {
            if(item != null)
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
