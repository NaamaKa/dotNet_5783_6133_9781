using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using DalApi;
using static BO.Enums;
namespace Bllmplementation;

internal class Order : BlApi.IOrder
{
    DalApi.IDal? Dal = DalApi.Factory.Get();
    /// <summary>
    ///  return all orders for list
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NoOrdersForListExeption">no orders for list found</exception>
    public IEnumerable<OrderForList> GetListOfOrders()
    { 
        IEnumerable<DO.Order?> orderList = Dal!.order.GetAll();
        try
        {
            return orderList
          .Where(item => item != null)
          .Select(item => new OrderForList()
          {
              ID = item!.Value.OrderNum,
              CostumerName = item.Value.costumerName,
              Status = CheckStatus(item.Value.OrderDate, item.Value.shippingDate, item.Value.arrivleDate),
              AmountOfItems = GetAmountItems(item.Value.OrderNum),
              TotalPrice = CheckTotalSum(item.Value.OrderNum)
          });
        }
        catch
        {
            
            throw new NoOrdersForListExeption("no orders for list") { };
        }
    }
    public void AddOrder(BO.Order o)
    {

        CheckCorrectData(o.ID,o!.CostumerName, o.CostumerAddress, o.CostumerEmail);
        try
        {
            Dal!.order.Add(newOrderWithData(o.ID, o!.CostumerName, o.CostumerAddress, o.CostumerEmail));
        }
        catch
        {
            throw new BO.ProductAlreadyExistsException("product already exists") { ProductAlreadyExists = o.ToString() };

        }
    }
    private static DO.Order newOrderWithData(int id, string name, string address, string Email)
    {
        DO.Order o = new()
        {
            costumerName = name,
            OrderNum=id,
            mail=Email,
            address=address,
        };
        return o;
    }
    public void CheckCorrectData(int id, string name,string address,string Email)
    {
        if (id < 0)
        {
            throw new BO.NegativeIdException("negative id") { NegativeId = id.ToString() };
        }
        if (string.IsNullOrEmpty(name))
        {
            throw new BO.EmptyNameException("empty name") { EmptyName = name!.ToString() };
        }
        if (string.IsNullOrEmpty(address))
        {
            throw new BO.EmptyAddressException("empty address") { EmptyAddress = name!.ToString() };
        }
        if (string.IsNullOrEmpty(Email))
        {
            throw new BO.EmptyEmailException("empty Email") { EmptyEmail = name!.ToString() };
        }
        return;

    }
    public BO.Order GetOrderDetails(int id)
    {
        if (id <= 0)
        {
            throw new NegativeIdException("negative id") { NegativeId = id.ToString() };
        }
        else
        {
            DO.Order o = new DO.Order();
            try
            {
                o = Dal!.order.Get(e => e?.OrderNum == id);
            }
            catch (DO.RequestedItemNotFoundException)
            {
                throw new OrderNotExistsException("order not exists") { OrderNotExists = o.ToString() };

            }
            return DOorderToBOorder(o);
        }
    }
    public BO.OrderTracking GetOrderTracking(int orderId)
    {
        DO.Order o = new DO.Order();
        try
        {
            o = Dal!.order.Get(e => e?.OrderNum == orderId);
        }
        catch (DO.RequestedItemNotFoundException)
        {
            throw new OrderNotExistsException("order not exists") { OrderNotExists = o.ToString() };

        }
        OrderTracking orderTracking = new OrderTracking();
        orderTracking.ID = orderId;
        orderTracking.Status = CheckStatus(o.OrderDate, o.shippingDate, o.arrivleDate);
        if (orderTracking.listOfStatus != null)
        {
            switch (orderTracking.Status)
            {

                case OrderStatus.Arrived:
                    orderTracking.listOfStatus.Add(new OrderTracking.StatusAndDate()
                    {
                        Date = o.OrderDate,
                        Status = BO.Enums.OrderStatus.Arrived
                    });
                    break;
                case OrderStatus.Sent:
                    orderTracking.listOfStatus.Add(new OrderTracking.StatusAndDate()
                    {
                        Date = o.OrderDate,
                        Status = BO.Enums.OrderStatus.Arrived
                    });
                    orderTracking.listOfStatus.Add(new OrderTracking.StatusAndDate()
                    {
                        Date = o.shippingDate,
                        Status = BO.Enums.OrderStatus.Sent

                    });
                    break;
                case OrderStatus.Submitted:
                    orderTracking.listOfStatus.Add(new OrderTracking.StatusAndDate()
                    {
                        Date = o.OrderDate,
                        Status = BO.Enums.OrderStatus.Arrived
                    });
                    orderTracking.listOfStatus.Add(new OrderTracking.StatusAndDate()
                    {
                        Date = o.shippingDate,
                        Status = BO.Enums.OrderStatus.Sent

                    }); orderTracking.listOfStatus.Add(new OrderTracking.StatusAndDate()
                    {
                        Date = o.arrivleDate,
                        Status = BO.Enums.OrderStatus.Submitted

                    });
                    break;
            }
        }
        else
        {
            throw new Exception("no status");
        }
        return orderTracking;

    }
    public int GetnextidFromDO()
    {
        int id = Dal!.order.GetNextId();
        return id;
    }
    public BO.Order UpdateDeliveryDate(int orderId)
    {
        DO.Order o = new DO.Order();
        try
        {
            o = Dal!.order.Get(e => e?.OrderNum == orderId);
        }
        catch
        {
            throw new BO.OrderNotExistsException("order not exists") { OrderNotExists = o.ToString() };

        }
        try
        {
            if (CheckStatus(o.OrderDate, o.shippingDate, o.arrivleDate) == BO.Enums.OrderStatus.Sent)
            {
                o.arrivleDate = DateTime.Now;
                try
                {
                    Dal.order.Update(o);
                }
                catch (DO.RequestedUpdateItemNotFoundException)
                {

                    throw new BO.UpdateOrderNotSucceedException("update order not succeed") { UpdateOrderNotSucceed = o.ToString() };
                }
            }

        }
        catch
        {
            throw new BO.OrderHasAlreadyProvidedException("Order has already sent") { OrderHasAlreadyProvided = orderId.ToString() };

        }
        return DOorderToBOorder(o); ;
    }

