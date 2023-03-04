using DalApi;
using Dal;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DalXml;

namespace Dal;
public class XmlOrder : IOrder
{
    string OrderPath = @"Orders.xml";


    /// <summary>
    /// add a new order to the list of orders
    /// </summary>
    /// <param name="_p">an order</param>
    /// <returns>int of the id of the order</returns>
    /// <exception cref="Exception">order exists</exception>
    public int Add(DO.Order _o)
    {
        XElement OrdersRoot = XMLTools.LoadListFromXMLElement(OrderPath);
        if (OrdersRoot == null)
        {
            throw new RequestedItemNotFoundException("order not exists,can not get") { RequestedItemNotFound = _o.ToString() };
        }

        XElement ifExsistOrder = (from p in OrdersRoot.Elements()
                                  where int.Parse(p.Element("ID")!.Value) == _o.OrderNum
                                  select p).FirstOrDefault()!;

        if (ifExsistOrder != null)

            throw new DO.ItemAlreadyExistsException("order exists, can not add") { ItemAlreadyExists = _o.ToString() };
        int id = XmlConfig.getOrderId();
        XElement OrderElement = new XElement("Order", new XElement("ID", id.ToString()),
                                new XElement("CustomerEmail", _o.mail),
                                new XElement("CustomerName", _o.costumerName),
                                new XElement("CustomerAdress", _o.address),
                                new XElement("OrderDate", DateTime.Now.ToString()),
                                new XElement("ShipDate", _o.shippingDate == null ? null : _o.shippingDate.ToString()),
                                new XElement("DeliveryDate", _o.arrivleDate == null ? null : _o.arrivleDate.ToString()));

        OrdersRoot.Add(OrderElement);
        XMLTools.SaveListToXMLElement(OrdersRoot, OrderPath);
        return id;
    

    }
    /// <summary>
    /// check if the order demanded exist and return it or an exception if not
    /// </summary>
    /// <param name="_num">the id of the order demanded</param>
    /// <returns>details of the order demanded</returns>
    /// <exception cref="Exception">order not exists</exception>
    public Order Get(Func<Order?, bool>? predict)
    {
        XElement OrdersRoot = XMLTools.LoadListFromXMLElement(OrderPath);

        if (OrdersRoot == null)
        {
            throw new RequestedItemNotFoundException("order not exists,can not get") { RequestedItemNotFound = predict?.ToString() };
        }
        if (predict == null)
        {
            throw new GetPredictNullException("the predict is empty") { GetPredictNull = null };
        }

        try
        {
            IEnumerable<DO.Order?>? ord = OrdersRoot.Elements().Select(x =>
            {
                DO.Order o = new();
                o.OrderNum = Int32.Parse(x.Element("ID")!.Value);
                o.address = x.Element("CustomerAdress")!.Value;
                o.mail = x.Element("CustomerEmail")!.Value;
                o.costumerName = x.Element("CustomerName")!.Value;
                o.shippingDate = DateTime.Parse(x.Element("ShipDate")!.Value);
                o.arrivleDate = DateTime.Parse(x.Element("DeliveryDate")!.Value);
                o.OrderDate = DateTime.Parse(x.Element("OrderDate")!.Value);
                return (DO.Order?)o;
            }).Where(x => predict(x));
            return (Order)ord.FirstOrDefault()!;
        }
        catch
        {
            throw new RequestedItemNotFoundException("order not exists,can not get") { RequestedItemNotFound = predict?.ToString() };
        }

       

    }

    /// <summary>
    /// cope the orders to a new arrey and return it
    /// </summary>
    /// <returns>arrey with all the orders</returns>
    public IEnumerable<Order?> GetAll(Func<Order?, bool>? predict = null)
    {
        XElement OrdersRoot = XMLTools.LoadListFromXMLElement(OrderPath);
        if (OrdersRoot == null)
            throw new RequestedItemNotFoundException("orders not exists,can not get") { RequestedItemNotFound = predict?.ToString() };
        try
        {
            IEnumerable<DO.Order?>? ord = OrdersRoot.Elements().Select(x =>
            {
                DO.Order o = new();
                o.OrderNum = Int32.Parse(x.Element("ID")!.Value.ToString());
                o.address = x.Element("CustomerAdress")!.Value.ToString();
                o.mail = x.Element("CustomerEmail")!.Value.ToString();
                o.costumerName = x.Element("CustomerName")!.Value.ToString();
                try
                {
                    o.shippingDate = DateTime.Parse(x.Element("ShipDate")!.Value.ToString());
                    o.arrivleDate = DateTime.Parse(x.Element("DeliveryDate")!.Value.ToString());
                    o.OrderDate = DateTime.Parse(x.Element("OrderDate")!.Value.ToString());
                }
                catch
                {
                    o.shippingDate = null;
                    o.arrivleDate = null;
                    o.OrderDate = null;
                }
                return (DO.Order?)o;
            }).Where(x => predict == null || predict(x));
            return ord;
        }
        catch
        {
            throw new RequestedItemNotFoundException("order not exists,can not get") { RequestedItemNotFound = predict?.ToString() };
        }
    }

    /// <summary>
    /// check if the order demanded exist and delete it or throw an exception if not
    /// </summary>
    /// <param name="_num">id of order to delete</param>
    /// <exception cref="Exception">order not exists, can not delete</exception>
    public void Delete(int _id)
    {

        #region temp

        List<DO.Order?> ListOrders = XMLTools.LoadListFromXMLSerializer<DO.Order?>(OrderPath);

        DO.Order? ord = ListOrders.Find(p => p!.Value.OrderNum == _id);
        try
        {
            if (ord != null)
            {
                ListOrders.Remove(ord);
                XMLTools.SaveListToXMLSerializer(ListOrders, OrderPath);
            }
        }
        catch
        {
            throw new RequestedItemNotFoundException("order not exists,can not delete") { RequestedItemNotFound = _id.ToString() };
        }
        #endregion
    }

    /// <summary>
    /// update date of order and throw exception if it does not exist
    /// </summary>
    /// <param name="_p"> id of order demanded to change</param>
    /// <exception cref="Exception">product not exists, can not update</exception>
    public void Update(Order _o)
    {

        #region temp
        List<DO.Order?> ListOrders = XMLTools.LoadListFromXMLSerializer<DO.Order?>(OrderPath);
        if (ListOrders is null)
            throw new RequestedItemNotFoundException("orderItem not exists,can not do get") { RequestedItemNotFound = _o.ToString() };
        DO.Order? ord = ListOrders.Find(p => p!.Value.OrderNum == _o.OrderNum);
        try
        {
            if (ord != null)
            {
                ListOrders.Remove(ord);
                ListOrders.Add(_o); //no nee to Clone()
                XMLTools.SaveListToXMLSerializer(ListOrders, OrderPath);
            }
        }
        catch
        {
            throw new RequestedItemNotFoundException("orderItem not exists,can not update") { RequestedItemNotFound = _o.ToString() };
        }
        #endregion

    }

    public int GetNextId()
    {
        throw new NotImplementedException();
    }
}

