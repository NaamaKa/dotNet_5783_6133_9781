// See https://aka.ms/new-console-template for more information
using DO;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Data.Common;
namespace DalTest;
using Dal;
using DalApi;

public class Programm
{
    static private Product product = new Product();
    static private Order order = new Order();
    static private OrderItem orderItem = new OrderItem();
    static IDal IDalVariable = new Dal.DalList();



    public static void Main()
    {
        DataSource.startProgram();
        int choice;
        Console.WriteLine("Enter a number 1-3 or 0 to exit:");
        int.TryParse(Console.ReadLine(), out choice);

        while (choice != 0)
        {

            switch (choice)
            {
                case 1://product
                    productMenu();
                    break;

                case 2://order
                    orderMenu();
                    break;

                case 3://order item
                    orderItemMenu();
                    break;
            }
        }
    }

    #region product menu
    static void productMenu()
    {
        Console.WriteLine("Enter your choice " +
  "1-add,   " +
  "2-get one product,  " +
  "3-get all products,   " +
  "4-delete," +
  "5-update:");

        int productChoice;

        int parse;
        double parse2;
        float parse3;
        int.TryParse(Console.ReadLine(), out parse);
        productChoice = parse;

        switch (productChoice)
        {

            case 1://add
                Console.WriteLine("Enter product details:");
                Console.WriteLine("Name:");
                product.productName = Console.ReadLine();
                Console.WriteLine("Price:");
                double.TryParse(Console.ReadLine(), out parse2);
                product.productPrice = parse2;
                Console.WriteLine("type 0 for category Home, 1 - textile, 2 - kitchen, 3 - office, 4-garden, 5-toys");
                int category = int.Parse(Console.ReadLine());
                switch (category)
                {
                    case 0:
                        product.productCategory = (int)(Enums.Category.Home);
                        break;
                    case 1:
                        product.productCategory = (Enums.Category.Textile);
                        break;
                    case 2:
                        product.productCategory = (Enums.Category.Office);
                        break;
                    case 3:
                        product.productCategory = (Enums.Category.Kitchen);
                        break;
                    case 4:
                        product.productCategory = (Enums.Category.Garden);
                        break;
                    case 5:
                        product.productCategory = (Enums.Category.Toys);
                        break;
                }
                Console.WriteLine("Amount in stock:");
                int.TryParse(Console.ReadLine(), out parse);
                product.inStock = parse;
                try
                {
                    IDalVariable.product.Add(product);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                break;
            case 2:
                {
                    Console.WriteLine("Enter an Id of product:");
                    int.TryParse(Console.ReadLine(), out parse);
                    product.barkode = parse;
                    try
                    {
                        Console.WriteLine(IDalVariable.product.Get(product.barkode));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
                break;
            case 3:
                Console.WriteLine(IDalVariable.product.GetAll());
                break;
            case 4:
                Console.WriteLine("Enter an Id of product:");
                int id = int.Parse(Console.ReadLine());
                try
                {
                    IDalVariable.product.Delete(id);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                break;
            case 5://update
                {

                    Console.WriteLine("Enter an Id of product:");
                    int barcode = int.Parse(Console.ReadLine());
                    string name;
                    double price;
                    int amountInStock;

                    try
                    {
                        product = IDalVariable.product.Get(barcode);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    Console.WriteLine("the product to update is" + product);
                    Console.WriteLine("Enter the new details of the product:");
                    Console.WriteLine("name:");
                    name = Console.ReadLine();
                    Console.WriteLine("price:");
                    double.TryParse(Console.ReadLine(), out parse2);
                    price = parse2;
                    Console.WriteLine("category:");
                    Console.WriteLine("type 0 for category Home, 1 - textile, 2 - kitchen, 3 - office, 4-garden, 5-toys");
                    int categoryTemp = int.Parse(Console.ReadLine());
                    switch (categoryTemp)
                    {
                        case 0:
                            product.productCategory = (Enums.Category.Home);
                            break;
                        case 1:
                            product.productCategory = (Enums.Category.Textile);
                            break;
                        case 2:
                            product.productCategory = (Enums.Category.Office);
                            break;
                        case 3:
                            product.productCategory = (Enums.Category.Kitchen);
                            break;
                        case 4:
                            product.productCategory = (Enums.Category.Garden);
                            break;
                        case 5:
                            product.productCategory = (Enums.Category.Toys);
                            break;
                    }
                    Console.WriteLine("amount");
                    int.TryParse(Console.ReadLine(), out parse);
                    amountInStock = parse;
                    Product proTOUpdata = new Product() { barkode = barcode, productCategory = (Enums.Category)categoryTemp, productName = name, productPrice = price, inStock = amountInStock };
                    IDalVariable.product.Add(proTOUpdata);
                    break;
                }
        }
    }
    #endregion

    #region order menu
    static void orderMenu()
    {
        int parse;
        Console.WriteLine("Enter your choice:" +
            "1-add,   " +
            "2-get one product,  " +
            "3-get all products,   " +
            "4-delete," +
            "5-update:");
        int choiceForOrder;
        int.TryParse(Console.ReadLine(), out parse);
        choiceForOrder = parse;

        switch (choiceForOrder)
        {
            case 1://add
                DateTime date;
                Console.WriteLine("Enter order details:");
                Console.WriteLine("name");
                order.costumerName = Console.ReadLine();
                Console.WriteLine("email");
                order.mail = Console.ReadLine();
                Console.WriteLine("address");
                order.address = Console.ReadLine();
                IDalVariable.order.Add(order);
                break;

            case 2:
                Console.WriteLine("Enter an Id of Order:");
                int Id = int.Parse(Console.ReadLine());
                try
                {
                    Console.WriteLine(IDalVariable.order.Get(Id));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                break;

            case 3:
                foreach (Order myOrder in IDalVariable.order.GetAll())
                {
                    Console.WriteLine(myOrder);
                }
                break;

            case 4://delete
                Console.WriteLine("Enter an Id of order:");
                int IdToDelete = int.Parse(Console.ReadLine());
                try
                {
                    IDalVariable.order.Delete(IdToDelete);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                break;

            case 5://update
                Console.WriteLine("Enter an Id of order:");
                int idToUpdate = int.Parse(Console.ReadLine());
                try
                {
                    order = IDalVariable.order.Get(idToUpdate);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                Order orderToUpdate = new Order();
                orderToUpdate.OrderNum = idToUpdate;
                Console.WriteLine("the order to update is" + order);
                Console.WriteLine("Enter the new details of the order:");
                Console.WriteLine("customer name:");
                orderToUpdate.costumerName = Console.ReadLine();
                Console.WriteLine("email:");
                orderToUpdate.mail = Console.ReadLine();
                Console.WriteLine("address:");
                orderToUpdate.address = Console.ReadLine();
                DateTime.TryParse(Console.ReadLine(), out date);
                orderToUpdate.OrderDate = date;
                DateTime.TryParse(Console.ReadLine(), out date);
                orderToUpdate.shippingDate = date;
                DateTime.TryParse(Console.ReadLine(), out date);
                orderToUpdate.arrivleDate = date;
                IDalVariable.order.Update(orderToUpdate);
                break;
        }
    }

    #endregion

    #region order item menu
    static void orderItemMenu()
    {
        Console.WriteLine("Enter your choice:" +
       "1-add,   " +
       "2-get one product item,  " +
       "3-get all product items,   " +
       "4- get all items from spesific order" +
       "5- get all orders with wanted item" +
       "6-delete," +
       "7-update,   "
      );
        int parse;
        double parse3;
        int choiceOrderItem;
        int.TryParse(Console.ReadLine(), out parse);
        choiceOrderItem = parse;
        switch (choiceOrderItem)
        {
            case 1://add
                Console.WriteLine("Enter order item details:");
                Console.WriteLine("Enter order Id:");
                int.TryParse(Console.ReadLine(), out parse);
                orderItem.orderId = parse;
                Console.WriteLine("Enter items barcode");
                int.TryParse(Console.ReadLine(), out parse);
                orderItem.itemId = parse;
                Console.WriteLine("Enter orders price:");
                double.TryParse(Console.ReadLine(), out parse3);
                orderItem.price = parse3;
                Console.WriteLine("Enter items amount:");
                int.TryParse(Console.ReadLine(), out parse);
                orderItem.amount = parse;
                IDalVariable.orderItem.Add(orderItem);
                break;
            case 2://get one  order item
                Console.WriteLine("Enter an Id of order item:");
                int OrderItemId = int.Parse(Console.ReadLine());
                try
                {
                    Console.WriteLine(IDalVariable.orderItem.Get(OrderItemId));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                break;
            case 3://get all order items
                try
                {
                    Console.WriteLine(IDalVariable.orderItem.GetAll());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                break;
            case 4://get all items from spesific order
                Console.WriteLine("Enter an Id of order :");
                int OrderId = int.Parse(Console.ReadLine());
                try
                {
                    Console.WriteLine(IDalVariable.orderItem.GetOrderItemsFromOrder(OrderId));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                break;
            case 5://get all orders with wanted item
                Console.WriteLine("Enter an barcode of item :");
                int itemBarcode = int.Parse(Console.ReadLine());
                try
                {
                    //Console.WriteLine(IDalVariable.orderItem.GetOrdersOforderItems(itemBarcode));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                break;
            case 6://delete
                Console.WriteLine("Enter an Id order item");
                int OrderItemIdToDelete = int.Parse(Console.ReadLine());
                try
                {
                    IDalVariable.orderItem.Delete(OrderItemIdToDelete);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                break;
            case 7://update
                Console.WriteLine("Enter an Id order item");
                int OrderItemIdToUpdate = int.Parse(Console.ReadLine());

                try
                {
                    orderItem = IDalVariable.orderItem.Get(OrderItemIdToUpdate);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                Console.WriteLine("the order item to update is" + orderItem);
                Console.WriteLine("Enter the new details of the order item:");
                Console.WriteLine("orderId:");
                int orderId = int.Parse(Console.ReadLine());
                orderItem.orderId = orderId;
                Console.WriteLine("productId:");
                int productId = int.Parse(Console.ReadLine());
                orderItem.itemId = productId;
                Console.WriteLine("price:");
                int pricePerUnit = int.Parse(Console.ReadLine());
                orderItem.price = pricePerUnit;
                Console.WriteLine("amount");
                int quantity = int.Parse(Console.ReadLine());
                orderItem.amount = quantity;
                IDalVariable.orderItem.Update(orderItem);
                break;
        }
    }
}
#endregion