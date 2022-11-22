

using DO;

namespace Dal;
using static Dal.DataSource.Config;
using static Dal.DataSource;

public class DalOrderItem
{
    public void AddNewOrderItem(OrderItem _newOrderItem)
    {
       orderItems[_orderItemIndex] = _newOrderItem;
        _orderItemIndex++;
    }
    public OrderItem GetOrderItem(int _myNumOrder,int myProductBarcode)
    {
        for (int i = 0; i < _orderItemIndex; i++)
        {
            if (orderItems[i].orderId == _myNumOrder && orderItems[i].itemId == myProductBarcode)
            {
                Console.WriteLine(i);
                Console.WriteLine("came in");
                return orderItems[i];
            }
        }
        throw new Exception("item not found in order");
    }
    public OrderItem[] GetOrderItem()
    {
        OrderItem[] _orderItems = new OrderItem[_orderItemIndex];
        for(int i=0;i<_orderItemIndex;i++)
        {
            _orderItems[i]=orderItems[i];
        }
        if (_orderItems.Length>0)
             return _orderItems;
        throw new Exception("no items found");

    }
    public OrderItem[] GetOrderItems(int _myNumOrder)
    {
        OrderItem[] _orderItemsTemp=new OrderItem[_orderItemIndex];
        int counter = 0;
        for (int i = 0; i < _orderItemIndex; i++)
        {
            if (orderItems[i].orderId == _myNumOrder)
                _orderItemsTemp[counter++] = orderItems[i];
        }
        if (counter > 0)
        {
            OrderItem[] _orderItems = new OrderItem[counter];
            for(int i = 0; i < counter; i++)
            {
                _orderItems[i] = _orderItemsTemp[i];
            }
            return _orderItems;

        }
        throw new Exception("no items found in order");
    }
    public void DeleteOrderItem(int _myNumOrder, int _myProductBarcode)
    {
        for (int i = 0; i <_orderItemIndex; i++)
        {
            if (orderItems[i].orderId == _myNumOrder && orderItems[i].itemId==_myProductBarcode)
            {
                if (i == _orderItemIndex - 1)//wanted found in last place in product arrey
                {
                   _orderItemIndex--;
                    return;
                }
                else//found in middle-coppeis last product in array to temp and coppeis wntwd space to temp 
                {
                    OrderItem tempOrderItem = orderItems[_orderItemIndex - 1];
                    orderItems[i] = tempOrderItem;
                    _orderItemIndex--;
                    return;
                }
            }
        }
        throw new Exception("product not found");
    }
    public void UpdateOrderItem(OrderItem _newOrderItem)
    {
        for (int i = 0; i < _orderItemIndex; i++)
        {
            if (orderItems[i].orderId == _newOrderItem.orderId && orderItems[i].itemId == _newOrderItem.itemId)
            {
                orderItems[i] = _newOrderItem;
              return;
            }
        }
        throw new Exception("order item not found");
    }
}
