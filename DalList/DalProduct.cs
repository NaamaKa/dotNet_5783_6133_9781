
using DO;

namespace Dal;
using static Dal.DataSource;
using static Dal.DataSource.Config;


public class DalProduct
{
    /// <summary>
    /// gets a prodect and adds it to array of products
    /// </summary>
    /// <param name="_newProduct">product for putting in</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public int AddNewProduct(Product _newProduct)
    {
        if (_productIndex < 50)
        {
            _newProduct.barkode =ProductID;
            products[_productIndex] = _newProduct;
           _productIndex++;
            return _newProduct.barkode;
        }
        throw new Exception("no place for more products");
    }
    /// <summary>
    /// returns a spesific product 
    /// </summary>
    /// <param name="_myBarcode">barcode of broduct wanted</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public Product GetProduct(int _myBarcode)
    {
        for(int i=0;i< _productIndex; i++)
        {
            if (products[i].barkode==_myBarcode)
                return products[i];
        }
        throw new Exception("product not found");
    }
    /// <summary>
    /// gets all products and puts them in array
    /// </summary>
    /// <returns>returns array</returns>
    public Product[] GetProduct()
    {
       Product[] tempList= new Product[_productIndex];
        for (int i=0;i< _productIndex; i++)
        {
            tempList[i]=products[i];
        }
        return tempList;
    }
    /// <summary>
    /// deletsa spesific product
    /// </summary>
    /// <param name="_myBarcode">product wated to be deleted</param>
    /// <exception cref="Exception"></exception>
    public void DeleteProduct(int _myBarcode)
    {
        for (int i = 0; i < _productIndex; i++)
        {
            if (products[i].barkode == _myBarcode)
            {
                if (i== _productIndex-1)//wanted found in last place in product arrey
                {
                    _productIndex--;
                    return;
                }
                else//found in middle-coppeis last product in array to temp and coppeis wntwd space to temp 
                {
                    Product tempProduct = products[_productIndex-1];
                    Console.WriteLine(_productIndex);
                    products[i] = tempProduct;
                    _productIndex--;
                    return ;
                }
            }
        }
        throw new Exception("product not found");
    }
    /// <summary>
    /// updates a spesific product
    /// </summary>
    /// <param name="_newProduct">product to be replaced insead</param>
    public void UpdateProduct(Product _newProduct)
    {
        for (int i = 0; i < _productIndex; i++)
        {
            if (products[i].barkode == _newProduct.barkode)
            {
                products[i] = _newProduct;
                break;
            }
        }
    }
}
