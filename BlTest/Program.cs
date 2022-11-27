using Dal;
using DalApi;
using BlImplementation;
using BlApi;
using System.Runtime.CompilerServices;
using BO;
using DO;
using Enums = BO.Enums;
using System.Net.NetworkInformation;
using Bllmplementation;

namespace BlTest
{
    public class Program
    {
        private static readonly IBl blVariable = new Bl();
        private static IBl blVariableFromMethod = new Bl();
        static private DO.Product product = new DO.Product();
        static private BO.Product product1 = new BO.Product();

        static private BO.Order order = new BO.Order();
        static private BO.Order orderFromMethod = new BO.Order();
        static private BO.OrderTracking orderTrackingFromMethod = new BO.OrderTracking();
        static private BO.Cart cart = new BO.Cart();
        public static void Main()
        {
            int choice;
            Console.WriteLine("Enter 1 for product, 2 - cart, 3 - order, 0 to exit:");
            int.TryParse(Console.ReadLine(), out choice);


            while (choice != 0)
            {

                switch (choice)
                {
                    case 1://product
                        productMethod();
                        break;

                    case 2://cart
                        cartMethod();
                        break;

                    case 3://order
                        orderMethod();
                        break;
                }
            }
        }

        static void productMethod()
        {
            int choiceForProduct;
            int parse;
            Console.WriteLine("Enter 1 to get all products " +
                "2 - to get a product by id" +
                "3 - to add a product" +
                "4 - to remove a product" +
                "5 - to update a product ");
            double parse2;
            int.TryParse(Console.ReadLine(), out parse);
            choiceForProduct = parse;



            switch (choiceForProduct)
            {

                case 1://get all products
                    IEnumerable<ProductForList> listFromMethod = blVariable.Product.GetListOfProduct();
                    foreach (ProductForList productForList in listFromMethod)
                        Console.WriteLine(productForList);
                    break;

                case 2://get single product by id
                    {
                        Console.WriteLine("Enter an Id of product:");
                        int.TryParse(Console.ReadLine(), out parse);
                        product.barkode = parse;

                        try
                        {
                            Console.WriteLine(blVariable.Product.GetProductItem(product.barkode));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        break;
                    }

                case 3://add
                    {
                        Console.WriteLine("Enter product details:");
                        Console.WriteLine("Name:");
                        product.productName = Console.ReadLine();
                        Console.WriteLine("Price:");
                        int.TryParse(Console.ReadLine(), out parse);
                        product.productPrice = parse;
                        Console.WriteLine("type 0 for category kitchen, 1 - house, 2 - cellular, 3 - hair");
                        int c = int.Parse(Console.ReadLine());

                        switch (c)
                        {
                            case 0:
                                product.productCategory = DO.Enums.Category.Home;
                                break;
                            case 1:
                                product.productCategory = DO.Enums.Category.Textile;
                                break;
                            case 2:
                                product.productCategory = DO.Enums.Category.Kitchen;
                                break;
                            case 3:
                                product.productCategory = DO.Enums.Category.Garden;
                                break;
                            case 4:
                                product.productCategory = DO.Enums.Category.Office;
                                break;
                            case 5:
                                product.productCategory = DO.Enums.Category.Toys;
                                break;
                        }
                        Console.WriteLine("Amount in stock:");
                        int.TryParse(Console.ReadLine(), out parse);
                        product.inStock = parse;
                        try
                        {
                            blVariable.Product.AddProduct(product);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        break;
                    }

                case 4://delete
                    {
                        Console.WriteLine("Enter an Id of product:");
                        int id = int.Parse(Console.ReadLine());
                        try
                        {
                            blVariable.Product.DeleteProduct(id);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        break;
                    }

                case 5://update product
                    {


                        Console.WriteLine("Enter an Id of product:");
                        int Id = int.Parse(Console.ReadLine());
                        try
                        {
                            product1 = blVariable.Product.GetProductItem(Id);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }

                        Console.WriteLine("the product to update is" + product1);

                        Console.WriteLine("Enter the new details of the product:");

                        Console.WriteLine("name:");
                        product1.Name = Console.ReadLine();
                        Console.WriteLine("type 0 for category kitchen, 1 - house, 2 - cellular, 3 - hair");
                        int category = int.Parse(Console.ReadLine());
                        switch (category)
                        {
                            case 0:
                                product1.Category= BO.Enums.Category.Home;
                                break;
                            case 1:
                                product1.Category = BO.Enums.Category.Textile;
                                break;
                            case 2:
                                product1.Category = BO.Enums.Category.Kitchen;
                                break;
                            case 3:
                                product1.Category = BO.Enums.Category.Garden;
                                break;
                            case 4:
                                product1.Category = BO.Enums.Category.Office;
                                break;
                            case 5:
                                product1.Category = BO.Enums.Category.Toys;
                                break;

                        }
                        Console.WriteLine("price:");
                        double.TryParse(Console.ReadLine(), out parse2);
                        product1.Price = parse2;
                        Console.WriteLine("amount:");
                        int.TryParse(Console.ReadLine(), out parse);
                        product.inStock = parse;

                        blVariable.Product.UpdateProduct(product1);

                        break;
                    }

                case 6:
                    {

                        break;
                    }




            }
        }

        static void cartMethod()
        {
            BO.Cart cart = new BO.Cart();
            BO.Product product = new BO.Product();
            int choiceForCart;
            int parse;
            Console.WriteLine("Enter 1 to add product to the cart " +
               "2 - to update amount of product in the cart" +
               "3 - to place the order ");
            int.TryParse(Console.ReadLine(), out parse);
            choiceForCart = parse;
            switch (choiceForCart)
            {
                case 1://add product to cart
                    {
                        Console.WriteLine("enter product id");
                        int.TryParse(Console.ReadLine(), out parse);
                        int productID = parse;
                        Console.WriteLine("enter your name");
                        cart.CostumerName = Console.ReadLine();
                        Console.WriteLine("enter your email");
                        cart.CostumerEmail = Console.ReadLine();
                        Console.WriteLine("enter your address");
                        cart.CostumerAddress= Console.ReadLine();
                        Console.WriteLine("enter product id and amount of items in cart,for finish enter 0");
                        int productId;
                        int amount;
                        int.TryParse(Console.ReadLine(), out parse);
                        productId = parse;
                        int.TryParse(Console.ReadLine(), out parse);
                        amount = parse;
                        while (productID != 0)
                        {
                            BO.OrderItem orderItem = new BO.OrderItem()
                            {
                                //ID=
                                Name = blVariable.Product.GetProductItem(product.ID).Name,//manager
                                ID = productId,
                                Price = blVariable.Product.GetProductItem(product.ID).Price,//manager
                                Amount= amount
                            };

                            cart.Items.ToList().Add(orderItem);
                            Console.WriteLine("enter product id and amount of items in cart,for finish enter 0");
                            int.TryParse(Console.ReadLine(), out parse);
                            productId = parse;
                            int.TryParse(Console.ReadLine(), out parse);
                            amount = parse;
                        }
                        Console.WriteLine(blVariable.Cart.AddItemToCart(cart, productID));
                        break;
                    }
                case 2:
                    {
                        Console.WriteLine("enter product id ,new amount,and your details ");
                        int.TryParse(Console.ReadLine(), out parse);
                        int productID = parse;
                        int.TryParse(Console.ReadLine(), out parse);
                        int newAmount = parse;
                        cart.CostumerName = Console.ReadLine();
                        cart.CostumerEmail = Console.ReadLine();
                        cart.CostumerAddress = Console.ReadLine();
                        Console.WriteLine("enter product id and amount of items in cart,for finish enter 0");

                        int.TryParse(Console.ReadLine(), out parse);
                        int productId = parse;
                        int.TryParse(Console.ReadLine(), out parse);
                        int amount = parse;
                        while (productID != 0)
                        {
                            BO.OrderItem orderItem = new BO.OrderItem()
                            {

                                Name = blVariable.Product.GetProductItem(product.ID).Name,
                                ID = productId,
                                Price = blVariable.Product.GetProductItem(product.ID).Price,
                                Amount = amount
                            };
                            cart.Items.ToList().Add(orderItem);
                            Console.WriteLine("enter product id and amount of items in cart,for finish enter 0");
                            int.TryParse(Console.ReadLine(), out parse);
                            productId = parse;
                            int.TryParse(Console.ReadLine(), out parse);
                            amount = parse;
                        }
                        Console.WriteLine(blVariable.Cart.UpdateAmountOfItem(cart, productID, newAmount));
                        break;
                    }
                case 3:
                    {
                        Console.WriteLine("enter your details ");
                        cart.CostumerName = Console.ReadLine();
                        cart.CostumerEmail= Console.ReadLine();
                        cart.CostumerAddress = Console.ReadLine();
                        Console.WriteLine("enter product id and amount of items in cart,for finish enter 0");

                        int.TryParse(Console.ReadLine(), out parse);
                        int productId = parse;
                        int.TryParse(Console.ReadLine(), out parse);
                        int amount = parse;
                        while (productId != 0)
                        {
                            BO.OrderItem orderItem = new BO.OrderItem()
                            {

                                Name = blVariable.Product.GetProductItem(productId).Name,
                                ID = productId,
                                Price = blVariable.Product.GetProductItem(product.ID).Price,
                                Amount = amount
                            };
                            cart.Items.ToList().Add(orderItem);
                            Console.WriteLine("enter product id and amount of items in cart,for finish enter 0");
                            int.TryParse(Console.ReadLine(), out parse);
                            productId = parse;
                            int.TryParse(Console.ReadLine(), out parse);
                            amount = parse;
                        }
                        //blVariable.Cart.SubmitOrder(cart);
                        break;
                    }

            }

        }

        static void orderMethod()
        {
            int choiceForOrder;
            int parse;
            Console.WriteLine("Enter 1 to get all orders " +
                "2 - to get an order by id" +
                "3 - to update the shipping date of an order" +
                "4 - to update the delivery date of an order" +
                "5 - to track an order " +
                "or 0 to exit:");

            int.TryParse(Console.ReadLine(), out parse);
            choiceForOrder = parse;
            switch (choiceForOrder)
            {
                case 1://get all orders
                    {
                        IEnumerable<OrderForList> listFromMethod = blVariable.Order.GetListOfOrders();
                        foreach (OrderForList orderForList in listFromMethod)
                            Console.WriteLine(orderForList);
                        break;
                    }
                case 2://get a single order by id
                    {
                        Console.WriteLine("Enter an Id of order:");
                        int.TryParse(Console.ReadLine(), out parse);
                        order.ID = parse;

                        try
                        {
                            IEnumerable<BO.OrderItem> listFromMethod = blVariable.Order.GetOrderDetails(order.ID).Items;
                            Console.WriteLine(blVariable.Order.GetOrderDetails(order.ID).ID);
                            Console.WriteLine(blVariable.Order.GetOrderDetails(order.ID).CostumerEmail);
                            Console.WriteLine(blVariable.Order.GetOrderDetails(order.ID).CostumerAddress);
                            Console.WriteLine(blVariable.Order.GetOrderDetails(order.ID).Status);
                            Console.WriteLine(blVariable.Order.GetOrderDetails(order.ID).PaymentDate);
                            Console.WriteLine(blVariable.Order.GetOrderDetails(order.ID).ShippingDate);
                            Console.WriteLine(blVariable.Order.GetOrderDetails(order.ID).DeliveryDate);
                            foreach (BO.OrderItem item in listFromMethod)
                                Console.WriteLine(item);
                            Console.WriteLine(blVariable.Order.GetOrderDetails(order.ID).TotalPrice);
                            Console.WriteLine(blVariable.Order.GetOrderDetails(order.ID));

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        break;
                    }
                case 3://update shipping order
                    {
                        Console.WriteLine("Enter an Id of order:");
                        int.TryParse(Console.ReadLine(), out parse);
                        order.ID = parse;
                        try
                        {
                            orderFromMethod = blVariable.Order.UpdateShipDate(parse);
                            Console.WriteLine($@"you update the shipping date to ");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        break;
                    }
                case 4:// update delivery order
                    {

                        Console.WriteLine("Enter an Id of order:");
                        int.TryParse(Console.ReadLine(), out parse);
                        order.ID = parse;
                        try
                        {
                            orderFromMethod = blVariable.Order.UpdateShipDate(parse);
                            Console.WriteLine($@"you update the delivery date to {orderFromMethod.DeliveryDate}");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        break;
                    }
                case 5://tracking order
                    {

                        Console.WriteLine("Enter an Id of order:");
                        int.TryParse(Console.ReadLine(), out parse);
                        order.ID = parse;
                        try
                        {

                            orderTrackingFromMethod = blVariable.Order.GetOrderTracking(parse);
                            List<OrderTracking.StatusAndDate> listFromMethod = orderTrackingFromMethod.listOfStatus;
                            foreach (var item in listFromMethod)
                            {
                                Console.WriteLine(item);

                            }

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



}