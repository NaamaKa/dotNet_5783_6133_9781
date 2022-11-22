

using DO;
using static Dal.DataSource;
using static Dal.DataSource.Config;


namespace Dal;

public class DalOrder
{
    public int AddNewOrder(Order _newOrder)
    {
       
        _newOrder.OrderNum =OrderID;
       orders[_orderIndex]=_newOrder;
        _orderIndex++;
        return _newOrder.OrderNum;
    }
    public Order GetOrder(int _myNum)
    {
        for (int i = 0; i < _orderIndex; i++)
        {
            if (orders[i].OrderNum == _myNum)
                return orders[i];
        }
        throw new Exception("order not found");
    }
    public Order[] GetOrder()
    {
        Order[] tempList = new Order[_orderIndex];
        for(int i = 0; i < _orderIndex; i++)
        {
            tempList[i]= DataSource.orders[i];
        }
        return tempList;
    }
    public void DeleteOrder(int _myNum)
    {
        for (int i = 0; i < _orderIndex; i++)
        {
            if (orders[i].OrderNum == _myNum)
            {
                if (i ==_orderIndex - 1)//wanted found in last place in product arrey
                {
                   _orderIndex--;
                    return;
                }
                else//found in middle-coppeis last product in array to temp and coppeis wntwd space to temp 
                {
                    Order tempOrder = orders[_orderIndex - 1];
                    orders[i] = tempOrder;
                    _orderIndex--;
                    return;
                }
            }
        }
        throw new Exception("product not found");
    }
    public void UpdateOrder(Order _newOrder)
    {
        for (int i = 0; i < _orderIndex; i++)
        {
            if (orders[i].OrderNum == _newOrder.OrderNum)
            {
                orders[i] = _newOrder;
                break;
            }
        }
    }
}
