using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
namespace Bllmplementation;
using DalApi;
using System.Data.Common;

internal class Product:IProduct
{
    private IDal Dal = new Dal.DalList();
    public List<BO.ProductForList> GetProducts()
    {
        List<BO.ProductForList> products = new List<BO.ProductForList>();

        List<DO.Product> product1=Dal.product.getAll();
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
     public BO.Product GetProduct(int id)
    {
        
        if (id > 0)
        {
            DO.Product newProduct = new DO.Product();
            try
            {
            newProduct = Dal.product.Get(id);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            BO.Product productToReturn=new BO.Product();
            productToReturn.ID = newProduct.barkode;
            productToReturn.ID = newProduct.barkode;
            productToReturn.ID = newProduct.barkode;
            productToReturn.ID = newProduct.barkode;
            return productToReturn;

        }
        else {
            throw new Exception('id not ligal');
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
        if (id > 0&&name>0 && price >0 && amount >=0)
        {
            try
            {
                Dal.product.Add(id, name, price, amount);
            }
            catch
            {
                throw Exception;
            }
        }
    }
    public void DeleteProduct(int id)
    {
        List<DO.OrderItem> orderList=Dal.orderItem.GetAll();
        int count=0;
        foreach(Order order in orderList)
        {
            if (order.id != id)
                count++;
        }
        if (count == orderList.Count())
        {
            try
            {
                Dal.orderItem.Dalete(id);
            }
            catch 
            {
                throw Exception;
            }
        }
    }
    public void UpdateProduct(Product productToUpdate)
    {
        if (id > 0 && productToUpdate.name > 0 && productToUpdate.price > 0 && productToUpdate.amount >= 0)
        {
            try
            {
                Dal.product.Update(productToUpdate);
            }
            catch
            {
                throw Exception;
            }
        }
    }
}
