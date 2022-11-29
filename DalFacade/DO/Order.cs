

using System.Diagnostics;

namespace DO;

public struct Order
   
{
  
    public int OrderNum { get; set; }
    public string? costumerName { get; set; }
    public string? mail { get; set; }
    public string? address { get; set; }
    public DateTime? OrderDate { get; set; }
    public DateTime? shippingDate { get; set; }
    public DateTime? arrivleDate { get; set; }
    public override string ToString() => $@"
        Order ID={OrderNum}, 
        Costumer name {costumerName}
    	Costumer email: {mail}
    	Costumer address: {address}
    	Order date: {OrderDate}
    	Shipping date: {shippingDate}
    	Arrivle date: {arrivleDate}
    ";
}
