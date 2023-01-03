using DO;
using static Dal.DataSource;
using static Dal.DataSource.Config;

using DalApi;
using System.Linq;

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
            return (IEnumerable<Order?>)orders;
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
        //return orders.Where ( order=> order. = _myNum)
        //{
        //var listToReturn= from Order order in orders
        //       where order.Equals(true) && order.OrderNum == _myNum
        //       select order;
        //return listToReturn.ToList();
        foreach (var order in orders)
        {
            if (order != null)
                if (order?.OrderNum == _myNum)
                    return (Order)order;
        }
        throw new Exception("order not found");
    }
    public List<Order?> GetAll()
    {
        return (from Order? o in orders
               select o).ToList();
    }
    public void Delete(int _myNum)
    {
        //var item= orders
        //    .Where(order=> order !=null)
        //    .Where(order => order.OrderID = _myNum)
        foreach (var _order in orders)
        {
            if (OrderID == _myNum)
            {
                orders.Remove(_order);
                break;
            }
        }
        throw new Exception("product not found");
    }
    public void Update(Order _newOrder)
    {
        foreach (var _order in orders)
        {
            if(_order != null)
            if (_order.Value.OrderNum == _newOrder.OrderNum)
            {
                orders.Remove(_order);
                orders.Add(_newOrder);
                break;
            }
        }
    }
}