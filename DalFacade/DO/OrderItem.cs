

namespace DO;

public struct OrderItem
{
    public int id { get; set; }
    public int orderId { get; set; }
    public int itemId { get; set; }
    public double price { get; set; }
    public int amount { get; set; }
    public override string ToString() => $@"
        id={id},
        Order ID={orderId}, 
        Item ID - {itemId}
    	Price: {price}
    	Amount: {amount}
    ";
}
