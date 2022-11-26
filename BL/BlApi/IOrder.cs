using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface IOrder
{
    public List<OrderForList> GetOrders (int ID);
   // public OrderForList GetOrderFor { set; get; }
   // public OrderForList UpdateShippingOrder { set; get; }
    //public OrderForList UpdateOrderToProvide { set; get; }
    //public OrderTracking OrderTracking { set; get; }
}
