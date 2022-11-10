

using DO;

namespace Dal;

public class DalOrderItem
{
    public void AddNewOrderItem(OrderItem _newOrderItem)
    {
        DataSource.orderItems[DataSource.Config._orderItemIndex] = _newOrderItem;
        DataSource.Config._orderItemIndex++;
    }
    public OrderItem GetOrderItem(int _myNumOrder,int myProductBarcode)
    {
        for (int i = 0; i < DataSource.Config._orderItemIndex; i++)
        {
            if (DataSource.orderItems[i].orderId == _myNumOrder&& DataSource.orderItems[i].itemId== myProductBarcode)
                return DataSource.orderItems[i];
        }
        throw new Exception("item not found in order");
    }
    public OrderItem[] GetOrderItem()
    {
        OrderItem[] _orderItems = new OrderItem[DataSource.Config._orderItemIndex];
        for(int i=0;i<DataSource.Config._orderItemIndex;i++)
        {
            _orderItems[i]=DataSource.orderItems[i];
        }
        return _orderItems;
    }
    public void DeleteOrderItem(int _myNumOrder, int myProductBarcode)
    {
        for (int i = 0; i < DataSource.Config._orderItemIndex; i++)
        {
            if (DataSource.orderItems[i].orderId == _myNumOrder && DataSource.orderItems[i].itemId == myProductBarcode)
            {
                DataSource.orderItems[i] = DataSource.orderItems[DataSource.Config._orderItemIndex];
                DataSource.Config._orderItemIndex--;
                break;
            }
        }
        throw new Exception("utem not found in order");
    }
    public void UpdateOrderItem(OrderItem _newOrderItem)
    {
        for (int i = 0; i < DataSource.Config._orderItemIndex; i++)
        {
            if (DataSource.orderItems[i].orderId == _newOrderItem.orderId && DataSource.orderItems[i].itemId == _newOrderItem.itemId)
            {
                DataSource.orderItems[i] = _newOrderItem;
                break;
            }
        }
    }
}
