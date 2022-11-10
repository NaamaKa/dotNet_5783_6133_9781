

namespace DO;

public struct OrderItem
{
    static int counter = 10000;
    public int OrderNum { get { return OrderNum; } set { counter++; } }
    public int orderId { get; set; }
    public int itemId { get; set; }
    public float price { get; set; }
    public int amount { get; set; }
}
