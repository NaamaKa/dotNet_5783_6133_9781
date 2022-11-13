

namespace DO;

public struct OrderItem
{
    //public int OrderNum { get; set; }
    public int orderId { get; set; }
    public int itemId { get; set; }
    public double price { get; set; }
    public int amount { get; set; }
    public override string ToString() => $@"
        Order ID={orderId}, 
        Item ID - {itemId}
    	Price: {price}
    	Amount: {amount}
    ";
}
