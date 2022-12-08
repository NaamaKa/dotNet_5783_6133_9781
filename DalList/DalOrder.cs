using DO;
using static Dal.DataSource;
using static Dal.DataSource.Config;

using DalApi;
namespace Dal;

public class DalOrder : IOrder
{
    public int Add(Order _newOrder)
    {
        _newOrder.OrderNum = OrderID;
        orders.Add(_newOrder);
        return _newOrder.OrderNum;
    }
    public Order Get(int _myNum)
    {
        foreach (var order in orders)
        {
            if(order != null)
                if (order?.OrderNum == _myNum)
                    return (Order)order;
        }
        throw new Exception("order not found");
    }
    public List<Order?> GetAll()
    {
        List<Order?> tempOrders = new List<Order?>();
        foreach (var order in orders)
        {
            tempOrders.Add(order);
        }
        return tempOrders;
    }
    public void Delete(int _myNum)
    {
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
        foreach (Order _order in orders)
        {
            if (_order.OrderNum == _newOrder.OrderNum)
            {
                orders.Remove(_order);
                orders.Add(_newOrder);
                break;
            }
        }
    }
}