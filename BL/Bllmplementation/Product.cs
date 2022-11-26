using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
namespace Bllmplementation;
using Dal;
using System.Data.Common;

internal class Product:IProduct
{
     public List<BO.ProductForList> GetProducts()
    {
        List<BO.ProductForList> products=new List<BO.ProductForList>;
        List <BO.product> product1=DalProduct.getAll();
        int count=0;
        foreach(Product product in products)
        {
            product.Id = product1[count].barkode;
            product.Name = product1[count].productName;
            product.Category = product1[count].productCategory;
            product.Price = product1[count].productPrice;
            count++;
        }
        return products;
    }
     public Product GetProductFor(int id)
    {
        if (id > 0)
        {
            try
            {
            List <Do.product> productList=DalProduct.getAll();
            Product newProduct = new Product(productList[id].Barkode,productList[id].ProductName,productList[id].ProductCategory,productList[id].ProductPrice,productList[id].ProductInStock);
            }
            catch
            {
                throw Exception;
            }
            return newProduct;
        }
    }
    public ProductForList GetProductForList(int id)
    {
        if (id > 0)
        {
            try
            {
            List <Do.product> productList=DalProduct.getAll();
            ProductForList newProduct = new ProductForList(productList[id].Barkode,productList[id].ProductName,productList[id].ProductPrice,productList[id].ProductCategory);
            }
            catch
            {
                throw Exception;
            }
            return newProduct;
        }
    }
    public void AddProduct(int id,string name,float price,int amount)
    {
        if (id > 0,name>0,price>0,amount>=0)
        {
            try
            {
                DataSource.addNewProduct(id,name,price,amount)
            }
            catch
            {
                throw Exception;
            }
        }
    }
    public void DeleteProduct(int id)
    {
        List<OrderItem> orderList=DalOrderItem.GetAll();
        int count=0;
        foreach(Order order in orderList)
        {

        }
    }
}
