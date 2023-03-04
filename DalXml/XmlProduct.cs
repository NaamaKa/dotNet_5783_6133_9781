using DalApi;
using DalXml;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal;

public class XmlProduct : IProduct
{
    readonly string ProductPath = @"Product.xml";

    #region methods

    /// <summary>
    /// add a new product to the list of products
    /// </summary>
    /// <param name="_p">a product</param>
    /// <returns>int of the id of the product</returns>
    /// <exception cref="Exception">product exists</exception>
    public int Add(Product _p)
    {
        List<Product?> ListProduct = XMLTools.LoadListFromXMLSerializer<Product?>(ProductPath);
        if (ListProduct.FirstOrDefault(e => e!.Value.productName == _p.productName && e.Value.productPrice == _p.productPrice && e.Value.productCategory == _p.productCategory && e.Value.inStock == _p.inStock) is not null)
        {
            throw new ItemAlreadyExistsException("product exists, can not add") { ItemAlreadyExists = _p.ToString() };
        }
        _p.barkode = XmlConfig.getProductId();
        ListProduct.Add(_p);
        XMLTools.SaveListToXMLSerializer(ListProduct, ProductPath);
        return _p.barkode;
    }


    /// <summary>
    /// check if the product demanded exist and return it or an exception if not
    /// </summary>
    /// <param name="_num">the id of the product demanded</param>
    /// <returns>details of the product demanded</returns>
    /// <exception cref="Exception">product not exists</exception>
    public Product Get(Func<Product?, bool>? predict)
    {
        List<Product> ListProduct = XMLTools.LoadListFromXMLSerializer<Product>(ProductPath);
        if (ListProduct == null)
        {
            throw new RequestedItemNotFoundException("order not exists,can not get") { RequestedItemNotFound = null };
        }
        if (predict == null)
        {
            throw new GetPredictNullException("the predict is empty") { GetPredictNull = null };
        }
        try
        {
            DO.Product? product = ListProduct.Find(p => predict(p));
            return (Product)product;
        }
        catch
        {
            throw new RequestedItemNotFoundException("product not exists,can not do get") { RequestedItemNotFound = predict.ToString() };
        }
    }


    /// <summary>
    /// cope the products to a new arrey and return it
    /// </summary>
    /// <returns>arrey with all the products</returns>
    public IEnumerable<Product?> GetAll(Func<Product?, bool>? predict = null)
    {
        List<Product?> ListProduct = XMLTools.LoadListFromXMLSerializer<Product?>(ProductPath);


        if (ListProduct == null)
        {
            throw new RequestedItemNotFoundException("product not exists,can not get") { RequestedItemNotFound = "jjj".ToString() };
        }
        if (predict == null)
        {
            return (IEnumerable<Product?>)ListProduct;

        }
        else
        {
            //List<Product?> _products = new List<Product?>();
            //_products=DataSource._Products.FindAll(e=> predict(e)); 
            //return _products;
            try
            {
                IEnumerable<Product?> product = ListProduct.FindAll(p => predict(p));
                return (IEnumerable<Product?>)product;
            }
            catch
            {
                throw new RequestedItemNotFoundException("products not exists,can not get") { RequestedItemNotFound = "jjj".ToString() };
            }
        }
    }

    /// <summary>
    /// check if the product demanded exist and delete it or throw an exception if not
    /// </summary>
    /// <param name="_num">id of product to delete</param>
    /// <exception cref="Exception">product not exists, can not delete</exception>
    public void Delete(int _num)
    {
        List<DO.Product?> ListProduct = XMLTools.LoadListFromXMLSerializer<DO.Product?>(ProductPath);
        if (ListProduct == null)
        {
            throw new RequestedItemNotFoundException("product not exists,can not get") { RequestedItemNotFound = _num.ToString() };
        }
        DO.Product? pre = ListProduct.Find(p => p!.Value.barkode == _num);
        try
        {
            if (pre != null)
            {
                ListProduct.Remove(pre);
                XMLTools.SaveListToXMLSerializer(ListProduct, ProductPath);

            }
        }
        catch
        {
            throw new RequestedItemNotFoundException("product not exists,can not do delete") { RequestedItemNotFound = _num.ToString() };
        }


    }

    /// <summary>
    /// update data of product and throw exception if it does not exist
    /// </summary>
    /// <param name="_p"> id of product demanded to change</param>
    /// <exception cref="Exception">product not exists, can not update</exception>
    public void Update(Product _p)
    {
        if (_p.productName == null || _p.productCategory == null)
        {
            return;

        }

        List<DO.Product?> ListProduct = XMLTools.LoadListFromXMLSerializer<DO.Product?>(ProductPath);
        if (ListProduct is null)
            throw new RequestedItemNotFoundException("product not exists,can not get") { RequestedItemNotFound = _p.ToString() };
        DO.Product? pro = ListProduct.Find(p => p!.Value.barkode == _p.barkode);
        try
        {
            if (pro != null)
            {
                ListProduct.Remove(pro);
                ListProduct.Add(_p); //no nee to Clone()
                XMLTools.SaveListToXMLSerializer(ListProduct, ProductPath);

            }
        }
        catch
        {
            throw new RequestedItemNotFoundException("product not exists,can not do update") { RequestedItemNotFound = _p.ToString() };
        }




        #endregion

    }

    public int GetNextId()
    {
        throw new NotImplementedException();
    }
}

