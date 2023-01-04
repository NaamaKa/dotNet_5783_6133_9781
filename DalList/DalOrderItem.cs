using DO;
using DalApi;
using System.Security.Cryptography;

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
        try
        {
            return (from OrderItem orderItem in orderItems
                    where (orderItem.Equals(true) && orderItem.orderId == _myNumOrder && orderItem.itemId == myProductBarcode)
                    select orderItem).First();
            //foreach (var item in orderItems)
            //{
            //    if (item != null)
            //        if (item.Value.orderId == _myNumOrder && item.Value.itemId == myProductBarcode)
            //            return item.Value;
            //}
        }
        catch
        {
            throw new RequestedOrderItemNotFoundException("orderItem not exist") { RequestedOrderItemNotFound = _myNumOrder.ToString() };
        }
   
    }
    public List<OrderItem?> GetAll()
    {
        try {
            return orderItems
               .Where(oi => oi.Equals(true))
               .Select(oi => oi).ToList();
        }
        catch
        {
            throw new RequestedOrdersItemNotFoundException("orderItem not exist") {  };

        }
        //foreach (var item in orderItems)
        //{
        //    if (item != null)
        //        tempOrderItems.Add(item);
        //}
        //if (tempOrderItems.Count > 0)
        //    return tempOrderItems;


    }
    public OrderItem Get(int _id)
    {
        try {
            return (from OrderItem orderItem in orderItems
                    where (orderItem.Equals(true) && orderItem.id == _id)
                    select orderItem).First();
        }
        catch
        {
            throw new RequestedOrderItemNotFoundException("orderItem not exist") { RequestedOrderItemNotFound = _id.ToString() };

        }

        //foreach (var item in orderItems)
        //{
        //    if(item!=null)
        //    if (item.Value.id == _id)
        //    {
        //        return item.Value;
        //    }
        //}
    }
    public List<OrderItem> GetOrderItemsFromOrder(int _myNumOrder)
    {
        try
        {
            return (from OrderItem orderItem in orderItems
                    where (orderItem.Equals(true) && orderItem.id == _myNumOrder)
                    select orderItem).ToList();
        }
        catch
        {
            throw new RequestedOrderItemNotFoundException("orderItem not exist") { RequestedOrderItemNotFound = _myNumOrder.ToString() };

        }

        //foreach (var item in orderItems)
        //{
        //    if (item != null)
        //        if (item.Value.orderId == _myNumOrder)
        //    {
        //        tempOrderItems.Add((OrderItem)item);
        //    }
        //}
        //if (tempOrderItems.Count > 0)
        //    return tempOrderItems;
    }
    public List<OrderItem> GetOrdersOfOrderItems(int _myItemId)
    {
        try
        {
            return (from OrderItem orderItem in orderItems
                    where (orderItem.Equals(true) && orderItem.itemId == _myItemId)
                    select orderItem).ToList();
        }
        catch
        {
            throw new RequestedOrderItemNotFoundException("orderItem not exist") { RequestedOrderItemNotFound = _myItemId.ToString() };

        }
        //foreach (var item in orderItems)
        //{
        //    if (item != null)
        //        if (item.Value.itemId == _myItemId)
        //    {
        //        tempOrders.Add((OrderItem)item);
        //    }
        //}
        //if (tempOrders.Count > 0)
        //    return tempOrders;
    }
    #endregion

    public void Delete(int _myNumOrder)
    {
        try
        {
            orderItems.Remove(orderItems
                .Where(item => item is not null && item.Value.orderId == _myNumOrder)
                .Select(order => order).FirstOrDefault());
            //foreach (var item in orderItems)
            //{
            //if (item != null)
            //    if (item.Value.orderId == _myNumOrder)
            //{
            //    orderItems.Remove(item);
            //}
            //}
        }
        catch
        {
            throw new RequestedOrderNotFoundException("order not exist") { RequestedOrderNotFound = _myNumOrder.ToString() };
         
        }
    }
    public void Delete(int _myNumOrder, int _myProductBarcode)
    {
        try {
            orderItems.Remove(orderItems
               .Where(item => item is not null && item.Value.orderId == _myNumOrder && item.Value.itemId == _myProductBarcode)
               .Select(order => order).FirstOrDefault());
            //foreach (var item in orderItems)
            //{
            //    if (item != null)
            //        if (item.Value.orderId == _myNumOrder && item.Value.itemId == _myProductBarcode)
            //    {
            //        orderItems.Remove(item);
            //        break;
            //    }
            //}
        }
        catch 
        {
            throw new RequestedOrderNotFoundException("order not exist") { RequestedOrderNotFound = _myNumOrder.ToString() };
        }
        
   
        
    }
    public void Update(OrderItem _newOrderItem)
    {
        try {
            orderItems.Remove(orderItems
               .Where(item => item is not null && item.Value.orderId == _newOrderItem.orderId && item.Value.itemId == _newOrderItem.itemId)
               .Select(order => order).FirstOrDefault());
            orderItems.Add(_newOrderItem);
            //foreach (var item in orderItems)
            //{
            //    if (item != null)
            //        if (item.Value.orderId == _newOrderItem.orderId && item.Value.itemId == _newOrderItem.itemId)
            //    {
            //        orderItems.Remove(item);
            //        orderItems.Add(_newOrderItem);
            //        break;
            //    }
            //}
            }
        catch
        {
            throw new RequestedOrderNotFoundException("order not exist") { RequestedOrderNotFound = _newOrderItem.ToString() };
        }
          
    }
}
