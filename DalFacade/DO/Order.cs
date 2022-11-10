

namespace DO;

public struct Order
   
{
    static int counter = 10000;
    public int OrderNum { get { return OrderNum; } set { counter++; }  }
    public string costumerName { get; set; }
    public string mail { get; set; }
    public string address { get; set; }
    public DateTime OrderDate { get { return OrderDate; } set { OrderDate=DateTime.Now; } }
    public DateTime shippingDate { get; set; }
    public DateTime arrivleDate { get; set; }


}
