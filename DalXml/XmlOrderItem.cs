using DalApi;
using DalXml;
using DO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal
{

    public class XmlOrderItem : IOrderItem
    {
        string OrderItemPath = @"OrderItems.xml";

        /// <summary>
        /// add a new orderitem and throw exception if it does not exist
        /// </summary>
        /// <param name="_newOrderItem">new one to add</param>
        /// <returns>new orderIdem id</returns>
        /// <exception cref="Exception"></exception>
        public int Add(OrderItem _newOrderItem)
        {
            List<DO.OrderItem?> ListOrderItem = XMLTools.LoadListFromXMLSerializer<OrderItem?>(OrderItemPath);

            if (ListOrderItem.FirstOrDefault(e => e!.Value.orderId == _newOrderItem.id && e.Value.id == _newOrderItem.id) != null)
                throw new ItemAlreadyExistsException("order exists, can not add") { ItemAlreadyExists = _newOrderItem.ToString() };

            //לשנות ID
            _newOrderItem.id = XmlConfig.getOrderItemId();
            ListOrderItem.Add(_newOrderItem); //no need to Clone()
            XMLTools.SaveListToXMLSerializer(ListOrderItem, OrderItemPath);
            return _newOrderItem.id;
        }

        /// <summary>
        /// return specific item by id and throw exception if it does not exist 
        /// </summary>
        /// <param name="orderItemID"></param>
        /// <returns>order item</returns>
        /// <exception cref="Exception"></exception>
        public OrderItem Get(Func<OrderItem?, bool>? predict)
        {

            List<DO.OrderItem?> ListOrderItem = XMLTools.LoadListFromXMLSerializer<OrderItem?>(OrderItemPath);
            if (predict == null)
            {
                throw new GetPredictNullException("the predict is empty") { GetPredictNull = null };
            }
            DO.OrderItem? orderItem = ListOrderItem.Find(p => predict(p));

            if (orderItem != null)
                return (DO.OrderItem)orderItem; //no need to Clone()
            else
                throw new RequestedItemNotFoundException("orderItem not exists,can not do get") { RequestedItemNotFound = predict.ToString() };


        }
        /// <summary>
        /// return all order items and throw exception if it does not exist
        /// </summary>
        /// <returns>order item arr</returns>
        public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? predict = null)
        {
            List<DO.OrderItem?> ListOrderItem = XMLTools.LoadListFromXMLSerializer<OrderItem?>(OrderItemPath);
            if (predict == null)
            {
                return (IEnumerable<OrderItem?>)ListOrderItem;
            }
            IEnumerable<OrderItem?> orderItem = ListOrderItem.FindAll(p => predict(p));

            if (orderItem is null)
                throw new RequestedItemNotFoundException("orderItem not exists,can not do get") { RequestedItemNotFound = predict.ToString() };

            return orderItem;


        }
        /// <summary>
        ///  delete order item and throw exception if it does not exist
        /// 
        /// </summary>
        /// <param name="_orderItemID">order item to delete</param>
        /// <exception cref="Exception"></exception>
        public void Delete(int _orderItemID)
        {
            List<DO.OrderItem?> ListOrderItem = XMLTools.LoadListFromXMLSerializer<DO.OrderItem?>(OrderItemPath);

            DO.OrderItem? ord = ListOrderItem.Find(p => p!.Value.id == _orderItemID);
            try
            {
                if (ord != null)
                {
                    ListOrderItem.Remove(ord);
                    XMLTools.SaveListToXMLSerializer(ListOrderItem, OrderItemPath);
                }
            }
            catch
            {
                throw new RequestedItemNotFoundException("orderItem not exists,can not delete") { RequestedItemNotFound = _orderItemID.ToString() };
            }


        }
        /// <summary>
        ///  update date of product and throw exception if it does not exist
        /// </summary>
        /// <param name="_newOrderItem">order item to update</param>
        /// <exception cref="Exception"></exception>
        public void Update(OrderItem _newOrderItem)
        {
            if (_newOrderItem.id == 0 || _newOrderItem.orderId == 0 || _newOrderItem.id == 0 || _newOrderItem.price == 0 || _newOrderItem.amount == 0)
            {
                return;

            }

            List<DO.OrderItem?> ListOrderItem = XMLTools.LoadListFromXMLSerializer<DO.OrderItem?>(OrderItemPath);
            if (ListOrderItem is null)
                throw new RequestedItemNotFoundException("orderItem not exists,can not do get") { RequestedItemNotFound = _newOrderItem.ToString() };
            DO.OrderItem? ord = ListOrderItem.Find(p => p!.Value.id == _newOrderItem.id);
            try
            {
                if (ord != null)
                {
                    ListOrderItem.Remove(ord);
                    ListOrderItem.Add(_newOrderItem); //no nee to Clone()
                    XMLTools.SaveListToXMLSerializer(ListOrderItem, OrderItemPath);
                }
            }
            catch
            {
                throw new RequestedItemNotFoundException("orderItem not exists,can not update") { RequestedItemNotFound = _newOrderItem.ToString() };
            }


        }

        public OrderItem GetOrderItem(int _myNumOrder, int myProductBarcode)
        {
            throw new NotImplementedException();
        }

        public List<OrderItem?>? GetOrderItemsFromOrder(int _orderId)
        {
            throw new NotImplementedException();
        }

        public List<OrderItem> GetOrdersOfOrderItems(int _itemId)
        {
            throw new NotImplementedException();
        }
    }
}

