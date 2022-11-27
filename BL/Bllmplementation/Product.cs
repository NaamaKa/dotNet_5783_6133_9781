using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
namespace Bllmplementation;
using DalApi;
using System.Data.Common;

internal class Product:BlApi.IProduct
{
    private IDal Dal = new Dal.DalList();
    public List<BO.ProductForList> GetProducts()
    {
        List<BO.ProductForList> products = new List<BO.ProductForList>();

        List<DO.Product> product1=new List<DO.Product>();
        product1=Dal.product.GetAll();
        int count=0;
        foreach(BO.ProductForList product in products)
        {
            product.Id = product1[count].barkode;
            product.Name = product1[count].productName;
            //product.Category = product1[count].productCategory;
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
            throw new Exception("id not ligal");
        }
    }
    public BO.ProductForList GetProductForList(int id)
    {
        if (id > 0)
        {
            BO.ProductForList newProduct = new BO.ProductForList();
            try
            {
            List <DO.Product> productList=new List<DO.Product>();
            productList= Dal.product.GetAll();
            
                newProduct.Price = productList[id].productPrice;
                newProduct.Id = productList[id].barkode;
                newProduct.Name = productList[id].productName;
                //newProduct.Category = productList[id].productCategory;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return newProduct;
        }
        else
        {
            throw new Exception("id not ligal");
        }
    }
    public void AddProduct(int id,string name,float price,int amount)
    {
        if (id > 0&&name!="" && price >0 && amount >=0)
        {
            try
            {
                DO.Product product = new DO.Product();
                product.barkode = id;
                product.productName = name;
                product.productPrice = price;
                Dal.product.Add(product);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    public void DeleteProduct(int id)
    {
        List<DO.OrderItem> orderList=new List<DO.OrderItem>();
        orderList=Dal.orderItem.GetAll();
        int count=0;
        foreach(DO.OrderItem order in orderList)
        {
            if (order.id != id)
                count++;
        }
        if (count == orderList.Count())
        {
            try
            {
                Dal.orderItem.Delete(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    public void UpdateProduct(DO.Product productToUpdate)
    {
        if (productToUpdate.barkode > 0  && productToUpdate.productPrice > 0 && productToUpdate.productName != ""&& productToUpdate.inStock>=0)
        {
            try
            {
                Dal.product.Update(productToUpdate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }


}