    public BO.Order UpdateShipDate(int orderId)
    {
        DO.Order o = new DO.Order();
        try
        {
            o = Dal!.order.Get(e => e?.OrderNum == orderId);
        }
        catch (DO.RequestedItemNotFoundException)
        {
            throw new BO.OrderNotExistsException("order not exists") { OrderNotExists = o.ToString() };

        }
        try
        {
            if (CheckStatus(o.OrderDate, o.shippingDate, o.arrivleDate) == BO.Enums.OrderStatus.Arrived)
            {
                o.shippingDate = DateTime.Now;
                try
                {
                    Dal.order.Update(o);
                }
                catch (DO.RequestedUpdateItemNotFoundException)
                {

                    throw new UpdateOrderNotSucceedException("update order not succeed") { UpdateOrderNotSucceed = o.ToString() };
                }


            }

        }
        catch
        {
            throw new BO.OrderHasAlreadySentException("Order has already sent") { OrderHasAlreadySent = orderId.ToString() };

        }
        return DOorderToBOorder(o); ;
    }
    #region ezer
    public BO.Order DOorderToBOorder(DO.Order o)
    {
        BO.Order newOrder = new BO.Order()
        {
            ID = o.OrderNum,
            CostumerName = o.costumerName,
            CostumerEmail = o.mail,
            CostumerAddress = o.address,
            Status = CheckStatus(o.OrderDate, o.shippingDate, o.arrivleDate),
            PaymentDate = o.OrderDate,
            ShippingDate = o.shippingDate,
            DeliveryDate = o.arrivleDate,
            Items = GetAllItemsToOrder(o.OrderNum)!,
            TotalPrice = CheckTotalSum(o.OrderNum)


        };
        return newOrder;
    }
    public OrderStatus CheckStatus(DateTime? OrderDate, DateTime? ShipDate, DateTime? DeliveryDate)
    {
        DateTime today = DateTime.Now;
        if (today.Equals(OrderDate) && today.Equals(ShipDate) && today.Equals(DeliveryDate))
            return OrderStatus.Submitted;
        else if (today.Equals(OrderDate) && today.Equals(ShipDate))
            return OrderStatus.Sent;
        else
            return OrderStatus.Arrived;
    }
    public int GetAmountItems(int id)
    {
        IEnumerable<DO.OrderItem> orderItemList = new List<DO.OrderItem>();
        orderItemList = Dal!.orderItem.GetOrderItemsFromOrder(id);
        int sum = 0;
        var newSum=from OrderItem item in orderItemList
                   select ( sum += item.Amount).ToString(); 
        return sum;

    }
    public double CheckTotalSum(int id)
    {
        IEnumerable<DO.OrderItem> orderItemList = new List<DO.OrderItem>();
        orderItemList = Dal!.orderItem.GetOrderItemsFromOrder(id);
        double sum = 0;
        var newSum = from OrderItem item in orderItemList
                     select (sum = sum + item.Price * item.Amount).ToString();
        return sum;
    }
    public List<OrderItem> GetAllItemsToOrder(int id)
    {
        IEnumerable<DO.OrderItem> orderItemList = new List<DO.OrderItem>();
        orderItemList = Dal!.orderItem.GetOrderItemsFromOrder(id);
        int count = 0;
        return orderItemList
            .Select(item => new OrderItem()
            {
                NumInOrder = count++,
                ID = item.id,
                Name = getOrderItemName(item.itemId),
                Price = item.price,
                Amount = item.amount,
                TotalPrice = item.price * item.amount
            }).ToList();
    }
    public string getOrderItemName(int productId)
    {
        DO.Product product = new();
        product = Dal!.product.Get(e => e?.barkode == productId);
        return product.productName ?? throw new Exception("no product");
    }
    #endregion
}
