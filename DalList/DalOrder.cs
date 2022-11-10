

using DO;
using static Dal.DataSource;

namespace Dal;

public class DalOrder
{
    public int AddNewOrder(Order _newOrder)
    {
        _newOrder.OrderNum = DataSource.Config.OrderID;
        DataSource.orders[DataSource.Config._orderIndex]=_newOrder;
        DataSource.Config._orderIndex++;
        return _newOrder.OrderNum;
    }
    public Order GetOrder(int _myNum)
    {
        for (int i = 0; i < DataSource.Config._orderIndex; i++)
        {
            if (DataSource.orders[i].OrderNum == _myNum)
                return DataSource.orders[i];
        }
        throw new Exception("order not found");
    }
    public Order[] GetOrder()
    {
        Order[] tempList = new Order[DataSource.Config._orderIndex];
        for(int i = 0; i < DataSource.Config._orderIndex; i++)
        {
            tempList[i]= DataSource.orders[i];
        }
        return tempList;
    }
    public void DeleteOrder(int _myNum)
    {
        for (int i = 0; i < DataSource.Config._orderIndex; i++)
        {
            if (DataSource.orders[i].OrderNum == _myNum)
            {
                DataSource.orders[i] = DataSource.orders[DataSource.Config._orderIndex];
                DataSource.Config._orderIndex--;
                break;
            }
        }
        throw new Exception("order not found");
    }
    public void UpdateOrder(Order _newOrder)
    {
        for (int i = 0; i < DataSource.Config._orderIndex; i++)
        {
            if (DataSource.orders[i].OrderNum == _newOrder.OrderNum)
            {
                DataSource.orders[i] = _newOrder;
                break;
            }
        }
    }
}
