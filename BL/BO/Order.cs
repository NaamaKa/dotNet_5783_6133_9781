using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class Order
{
    #region proporties
    public int ID { get; set; }
    public string? CostumerName { get; set; }
    public string? CostumerEmail { get; set; }
    public string? CostumerAddress { get; set; }
    public Enums.OrderStatus Status { get; set; }
    public DateTime? PaymentDate { get; set; }
    public DateTime? ShippingDate { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public List<OrderItem> Items { get; set; }
    public double TotalPrice { get; set; }
    #endregion

    #region tostring
    public override string ToString() => $@"
        Order number={ID}, 
        name :{CostumerName},
    	email: {CostumerEmail},
    	address: {CostumerAddress},
    	payment date: {PaymentDate},
    	Shipping date: {ShippingDate},
    	delivery date: {DeliveryDate},
        items in order :{Items},
        total price:{TotalPrice} 
    ";
    #endregion

}
