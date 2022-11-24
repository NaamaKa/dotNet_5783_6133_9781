using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BO;

internal class Order
{
    public int ID { get; set; }
    public string CostumerName { get; set; }
    public string CostumerEmail { get; set; }
    public string CostumerAddress { get; set; }
    public DateTime OrderDate { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime PaymentDate { get; set; }
    public DateTime ShippingDate { get; set; }
    public DateTime DeliveryDate { get; set; }
    public List<OrderItem> Items { get; set; }
    public double TotalPrice { get; set; }

    public override string ToString() => $@"
        Order number={Id}, 
        name :{CostumerName},
    	email: {CostumerEmail},
    	address: {CostumerAddress},
    	payment date: {PaymentDate},
    	Shipping date: {ShippingDate},
    	delivery date: {DeliveryDate},
        items in order :{Items},

        
    ";
}
