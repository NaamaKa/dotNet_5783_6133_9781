using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface IProduct
{
    //  עבור מנהל ועבור קטלוג ראשי
    public IEnumerable<BO.ProductForList?> GetListOfProduct();

    public int GetnextidFromDO();
    //עבור מנהל
    public BO.Product? GetProductItem(int id);

    //קטלוג קונה
    public BO.ProductItem? GetProductItemForCatalog(int id, BO.Cart CostumerCart);
    public IEnumerable<BO.ProductForList?> GetProductForListByCategory(string? myCategory);

    //עבור מנהל
    public void AddProduct(BO.Product p);
    public void UpdateProduct(BO.Product item);
    public void DeleteProduct(int id);
}


