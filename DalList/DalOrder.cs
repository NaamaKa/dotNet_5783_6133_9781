

using DO;
using static Dal.DataSource;
using static Dal.DataSource.Config;


namespace Dal;

public class DalOrder
{
    public int AddNewOrder(Order _newOrder)
    {
       
        _newOrder.OrderNum =OrderID;
       orders.Add(_newOrder);
        return _newOrder.OrderNum;
    }
    public Order GetOrder(int _myNum)
    {
        foreach(var order in orders)
        {
            if(order.OrderNum == _myNum)
                return order;
        }
        throw new Exception("order not found");
    }
    public List<Order> GetOrder()
    {
        List<Order> tempOrders = new List<Order>();
        foreach(var order in orders)
        {
            tempOrders.Add(order);
        }
        return tempOrders;
    }
    public void DeleteOrder(int _myNum)
    {
        foreach(Order _order in orders)
        {
            if(OrderID == _myNum)
            {
                orders.Remove(_order);
                break;
            }
        }
        throw new Exception("product not found");
    }
    public void UpdateOrder(Order _newOrder)
    {
        foreach(Order _order in orders)
        {
            if(_order.OrderNum== _newOrder.OrderNum)
            {
                orders.Remove(_order);
                orders.Add(_newOrder);
                break;
            }
        }
    }
}
