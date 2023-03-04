using DalApi;
using DO;
using System;
using System.Diagnostics;
using System.Net;
using System.Reflection.PortableExecutable;
using static DO.Enums;

namespace Dal;

public static class DataSource
{

    static DataSource()
    {
        s_Initialize();
    }

    static readonly Random rnd = new Random();
    internal static List<Product?> products = new List<Product?>();
    internal static List<OrderItem?> orderItems = new List<OrderItem?>();
    internal static List<Order?> orders = new List<Order?>();
    static public void startProgram()
    {
        return;
    }
    static private void addNewProduct(string PName, Enums.Category PCategory, double PPrice, int PInStock)
    {
        Product p = new Product() { barkode = Config.ProductID, productName = PName, productCategory = PCategory, productPrice = PPrice, inStock = PInStock };
        products.Add(p);
    }

    static private void addNewOrder(string newCustomerName, string newCustomerEmail, string newCustomerAdress, DateTime NewOrderDate, DateTime? newShipDate, DateTime? newDeliveryDate)
    {
        Order o = new Order() { OrderNum = Config.OrderID, OrderDate = NewOrderDate, costumerName = newCustomerName, mail = newCustomerEmail, address = newCustomerAdress ,shippingDate=newShipDate,arrivleDate=newDeliveryDate};
        orders.Add(o);
    }
    //static private void addNewOrder(string newCustomerName, string newCustomerEmail, string newCustomerAdress)
    //{
    //    DateTime _today = DateTime.Now;
    //    int daysAgo = new Random().Next(600);
    //    DateTime NewOrderDate = _today.AddDays(-daysAgo);
    //    int daysbetweenOrderToShip = new Random().Next(10);
    //    DateTime newShipDate = NewOrderDate.AddDays(daysbetweenOrderToShip);
    //    int daysbetweenDeliveryToShip = new Random().Next(7);
    //    DateTime newDeliveryDate = newShipDate.AddDays(daysbetweenDeliveryToShip);
    //    addNewOrder(newCustomerName, newCustomerEmail, newCustomerAdress, NewOrderDate, newShipDate, newDeliveryDate);
    //}
    static private void addNewOrderItem(int _id, int OrderId, int OrderItemId, double Oprice, int Oamount)
    {
        OrderItem oi = new OrderItem() { id = _id, itemId = OrderItemId, orderId = OrderId, price = Oprice, amount = Oamount };
        orderItems.Add(oi);
    }
    static private void s_Initialize()  ///
    {
        #region new products
        addNewProduct("fuzzy carpet", (Category.Home), 12.5, 38);//100000
        addNewProduct("Wardrobe", (Category.Home), 659, 53);     //100001
        addNewProduct("Fabric sofa", (Category.Home), 4999, 35);//100002
        addNewProduct("Leather sofa", (Category.Home), 7075, 20);//100003
        addNewProduct("single bed", (Category.Home), 1300, 45);  //100004
        addNewProduct("Double bed", (Category.Home), 3000, 50);   //100005
        addNewProduct("living room table", (Category.Home), 2900, 35);//100006
        addNewProduct("Soft bedding", (Category.Textile), 69.9, 100);//100007
        addNewProduct("6 face towels", (Category.Textile), 35.9, 108);//100008
        addNewProduct("6 glass cups", (Category.Kitchen), 12.5, 100);//100009
        addNewProduct("6 glass plate", (Category.Kitchen), 25, 110);//100010
        addNewProduct("office chair", (Category.Office), 350, 82);//100011
        addNewProduct("Computer desk", (Category.Office), 1020, 25);//100012
        addNewProduct("Garden dining area", (Category.Garden), 2499, 43);//100013
        addNewProduct("Storage bench", (Category.Garden), 350, 30);//100014
        addNewProduct("playing surface", (Category.Toys), 79.9, 50);//100015
        addNewProduct("Dinosaur doll", (Category.Toys), 19.9, 100);//100016
        addNewProduct("Cutting Board", (Category.Kitchen), 19, 60);//100017
        addNewProduct("2 serving plates", (Category.Kitchen), 30, 15);//100018
        addNewProduct("forks", (Category.Kitchen), 22, 100);//100019
        addNewProduct("12 markers", (Category.Toys), 19.9, 0);//100020



        #endregion
        #region new order item

        addNewOrderItem(Config.IdOrderItem, 100000, 500001, 25, 2);
        addNewOrderItem(Config.IdOrderItem, 100002, 500001, 4999, 1);
        addNewOrderItem(Config.IdOrderItem, 100008, 500001, 35.9, 1);
        addNewOrderItem(Config.IdOrderItem, 100011, 500001, 700, 2);

        addNewOrderItem(Config.IdOrderItem, 100000, 500002, 12.5, 1);
        addNewOrderItem(Config.IdOrderItem, 100001, 500002, 1318, 2);
        addNewOrderItem(Config.IdOrderItem, 100002, 500002, 14, 3);
        addNewOrderItem(Config.IdOrderItem, 100004, 500002, 2600, 2);

        addNewOrderItem(Config.IdOrderItem, 100008, 500003, 71.8, 2);
        addNewOrderItem(Config.IdOrderItem, 100004, 500003, 2600, 2);

        addNewOrderItem(Config.IdOrderItem, 100008, 500004, 71.8, 2);

        addNewOrderItem(Config.IdOrderItem, 100011, 500005, 700, 2);

        addNewOrderItem(Config.IdOrderItem, 100002, 500006, 4999, 1);

        addNewOrderItem(Config.IdOrderItem, 100009, 500007, 50, 4);
        addNewOrderItem(Config.IdOrderItem, 100002, 500007, 4999, 1);

        addNewOrderItem(Config.IdOrderItem, 100001, 500008, 1318, 2);
        addNewOrderItem(Config.IdOrderItem, 100015, 500008, 79.9, 1);

        addNewOrderItem(Config.IdOrderItem, 100016, 500009, 59.7, 3);
        addNewOrderItem(Config.IdOrderItem, 100013, 500009, 2499, 1);
        addNewOrderItem(Config.IdOrderItem, 100001, 500009, 659, 1);

        addNewOrderItem(Config.IdOrderItem, 100009, 500010, 62.5, 5);

        addNewOrderItem(Config.IdOrderItem, 100010, 500011, 50, 2);
        addNewOrderItem(Config.IdOrderItem, 100011, 500011, 700, 2);


        addNewOrderItem(Config.IdOrderItem, 100009, 500012, 62.5, 5);
        addNewOrderItem(Config.IdOrderItem, 100010, 500012, 50, 2);

        addNewOrderItem(Config.IdOrderItem, 100009, 500013, 50, 4);
        addNewOrderItem(Config.IdOrderItem, 100011, 500013, 700, 2);

        addNewOrderItem(Config.IdOrderItem, 100008, 500014, 71.8, 2);
        addNewOrderItem(Config.IdOrderItem, 100002, 500014, 4999, 1);

        addNewOrderItem(Config.IdOrderItem, 100000, 500015, 12.5, 1);
        addNewOrderItem(Config.IdOrderItem, 100004, 500015, 2600, 2);

        addNewOrderItem(Config.IdOrderItem, 100008, 500016, 35.9, 1);
        addNewOrderItem(Config.IdOrderItem, 100011, 500016, 700, 2);

        addNewOrderItem(Config.IdOrderItem, 100001, 500017, 1318, 2);
        addNewOrderItem(Config.IdOrderItem, 100004, 500017, 2600, 2);

        addNewOrderItem(Config.IdOrderItem, 100009, 500018, 62.5, 5);
        addNewOrderItem(Config.IdOrderItem, 100011, 500018, 700, 2);

        addNewOrderItem(Config.IdOrderItem, 100013, 500019, 2499, 1);
        addNewOrderItem(Config.IdOrderItem, 100001, 500019, 1318, 2);

        addNewOrderItem(Config.IdOrderItem, 100002, 500020, 4999, 1);
        #endregion
        #region new order

        Random rnd = new Random();
        addNewOrder("David Levi", "david@gmail.com", "buksboim 12", DateTime.Now.AddDays(-(rnd.Next(10))), DateTime.Now.AddDays(-(rnd.Next(5))), DateTime.Now.AddDays(-(rnd.Next(3))));
        addNewOrder("Shimon Cohen", "Shimon@gmail.com", "shaulzon 9", DateTime.Now.AddDays(-(rnd.Next(20))), DateTime.Now.AddDays(-(rnd.Next(15))), null);
        addNewOrder("Chaim Uzan", "Chaim@gmail.com", "Ruzin 10", DateTime.Now.AddDays(-(rnd.Next(8))), null,null);
        addNewOrder("Menachem Safra", "Menachem@gmail.com", "Parnas 8", DateTime.Now.AddDays(-(rnd.Next(5))), DateTime.Now.AddDays(-(rnd.Next(3))),null);
        addNewOrder("Mati Pollack", "Mati@gmail.com", "Zerach 9", DateTime.Now.AddDays(-(rnd.Next(13))), null,null);
        addNewOrder("Reuven Blau", "Reuven@gmail.com", "Bar Ilan 56", DateTime.Now.AddDays(-(rnd.Next(14))), DateTime.Now.AddDays(-(rnd.Next(10))), DateTime.Now.AddDays(-(rnd.Next(5))));
        addNewOrder("Levi Nachmon", "Levi@gmail.com", "yerushalaim 23", DateTime.Now.AddDays(-(rnd.Next(9))), DateTime.Now.AddDays(-(rnd.Next(5))), null);
        addNewOrder("Yehuda Eliovich", "Yehuda@gmail.com", "rabi Akiva 12", DateTime.Now.AddDays(-(rnd.Next(13))), DateTime.Now.AddDays(-(rnd.Next(11))), DateTime.Now.AddDays(-(rnd.Next(5))));
        addNewOrder("Zvulun Chason", "Zvulun@gmail.com", "agasi 17", DateTime.Now.AddDays(-(rnd.Next(10))),null,null);
        addNewOrder("Dan Sason", "Dan@gmail.com", "vital 6", DateTime.Now.AddDays(-(rnd.Next(9))), DateTime.Now.AddDays(-(rnd.Next(7))), null);
        addNewOrder("Naftali Hersh", "Naftali@gmail.com", "Ptachia 9", DateTime.Now.AddDays(-(rnd.Next(8))), DateTime.Now.AddDays(-(rnd.Next(7))), DateTime.Now.AddDays(-(rnd.Next(3))));
        addNewOrder("Gadi Cohen", "Gadi@gmail.com", "yarden 54",DateTime.Now.AddDays(-(rnd.Next(10))), null,null);
        addNewOrder("Asher Fredz", "Asher@gmail.com", "Menashe 56", DateTime.Now.AddDays(-(rnd.Next(18))), DateTime.Now.AddDays(-(rnd.Next(13))), null);
        addNewOrder("Yossi Naiov", "Yossi@gmail.com", "Goldknopf 70", DateTime.Now.AddDays(-(rnd.Next(14))), DateTime.Now.AddDays(-(rnd.Next(9))), DateTime.Now.AddDays(-(rnd.Next(3))));
        addNewOrder("Benny Fried", "Benny@gmail.com", "buksboim 19",DateTime.Now.AddDays(-(rnd.Next(2))),null,null);
        addNewOrder("Avi Merry", "Avi@gmail.com", "Tarmish 87", DateTime.Now.AddDays(-(rnd.Next(3))), null,null);
        addNewOrder("Itzik Zer", "Itzik@gmail.com", "vital 14", DateTime.Now.AddDays(-(rnd.Next(6))), DateTime.Now.AddDays(-(rnd.Next(2))), null);
        addNewOrder("Kobi Green", "Kobi@gmail.com", "Pisga 12", DateTime.Now.AddDays(-(rnd.Next(17))), DateTime.Now.AddDays(-(rnd.Next(10))), DateTime.Now.AddDays(-(rnd.Next(3))));
        addNewOrder("Moshe Chaim", "Moshe@gmail.com", "Torah 67",DateTime.Now.AddDays(-(rnd.Next(21))), DateTime.Now.AddDays(-(rnd.Next(12))),null);
        addNewOrder("Aharon Rabi", "Aharon@gmail.com", "Baal Hatania 24", DateTime.Now.AddDays(-(rnd.Next(16))), DateTime.Now.AddDays(-(rnd.Next(5))), null);

        #endregion
    }


    internal static class Config
    {
        static private int _orderID = 100000;
        static private int _productID = 500000;
        static private int _idOrderItem = 200000;
        public static int IdOrderItem
        {
            get { return _idOrderItem++; }
        }
        public static int ProductID
        {
            get { return _productID++; }
        }
        public static int OrderID
        {
            get { return _orderID++; }
        }
        public static int DicreaseProductId
        {
            get { return _productID--; }
        }
    }

}