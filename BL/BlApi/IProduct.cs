using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface IProduct
{
    public List<ProductForList> GetProducts();
    public BO.Product GetProduct(int id);
    public ProductForList GetProductForList(int id);
    public void AddProduct(int id,string name,float price,int amount);
    public void DeleteProduct(int id);
    public void UpdateProduct(Product productToUpdate);
}
