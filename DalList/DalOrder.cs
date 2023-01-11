using DO;
using static Dal.DataSource;
using static Dal.DataSource.Config;

using DalApi;
using System.Linq;
using System.Security.Cryptography;

namespace Dal;

public class DalOrder : IOrder
{
    /// <summary>
    /// check if the order demanded exist and return it or an exception if not
    /// </summary>
    /// <param name="_num">the id of the order demanded</param>
    /// <returns>details of the order demanded</returns>
    /// <exception cref="Exception">order not exists</exception>
    public Order Get(Func<Order?, bool>? predict)
    {
        if (orders == null)
        {
            throw new RequestedItemNotFoundException("order not exists,can not get") { RequestedItemNotFound = predict?.ToString() };
        }
        if (predict == null)
        {
            throw new GetPredictNullException("the predict is empty") { GetPredictNull = null };
        }
        Order? _orderToGet = new Order();
        _orderToGet = orders.Find(e => predict(e));
        if (_orderToGet.HasValue)
            return (Order)_orderToGet;
        else
            throw new RequestedItemNotFoundException("order not exists,can not get") { RequestedItemNotFound = predict?.ToString() };

    }

    /// <summary>
    /// cope the orders to a new arrey and return it
    /// </summary>
    /// <returns>arrey with all the orders</returns>
    public IEnumerable<Order?> GetAll(Func<Order?, bool>? predict = null)
    {
        if (orders == null)
        {
            throw new RequestedItemNotFoundException("order not exists,can not get") { RequestedItemNotFound = predict?.ToString() };
        }
        if (predict == null)
        {
            return orders;
        }

        List<Order?> _Orders = new List<Order?>();
        _Orders = orders.FindAll(e => predict(e));
        return _Orders;
    }
    public int Add(Order _newOrder)
    {
        _newOrder.OrderNum = OrderID;
        orders.Add(_newOrder);
        return _newOrder.OrderNum;
    }
    public Order Get(int _myNum) 
    {
            var listToReturn = from Order order in orders
                               where order.Equals(true) && order.OrderNum == _myNum
                               select order;
            return listToReturn.First();
            throw new Exception("order not found");
    }
    public List<Order?> GetAll()
    {
        return (from Order? o in orders
               select o).ToList();
    }
    public void Delete(int _myNum)
    {
        try
        {
            orders.Remove(orders
              .Where(o => o is not null && o.Value.OrderNum == _myNum)
              .Select(order => order).FirstOrDefault());
        }
        catch
        {
            throw new RequestedOrderNotFoundException("order not foud") { RequestedOrderNotFound = _myNum.ToString() };
        }
    }
    public int GetNextId()
    {
        int temp = OrderID;
        int temp2 = DicreaseProductId;
        return temp;
    }
    public void Update(Order _newOrder)
    {
        try
        {
            orders.Remove(orders
              .Where(order => order is not null && order.Value.OrderNum == _newOrder.OrderNum)
              .Select(order => order).FirstOrDefault());
            orders.Add(_newOrder);
        }
        catch
        {
            throw new RequestedOrderNotFoundException("order not foud") { RequestedOrderNotFound = _newOrder.ToString() };
        }
    }
}