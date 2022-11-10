

using DO;
using System;

namespace Dal;

internal static class DataSource
{

    static DataSource()
    {
        s_Initialize();
    }
    

    static readonly Random  rnd = new Random();
    internal static  Product[] products = new Product[50];
    internal static OrderItem[] orderItems = new OrderItem[200];
    internal static Order[] orders = new Order[100];

    static private void addNewProduct(string PName,string PCategory,float PPrice,int PInStock)
    {
       products[Config._productIndex].barkode = Config.ProductID;
        products[Config._productIndex].productName = PName;
        products[Config._productIndex].productCategory =PCategory;
        products[Config._productIndex].productPrice = PPrice;
        products[Config._productIndex].inStock =PInStock;
        Config._productIndex++;
    }
    static private void addNewOrder(string Oname,string Omail,string Oaddress, DateTime OshippingDate,DateTime OarrivleDate)
    {
        orders[Config._orderIndex].OrderNum = Config.OrderID;
        orders[Config._orderIndex].OrderDate = DateTime.Now;
        orders[Config._orderIndex].OrderNum = 123;
        orders[Config._orderIndex].costumerName = Oname;
        orders[Config._orderIndex].shippingDate =OshippingDate;
        orders[Config._orderIndex].arrivleDate =OarrivleDate;
        orders[Config._orderIndex].mail = Omail;
        orders[Config._orderIndex].address = Oaddress;
        Config._orderIndex++;
    }
    static private void addNewOrderItem(int OrderItemId,int OrderId,float Oprice,int Oamount)
    {
       // orderItems[Config._orderItemIndex].OrderNum = 123;
        orderItems[Config._orderItemIndex].itemId =OrderItemId;
        orderItems[Config._orderItemIndex].orderId =OrderId;
        orderItems[Config._orderItemIndex].price = Oprice;
        orderItems[Config._orderItemIndex].amount = Oamount;
        Config._orderItemIndex++;
    }
    static private void s_Initialize()
    {
        Product[] productsArr = new Product[10];
        OrderItem[] orderItemsArr = new OrderItem[40];
        Order[] ordersArr = new Order[20];
        for (int i = 0; i < 10; i++)
        {
            addNewProduct(productsArr[i].productName, productsArr[i].productCategory, productsArr[i].productPrice, productsArr[i].inStock);
        }
        for (int i = 0; i < 20; i++)
        {
            addNewOrder(ordersArr[i].costumerName, ordersArr[i].mail, ordersArr[i].address, ordersArr[i].shippingDate, ordersArr[i].arrivleDate);
        }
        for (int i = 0; i < 40; i++)
        {
            addNewOrderItem(orderItemsArr[i].itemId, orderItemsArr[i].orderId, orderItemsArr[i].price, orderItemsArr[i].amount);
        }
    }
    internal static class Config
    {
        static internal int _productIndex=0 ;
        static internal int _orderIndex=0;
        static internal int _orderItemIndex=0;
        static private int _orderID = 100000;
        static private int _productID = 200000;
       
        public static int ProductID
        {
            get { return _productID++; }
        }
        public static int OrderID
        {
            get { return _orderID++; }
        }
    
    }
}
