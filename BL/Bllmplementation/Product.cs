using BlApi;
using BO;
using DalApi;
using DO;
using System;
using System.Diagnostics;
using System.Xml.Linq;
namespace BlImplementation;


public class Product : BlApi.IProduct
{

    private readonly IDal Dal = new Dal.DalList();


    #region Methodes
    public IEnumerable<ProductForList?> GetProductForListByCategory(string? myCategory)
    {
        IEnumerable<DO.Product?> productsList = new List<DO.Product?>();
        List<BO.ProductForList> productsForList = new List<BO.ProductForList>();
        productsList = Dal.product.GetAll();
        foreach (var product in productsList)
        {
            if (product != null)
            {
                if ((product?.productCategory).ToString() == myCategory)
                    productsForList.Add(new BO.ProductForList()
                    {
                        Id = product!.Value.barkode,
                        Name = product.Value.productName,
                        Price = product.Value.productPrice,
                        Category = (BO.Enums.Category)product!.Value.productCategory!
                    });
            }
        }
        return productsForList;

        throw new NotImplementedException();
    }
    public IEnumerable<BO.ProductForList> GetListOfProduct()
    {
        IEnumerable<DO.Product?> productsList = new List<DO.Product?>();
        List<BO.ProductForList> productsForList = new List<BO.ProductForList>();
        productsList = Dal.product.GetAll();
        foreach (var item in productsList)
        {
            if (item != null)
                productsForList.Add(new BO.ProductForList()
                {
                    Id = item.Value.barkode,
                    Name = item.Value.productName,
                    Price = item.Value.productPrice,
                    Category = (BO.Enums.Category)item!.Value.productCategory!
                });

        }
        return productsForList;
    }


    //עבור מנהל
    public BO.Product GetProductItem(int id)
    {
        if (id <= 0)
        {
            throw new BO.NegativeIdException("negative id") { NegativeId = id.ToString() };
        }
        else
        {
            DO.Product p = new DO.Product();
            try
            {
                p = Dal.product.Get(id);
            }
            catch
            {
                throw new BO.NegativeIdException("negative id") { NegativeId = id.ToString() };

            }
            BO.Product p1 = new BO.Product();

            p1 = DOToBO(p);
            return p1;

        }
    }

    //קטלוג קונה
    public BO.ProductItem GetProductItemForCatalog(int id, BO.Cart CostumerCart)
    {
        if (id <= 0)
        {
            throw new BO.NegativeIdException("negative id") { NegativeId = id.ToString() };
        }
        else
        {
            DO.Product p = new DO.Product();
            try
            {
                p = Dal.product.Get(id);
            }
            catch
            {
                throw new BO.NegativeIdException("negative id") { NegativeId = id.ToString() };

            }
            BO.ProductItem PI = new BO.ProductItem()
            {
                Id = p.barkode,
                Name = p.productName,
                Category = (BO.Enums.Category)p!.productCategory!,
                Price = p.productPrice,
                InStock = true,
                Amount = CostumerCart.Items.FindAll(e => e!.ID == id).Count(),

            };
            return PI;
        }
    }

    //עבור מנהל
    public void AddProduct(BO.Product p)
    {

        CheckCorectData(p.ID, p.Name!, p.Category, p.Price, p.InStock);
        try
        {
            Dal.product.Add(newProductWithData(p.ID, p.Name!, p.Category, p.Price, p.InStock));
        }
        catch (DO.ItemAlreadyExistsException)
        {
            throw new BO.ProductAlreadyExistsException("product already exists") { ProductAlreadyExists = p.ToString() };

        }
    }

    public void UpdateProduct(BO.Product item)
    {

        CheckCorectData(item.ID, item.Name!, item.Category, item.Price, item.InStock);
        try
        {
            Dal.product.Update(newProductWithData(item.ID, item!.Name!, item.Category, item.Price, item.InStock));
        }
        catch (DO.RequestedItemNotFoundException)
        {
            throw new BO.ProductNotExistsException("product not exists") { ProductNotExists = item.ToString() };

        }

    }

    public void DeleteProduct(int id)
    {
        IEnumerable<DO.OrderItem?> orderList = new List<DO.OrderItem?>();
        orderList = Dal.orderItem.GetAll();
        bool flag = false;
        foreach (var OI in orderList)
        {
            if (OI != null)
            {
                if (OI?.orderId == id)
                {
                    flag = true;
                }
            }
        }
        if (flag)
        {
            throw new BO.ProductInUseException("product in use") { ProductInUse = id.ToString() };
        }
        try
        {
            Dal.product.Delete(id);
        }
        catch
        {
            throw new BO.ProductNotExistsException("product not exists") { ProductNotExists = id.ToString() };
        }

    }





    #endregion
    #region help methodes

    #region DO to BO
    private BO.Product DOToBO(DO.Product p)
    {
        BO.Product p1 = new BO.Product()
        {
            ID = p.barkode,
            Name = p.productName,
            Price = p.productPrice,
            Category = (BO.Enums.Category)p!.productCategory!,
            InStock = p.inStock
        };
        return p1;
    }
    #endregion

    #region check corect data
    public void CheckCorectData(int id, string name, BO.Enums.Category category, double price, int inStock)
    {
        if (id < 0)
        {
            throw new BO.NegativeIdException("negative id") { NegativeId = id.ToString() };
        }
        if (string.IsNullOrEmpty(name))
        {
            throw new BO.EmptyNameException("empty name") { EmptyName = name!.ToString() };
        }
        if (price <= 0)
        {
            throw new BO.NegativePriceException("Nagative price") { NegativePrice = price.ToString() };
        }
        if (inStock < 0)
        {
            throw new BO.NegativeStockException("Nagative inStock") { NegativeStock = inStock.ToString() };
        }
        return;

    }
    #endregion

    #region new product with data

    private static DO.Product newProductWithData(int id, string name, BO.Enums.Category category, double price, int inStock)
    {
        DO.Product p = new()
        {
            barkode = id,
            productName = name,
            productCategory = (DO.Enums.Category)category,
            productPrice = price,
            inStock = inStock
        };
        return p;
    }
    public int GetnextidFromDO()
    {
        int id = Dal.product.GetNextId();
        return id;
    }


    #endregion
    #endregion




}










