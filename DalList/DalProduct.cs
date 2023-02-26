using DO;
using DalApi;
using System.Security.Cryptography;
namespace Dal;
using static Dal.DataSource;
using static Dal.DataSource.Config;


internal class DalProduct : IProduct
{
    /// <summary>
    /// check if the product demanded exist and return it or an exception if not
    /// </summary>
    /// <param name="_num">the id of the product demanded</param>
    /// <returns>details of the product demanded</returns>
    /// <exception cref="Exception">product not exists</exception>
    public Product Get(Func<Product?, bool>? predict)
    {
        if (products == null)
        {
            throw new RequestedItemNotFoundException("order not exists,can not get") { RequestedItemNotFound = predict!.ToString() };
        }
        if (predict == null)
        {
            throw new GetPredictNullException("the predict is empty") { GetPredictNull = null };
        }
        Product? _newProduct = products.Find(e => predict(e));
        if (_newProduct.HasValue)
            return (Product)_newProduct;
        else
            throw new RequestedItemNotFoundException("product not exists,can not do get") { RequestedItemNotFound = predict.ToString() };
    }

    /// <summary>
    /// cope the products to a new arrey and return it
    /// </summary>
    /// <returns>arrey with all the products</returns>
    public IEnumerable<Product?> GetAll(Func<Product?, bool>? predict = null)
    {
        if (products == null)
        {
            throw new RequestedItemNotFoundException("order not exists,cannot get") { RequestedItemNotFound = "jjj".ToString() };
        }
        if (predict == null)
        {
            return products;

        }
        else
        {
            List<Product?> _products = new List<Product?>();
            _products = products.FindAll(e => predict(e));
            return _products;
        }
    }
    public int GetNextId()
    {
        int temp = ProductID;
        int temp2 = DicreaseProductId;
        return temp;
    }
    /// <summary>
    /// gets a prodect and adds it to array of products
    /// </summary>
    /// <param name="_newProduct">product for putting in</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public int Add(Product _newProduct)
    {
        if (products.Count() < 50)
        {
            _newProduct.barkode = ProductID;
            products.Add(_newProduct);
        }
        else
        {
            throw new Exception("no place for more products");
        }
        return _newProduct.barkode;
    }
    /// <summary>
    /// returns a spesific product
    /// </summary>
    /// <param name="_myBarcode">barcode of broduct wanted</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public Product Get(int _myBarcode)
    {
        try
        {
            return (from Product product in products
                    where (product.Equals(true) && product.barkode == _myBarcode)
                    select product).First();
        }
        catch
        {
            throw new RequestedProductNotFoundException("product not exist") { RequestedProductNotFound = _myBarcode.ToString() };

        }

    }
    /// <summary>
    /// gets all products and puts them in array
    /// </summary>
    /// <returns>returns array</returns>
    public List<Product?> GetAll()
    {
        List<Product?> tempProducts = new List<Product?>();
        try
        {
            return (from Product? product in products
                    where product.Equals(true)
                    select product).ToList();
        }
        catch
        {
            throw new RequestedProductNotFoundException("orderItem not exist") { };
        }
    }
    /// <summary>
    /// deletsa spesific product
    /// </summary>
    /// <param name="_myBarcode">product wated to be deleted</param>
    /// <exception cref="Exception"></exception>
    public void Delete(int _myBarcode)
    {
        try
        {
            products.Remove(products
               .Where(p => p is not null && p.Value.barkode == _myBarcode)
               .Select(p => p).FirstOrDefault());
        }
        catch
        {
            throw new RequestedProductNotFoundException("product not exist") { RequestedProductNotFound = _myBarcode.ToString() };

        }

    }
    /// <summary>
    /// updates a spesific product
    /// </summary>
    /// <param name="_newProduct">product to be replaced insead</param>
    public void Update(Product _newProduct)
    {
        //    try
        //    {
        //        products.Remove(products
        //            .Where(p => p is not null && p.Value.barkode == _newProduct.barkode)
        //            .Select(p => p).FirstOrDefault());
        //        products.Add(_newProduct);
        //    }
        //    catch
        //    {
        //        throw new RequestedProductNotFoundException("product not exist") { RequestedProductNotFound = _newProduct.barkode.ToString() };

        //    }

        //}
        if (_newProduct.productName == null || _newProduct.productCategory == null)
        {
            return;

        }

        //if (DataSource._Products == null) throw new RequestedItemNotFoundException("order not exists,can not get") { RequestedItemNotFound = _p.ToString() };
        ////Product? _productToUpdate = new Product();
        ////_productToUpdate = DataSource._Products.Find(e => e.HasValue && e!.Value.ID == _p.ID);
        try
        {
            DataSource.products.Remove(DataSource.products
               .Where(e => e is not null && e.Value.barkode == _newProduct.barkode)
               .Select(e => (Product?)e!).First());
            DataSource.products.Add(_newProduct);
        }

        catch
        {
            throw new RequestedItemNotFoundException("product not exists,can not do update") { RequestedItemNotFound = _newProduct.ToString() };
        }

    }
}