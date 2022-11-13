﻿// See https://aka.ms/new-console-template for more information
using DO;
using System.Linq.Expressions;
//using DalList;
using Dal;

using System.Net.Http.Headers;
using System.Data.Common;

namespace DalTest
{
    public class Programm
    {
        static private Product product = new Product();
        static private Order order = new Order();
        static private OrderItem orderItem = new OrderItem();

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



        static void productMenu()
        {

            Console.WriteLine("Enter your choice " +
                "1-add,   " +
                "2-get one product,  " +
                "3-get all products,   " +
                "4-delete," +
                "5-update:");
            int productChoice;
            DalProduct p = new Dal.DalProduct();
            int parse;
            double parse2;
            float parse3;
            int.TryParse(Console.ReadLine(), out parse);
            productChoice = parse;

            while (productChoice != 0)
            {

                switch (productChoice)
                {

                    case 1://add
                        Console.WriteLine("Enter product details:");
                        Console.WriteLine("Name:");
                        product.productName = Console.ReadLine();
                        Console.WriteLine("Price:");
                        int.TryParse(Console.ReadLine(), out parse);
                        product.productPrice = parse;
                        Console.WriteLine("type 0 for category Home, 1 - textile, 2 - kitchen, 3 - office, 4-garden, 5-toys");
                        int category = int.Parse(Console.ReadLine());
                        switch (category)
                        {
                            case 0:
                                product.productCategory =(int)( Enums.Category.Home);
                                break;
                            case 1:
                                product.productCategory =(int)( Enums.Category.Textile);
                                break;
                            case 2:
                                product.productCategory = (int)(Enums.Category.Office);
                                break;
                            case 3:
                                product.productCategory = (int)(Enums.Category.Kitchen);
                                break;
                            case 4:
                                product.productCategory = (int)(Enums.Category.Garden);
                                break;
                            case 5:
                                product.productCategory = (int)(Enums.Category.Toys);
                                break;
                        }
                        Console.WriteLine("Amount in stock:");
                        int.TryParse(Console.ReadLine(), out parse);
                        product.inStock = parse;
                        try
                        {
                            p.AddNewProduct(product);
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
                            product.barkode= parse;
                            try
                            {
                                Console.WriteLine(p.GetProduct(product.barkode));
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }
                            break;
                        }
                    case 3:
                        foreach (Product myProduct in p.GetProduct())
                        {
                            Console.WriteLine(myProduct);
                        }
                        break;
                    case 4:
                        Console.WriteLine("Enter an Id of product:");
                        int id = int.Parse(Console.ReadLine());
                        try
                        {
                            p.DeleteProduct(id);
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
                            float price;
                            int amountInStock;

                            try
                            {
                                product = p.GetProduct(barcode);
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
                            float.TryParse(Console.ReadLine(), out parse3);
                            price = parse3;
                            Console.WriteLine("amount");
                            int.TryParse(Console.ReadLine(), out parse);
                            amountInStock = parse;
                            Product proTOUpdata = new Product() { barkode = barcode, productName = name, productPrice = price, inStock = amountInStock };
                            p.UpdateProduct(proTOUpdata);
                            break;
                        }
                }

            }
        }
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

            DalOrder o = new DalOrder();
            switch (choiceForOrder)
            {
                case 1://add
                    DateTime date;
                    Console.WriteLine("Enter order details:");
                    // int.TryParse(Console.ReadLine(), out parse);
                    order.costumerName= Console.ReadLine();
                    order.mail = Console.ReadLine();
                    order.address= Console.ReadLine();
                    //DateTime.TryParse(Console.ReadLine(), out date);
                    //order._orderDate = date;
                    //DateTime.TryParse(Console.ReadLine(), out date);
                    //order._shippingDate = date;
                    //DateTime.TryParse(Console.ReadLine(), out date);
                    //order._deliveryDate = date;
                    o.AddNewOrder(order);
                    break;

                case 2:
                    Console.WriteLine("Enter an Id of Order:");
                    int Id = int.Parse(Console.ReadLine());
                    try
                    {
                        Console.WriteLine(o.GetOrder(Id));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    break;

                case 3:
                    foreach (Order myOrder in o.GetOrder())
                    {
                        Console.WriteLine(myOrder);
                    }
                    break;

                case 4://delete
                    Console.WriteLine("Enter an Id of order:");
                    int IdToDelete = int.Parse(Console.ReadLine());
                    try
                    {
                        o.DeleteOrder(IdToDelete);
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
                        order = o.GetOrder(idToUpdate);
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
                    orderToUpdate.shippingDate= date;
                    DateTime.TryParse(Console.ReadLine(), out date);
                    orderToUpdate.arrivleDate = date;
                    o.UpdateOrder(orderToUpdate);
                    break;
            }
        }


        static void orderItemMenu()
        {
            Console.WriteLine("Enter your choice:" +
           "1-add,   " +
           "2-get one product,  " +
           "3-get all products,   " +
           "4-delete," +
           "5-update,   " +
           "6-see al items in order:");
            int parse;
            float parse3;
            int choiceOrderItem;
            int.TryParse(Console.ReadLine(), out parse);
            choiceOrderItem = parse;
            DalOrderItem OI = new DalOrderItem();
            switch (choiceOrderItem)
            {
                case 1://add
                    Console.WriteLine("Enter order item details:");
                    int.TryParse(Console.ReadLine(), out parse);
                    orderItem.orderId= parse;
                    int.TryParse(Console.ReadLine(), out parse);
                    orderItem.itemId= parse;
                    float.TryParse(Console.ReadLine(), out parse3);
                    orderItem.price = parse3;
                    int.TryParse(Console.ReadLine(), out parse);
                    orderItem.amount= parse;
                    OI.AddNewOrderItem(orderItem);
                    break;
                case 2:
                    Console.WriteLine("Enter an Id of the order of the order item:");
                    int OrderId = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter an Id of the product of the order item:");
                    int ProductId = int.Parse(Console.ReadLine());
                    try
                    {
                        Console.WriteLine(OI.GetOrderItem(ProductId, OrderId));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    break;
                case 3:
                    foreach (OrderItem myOrderItem in OI.GetOrderItem())
                    {
                        Console.WriteLine(myOrderItem);//אולי צריך את toString??
                    }
                    break;
                case 4://delete
                    Console.WriteLine("Enter an Id of the product of the order item:");
                    int ProductIdToDelete = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter an Id of the order of the order item:");
                    int OrderIdToDelete = int.Parse(Console.ReadLine());
                    try
                    {
                        OI.DeleteOrderItem(ProductIdToDelete, OrderIdToDelete);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    break;
                case 5://update
                    Console.WriteLine("Enter an Id of the order of the order item:");
                    int OrderIdToUpdate = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter an Id of the product of the order item:");
                    int ProductIdTouodate = int.Parse(Console.ReadLine());
                    try
                    {
                        orderItem = OI.GetOrderItem(ProductIdTouodate, OrderIdToUpdate);
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
                    orderItem.price= pricePerUnit;
                    Console.WriteLine("amount");
                    int quantity = int.Parse(Console.ReadLine());
                    orderItem.amount = quantity;
                    OI.UpdateOrderItem(orderItem);
                    break;

                case 6:
                    Console.WriteLine("Enter Id of an order:");
                    int IdOrder = int.Parse(Console.ReadLine());
                    try
                    {
                        //foreach (OrderItem myOrderItem in OI.GetOrderItem(IdOrder))
                        //{
                        //    if (myOrderItem.OrderID != 0)
                        //        Console.WriteLine(myOrderItem);
                        //}
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    break;

            }
        }




    }
}