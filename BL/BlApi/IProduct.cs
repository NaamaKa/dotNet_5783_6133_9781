﻿using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface IProduct
{
    //  עבור מנהל ועבור קטלוג ראשי
    public IEnumerable<ProductForList?> GetListOfProduct();

    public int GetnextidFromDO();
    //עבור מנהל
    public BO.Product? GetProductItem(int id);

    //קטלוג קונה
    public BO.ProductItem? GetProductItemForCatalog(int id, BO.Cart CostumerCart);
    public IEnumerable<BO.ProductForList?> GetProductForListByCategory(string? myCategory);
    public BO.ProductForList? GetProductForList(int id);
    public IEnumerable<BO.ProductItem?> GetProductItemList(Func<DO.Product?, bool>? predict = null);

    
    //עבור מנהל
    public void AddProduct(BO.Product p);
    public void UpdateProduct(BO.Product item);
    public void DeleteProduct(int id);
}


