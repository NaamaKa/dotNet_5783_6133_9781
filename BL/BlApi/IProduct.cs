using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface IProduct
{
    //  הבור מנהל ועבור קטלוג ראשי
    public IEnumerable<BO.ProductForList>? GetListOfProduct();


    //עבור מנהל 
    public BO.Product? GetProductItem(int id);

    //קטלוג קונה
    public BO.ProductItem? GetProductItemForCatalog(int id, BO.Cart CostumerCart);

    //עבור מנהל
    public void AddProduct(DO.Product p);
    public void UpdateProduct(BO.Product item);
    public void DeleteProduct(int id);
}
