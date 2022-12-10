using DO;
using DalApi;
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
            throw new RequestedItemNotFoundException("order not exists,can not get") { RequestedItemNotFound = "jjj".ToString() };
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
        int temp = Config.ProductID;
        int temp2 = Config.DicreaseProductId;
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
        foreach (var product in products)
        {
            if (product != null)
            {
                if (product?.barkode == _myBarcode)
                {
                    return (Product)product;
                }
            }
        }
        throw new Exception("product not found");
    }
    /// <summary>
    /// gets all products and puts them in array
    /// </summary>
    /// <returns>returns array</returns>
    public List<Product?> GetAll()
    {
        List<Product?> tempProducts = new List<Product?>();
        foreach (var product in products)
        {
            if (product != null)
                tempProducts.Add(product);
        }
        if (tempProducts.Count > 0)
            return tempProducts;
        throw new Exception("no products found");
    }
    /// <summary>
    /// deletsa spesific product
    /// </summary>
    /// <param name="_myBarcode">product wated to be deleted</param>
    /// <exception cref="Exception"></exception>
    public void Delete(int _myBarcode)
    {
        foreach (var product in products)
        {
            if (product != null)
            {
                if (product?.barkode == _myBarcode)
                {
                    products.Remove(product);
                }
            }
        }
        throw new Exception("product not found");
    }
    /// <summary>
    /// updates a spesific product
    /// </summary>
    /// <param name="_newProduct">product to be replaced insead</param>
    public void Update(Product _newProduct)
    {
        foreach (var product in products)
        {
            if (product != null)
            {
                if (product?.barkode == _newProduct.barkode)
                {
                    products.Remove(product);
                    products.Add(_newProduct);
                    break;
                }
            }
        }
    }

}