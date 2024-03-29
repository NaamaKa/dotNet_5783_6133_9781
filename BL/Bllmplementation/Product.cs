﻿using BlApi;
using BO;
using DalApi;
using DO;
using System;
using System.Diagnostics;
using System.Xml.Linq;
namespace BlImplementation;


public class Product : BlApi.IProduct
{
    DalApi.IDal? Dal = DalApi.Factory.Get();
    #region Methodes
    /// <summary>
    ///
    /// </summary>
    /// <param name="myCategory"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public IEnumerable<ProductForList?> GetProductForListByCategory(string? myCategory)
    {
        IEnumerable<DO.Product?> productsList = new List<DO.Product?>();
        productsList = Dal!.product.GetAll();
        return productsList 
            .Where(product => product != null && (product?.productCategory).ToString() == myCategory)
            .Select(product => new ProductForList()
            {
                Id = product!.Value.barkode,
                Name = product.Value.productName,
                Price = product.Value.productPrice,
                Category = (BO.Enums.Category)product!.Value.productCategory!
            });
        throw new NotImplementedException();
    }
    public BO.ProductForList? GetProductForList(int id)
    {
        IEnumerable<DO.Product?> productsList = new List<DO.Product?>();
        if (Dal != null)
        {
            productsList = Dal.product.GetAll();
        }
        return productsList
         .Where(item => (item != null) && (item.Value.barkode == id))
         .Select(item => new BO.ProductForList()
         {
             Id = item!.Value.barkode,
             Name = item.Value.productName,
             Price = item.Value.productPrice,
             Category = (BO.Enums.Category)item.Value.productCategory
         }).FirstOrDefault();

    }
    public IEnumerable<BO.ProductItem?> GetProductItemList(Func<DO.Product?, bool>? predict = null)
    {
        IEnumerable<DO.Product?> productsList = new List<DO.Product?>();
        IEnumerable<DO.OrderItem?> orderItemList = new List<DO.OrderItem?>();
        if (Dal != null)
        {
            productsList = Dal.product.GetAll();
            orderItemList = Dal.orderItem.GetAll();
        }
        if (predict == null)
            return productsList
                .Where(product => product is not null && product.Value.productCategory is not null)
                .Select(p => new BO.ProductItem()
                {
                    Id = p.Value.barkode,
                    Name = p.Value.productName,
                    Category = (BO.Enums.Category)p.Value.productCategory,
                    Price = p.Value.productPrice,
                    InStock = p.Value.inStock > 0 ? true : false,
                    //AmoutInYourCart = CostumerCart.ItemList.FindAll(e => e?.ID == id).Count()
                    Amount = ((from item in orderItemList
                                        group item by item?.id into mygroup
                                        where mygroup.Key == p.Value.barkode
                                        select (mygroup.Count())).FirstOrDefault())
                }).ToList();
        else
            return productsList
           .Where(product => product is not null && product.Value.productCategory is not null && (predict(product)))
           .Select(p => new BO.ProductItem()
           {
               Id = p!.Value.barkode,
               Name = p.Value.productName,
               Category = (BO.Enums.Category)p.Value.barkode,
               Price = p.Value.productPrice,
               InStock = p.Value.inStock > 0 ? true : false,
               Amount = (from item in orderItemList
                                  group item by item?.id into mygroup
                                  where mygroup.Key == p.Value.barkode
                                  select (mygroup.Count())).FirstOrDefault()


           }).ToList();
    }
    public IEnumerable<ProductForList> GetListOfProduct()
    {
        IEnumerable<DO.Product?> productsList = new List<DO.Product?>();
        productsList = Dal!.product.GetAll();
        return productsList
            .Where(item => item != null)
            .Select(item => new ProductForList()
            {
                Id = item!.Value.barkode,
                Name = item.Value.productName,
                Price = item.Value.productPrice,
                Category = (BO.Enums.Category)item!.Value.productCategory!
            });
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
                p = Dal!.product.Get(e => e?.barkode == id);
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
                p = Dal!.product.Get(e => e?.barkode == id);
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
                Amount = CostumerCart!.Items!.FindAll(e => e!.ID == id).Count(),

            };
            return PI;
        }
    }

    //עבור מנהל
    public void AddProduct(BO.Product p)
    {

        CheckCorrectData(p.ID, p.Name!, p.Category, p.Price, p.InStock);
        try
        {
            Dal!.product.Add(newProductWithData(p.ID, p.Name!, p.Category, p.Price, p.InStock));
        }
        catch
        {
            throw new BO.ProductAlreadyExistsException("product already exists") { ProductAlreadyExists = p.ToString() };

        }
    }

    public void UpdateProduct(BO.Product item)
    {

        //CheckCorrectData(item.ID, item.Name!, item.Category, item.Price, item.InStock);
        //try
        //{
        //    Dal!.product.Update(newProductWithData(item.ID, item!.Name!, item.Category, item.Price, item.InStock));
        //}
        //catch (DO.RequestedItemNotFoundException)
        //{
        //    throw new BO.ProductNotExistsException("product not exists") { ProductNotExists = item.ToString() };

        //}
        CheckCorrectData(item.ID, item.Name!, item.Category, item.Price, item.InStock);
        try
        {
            if ( item.Name is null)
                throw new BO.GetEmptyCateporyException("the product category is empty")
                {
                    GetEmptyCatepory = null
                };
            if (Dal != null)
            {
                Dal.product.Update(newProductWithData(item.ID, item.Name, (BO.Enums.Category)item.Category, item.Price, item.InStock));
            }
        }
        catch (DO.RequestedItemNotFoundException)
        {
            throw new BO.ProductNotExistsException("product not exists") { ProductNotExists = item.ToString() };

        }
    }

    public void DeleteProduct(int id)
    {
        IEnumerable<DO.OrderItem?> orderList = new List<DO.OrderItem?>();
        orderList = Dal!.orderItem.GetAll();
        bool flag = false;
        var updateFlag = orderList
            .Where(OI => OI != null && OI?.orderId == id)
            .Select(OI => flag = true);
        if (flag)
        {
            throw new ProductInUseException("product in use") { ProductInUse = id.ToString() };
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

    #region check correct data
    public void CheckCorrectData(int id, string name, BO.Enums.Category category, double price, int inStock)
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
        int id = Dal!.product.GetNextId();
        return id;
    }


    #endregion
    #endregion




}










