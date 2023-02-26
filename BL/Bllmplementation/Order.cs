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
    #region get functions
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
    public List<OrderItem> GetAllItemsToOrder(int id)
    {
        IEnumerable<DO.OrderItem?>? orderItemList = new List<DO.OrderItem?>();

        orderItemList = Dal!.orderItem.GetOrderItemsFromOrder(id);
        int count = 0;
        return orderItemList!
            .Select(item => new OrderItem()
            {
                NumInOrder = count++,
                ID = item!.Value.id,
                Name = getOrderItemName(item!.Value.itemId),
                Price = item!.Value.price,
                Amount = item!.Value.amount,
                TotalPrice = item!.Value.price * item!.Value.amount
            }).ToList();
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

        switch (orderTracking.Status)
        {

            case OrderStatus.Arrived:
                if (orderTracking.listOfStatus == null) orderTracking.listOfStatus = new List<OrderTracking.StatusAndDate?>();

                orderTracking.listOfStatus.Add(new OrderTracking.StatusAndDate()
                {
                    Date = o.OrderDate,
                    Status = BO.Enums.OrderStatus.Arrived
                });
                break;
            case OrderStatus.Sent:
                if (orderTracking.listOfStatus == null) orderTracking.listOfStatus = new List<OrderTracking.StatusAndDate?>();

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
                if (orderTracking.listOfStatus == null) orderTracking.listOfStatus = new List<OrderTracking.StatusAndDate?>();
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


        return orderTracking;

    }
    public BO.Order GetOrderDetails(int id)
    {
        if (id <= 0)
        {
            throw new NegativeIdException("negative id") { NegativeId = id.ToString() };
        }
        else
        {
            DO.Order o = new();
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
    #endregion

    #region help methods
    /// <summary>
    /// changes order from DO to an BO order
    /// </summary>
    /// <param name="o"></param>
    /// <returns></returns>
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
    /// <summary>
    /// checks the status
    /// </summary>
    /// <param name="OrderDate"></param>
    /// <param name="ShipDate"></param>
    /// <param name="DeliveryDate"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public OrderStatus CheckStatus(DateTime? OrderDate, DateTime? ShipDate, DateTime? DeliveryDate)
    {
        DateTime today = DateTime.Now;
        if (OrderDate != null)
        {
            if (ShipDate == null)
            {
                return OrderStatus.Submitted;
            }
            else
            {
                if (DeliveryDate == null)
                {
                    return OrderStatus.Sent;
                }
                else
                {
                    return OrderStatus.Arrived;
                }
            }
        }
        throw new NotImplementedException();
    }
    /// <summary>
    /// gets amount of items  in order
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="BO.OrderNotExistsException"></exception>
    public int GetAmountItems(int id)
    {
        IEnumerable<DO.OrderItem?> orderItemList = new List<DO.OrderItem?>();
        try
        {
            if (Dal != null)
            {
                orderItemList = Dal.orderItem.GetAll(e => e?.orderId == id);
            }
        }
        catch
        {
            throw new BO.OrderNotExistsException("order not exists,can not get all orderItems") { OrderNotExists = id.ToString() };

        }

        return orderItemList
                       .Where(item => item != null)
                       .Sum(item => item!.Value.amount);



    }
    /// <summary>
    /// checks total sum of order
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public double CheckTotalSum(int id)
    {
        IEnumerable<DO.OrderItem?>? orderItemList = new List<DO.OrderItem?>();

        orderItemList = Dal!.orderItem.GetOrderItemsFromOrder(id);
        double sum = 0;
        return orderItemList
                    .Where(item => item != null)
                    .Sum(item => item!.Value.price * item.Value.amount);
    }
   /// <summary>
   /// checks the name of order by id
   /// </summary>
   /// <param name="productId"></param>
   /// <returns></returns>
   /// <exception cref="Exception"></exception>
    public string getOrderItemName(int productId)
    {
        DO.Product product = new();
        product = Dal!.product.Get(e => e?.barkode == productId);
        return product.productName ?? throw new Exception("no product");
    }
    /// <summary>
    /// builds a new order wiyh wanted data
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <param name="address"></param>
    /// <param name="Email"></param>
    /// <returns></returns>
    private static DO.Order newOrderWithData(int id, string name, string address, string Email)
    {
        DO.Order o = new()
        {
            costumerName = name,
            OrderNum = id,
            mail = Email,
            address = address,
        };
        return o;
    }
    /// <summary>
    /// gets data from user and checks if it is good and if not trow ex
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <param name="address"></param>
    /// <param name="Email"></param>
    /// <exception cref="BO.NegativeIdException"></exception>
    /// <exception cref="BO.EmptyNameException"></exception>
    /// <exception cref="BO.EmptyAddressException"></exception>
    /// <exception cref="BO.EmptyEmailException"></exception>
    public void CheckCorrectData(int id, string name, string address, string Email)
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
    public int GetnextidFromDO()
    {
        int id = Dal!.order.GetNextId();
        return id;
    }
    #endregion
    #region update methods
    /// <summary>
    /// updets the shiping date to be the current date
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    /// <exception cref="BO.OrderNotExistsException"></exception>
    /// <exception cref="BO.OrderHasAlreadyProvidedException"></exception>
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

                    throw new UpdateOrderNotSucceedException("update order not succeed") { UpdateOrderNotSucceed = o.ToString() };
                }
            }

        }
        catch
        {
            throw new BO.OrderHasAlreadyProvidedException("Order has already sent") { OrderHasAlreadyProvided = orderId.ToString() };

        }
        return DOorderToBOorder(o); ;
    }
    /// <summary>
    /// updats the deliveru date to be the current date
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    /// <exception cref="BO.OrderNotExistsException"></exception>
    /// <exception cref="BO.OrderHasAlreadySentException"></exception>
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
            if (CheckStatus(o.OrderDate, o.shippingDate, o.arrivleDate) == BO.Enums.OrderStatus.Submitted)
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
    #endregion
    /// <summary>
    /// gets an order and if all data is ok adds it in to the order list
    /// </summary>
    /// <param name="o"></param>
    /// <exception cref="BO.ProductAlreadyExistsException"></exception>
    public void AddOrder(BO.Order o)
    {

        CheckCorrectData(o.ID, o!.CostumerName, o.CostumerAddress, o.CostumerEmail);
        try
        {
            Dal!.order.Add(newOrderWithData(o.ID, o!.CostumerName, o.CostumerAddress, o.CostumerEmail));
        }
        catch
        {
            throw new BO.ProductAlreadyExistsException("product already exists") { ProductAlreadyExists = o.ToString() };

        }
    }
}
