using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BlApi;

public interface ICart
{
    public Cart AddItemToCart(Cart _myCart, int _id);
    public Cart UpdateAmountOfItem(Cart _myCart, int _id, int _newAmount);
    public void SubmitOrder(Cart _myCart, string _cName, string _cEmail, string _Address);
}
