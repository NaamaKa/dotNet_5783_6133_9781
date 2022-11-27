using DO;
using DalApi;
namespace Dal;
using static Dal.DataSource;
using static Dal.DataSource.Config;


internal class DalProduct : IProduct
{
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
            if (product.barkode == _myBarcode)
            {
                return product;
            }
        }
        throw new Exception("product not found");
    }
    /// <summary>
    /// gets all products and puts them in array
    /// </summary>
    /// <returns>returns array</returns>
    public List<Product> GetAll()
    {

        List<Product> tempProducts = new List<Product>();
        foreach (var product in products)
        {
            tempProducts.Add(product);
        }
        return tempProducts;
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
            if (product.barkode == _myBarcode)
            {
                products.Remove(product);
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
            if (product.barkode == _newProduct.barkode)
            {
                products.Remove(product);
                products.Add(_newProduct);
                break;
            }
        }

    }
}