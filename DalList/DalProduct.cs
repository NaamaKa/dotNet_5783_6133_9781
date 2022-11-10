
using DO;

namespace Dal;

public class DalProduct
{
    public int AddNewProduct(Product _newProduct)
    {
        _newProduct.barkode = DataSource.Config.ProductID;
        DataSource.products[DataSource.Config._productIndex] = _newProduct;
        DataSource.Config._productIndex++;
        return _newProduct.barkode;
    }
    public Product GetProduct(int _myBarcode)
    {
        for(int i=0;i< DataSource.Config._productIndex; i++)
        {
            if (DataSource.products[i].barkode==_myBarcode)
                return DataSource.products[i];
        }
        throw new Exception("product not found");
    }
    public Product[] GetProduct()
    {
       Product[] tempList= new Product[DataSource.Config._productIndex];
        for (int i=0;i< DataSource.Config._productIndex; i++)
        {
            tempList[i]= DataSource.products[i];
        }
        return tempList;
    }
    public void DeleteProduct(int _myBarcode)
    {
        for (int i = 0; i < DataSource.Config._productIndex; i++)
        {
            if (DataSource.products[i].barkode == _myBarcode)
            {
                DataSource.products[i] = DataSource.products[DataSource.Config._productIndex];
                DataSource.Config._productIndex--;
                break;
            }
        }
        throw new Exception("product not found");
    }
    public void UpdateProduct(Product _newProduct)
    {
        for (int i = 0; i < DataSource.Config._productIndex; i++)
        {
            if (DataSource.products[i].barkode == _newProduct.barkode)
            {
                DataSource.products[i] = _newProduct;
                break;
            }
        }
    }
}
