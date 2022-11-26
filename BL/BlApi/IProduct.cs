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
    public Product GetProductForProduct(int id);
    public ProductForList GetProductForList(int id);
    public void AddProduct(int id,string name,float price,int amount);
    public void DeleteProduct(int id);
}
