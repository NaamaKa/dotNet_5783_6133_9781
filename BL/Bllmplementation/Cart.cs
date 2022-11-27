using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BlApi;
using Dal;
using DalApi;

namespace Bllmplementation;

internal class Cart : ICart
{
    private IDal Dal = new Dal.DalList();
    public BO.Cart AddItemToCart(BO.Cart _myCart, int _id)
    {
        DO.Product _wantedProduct= Dal.product.Get(_id);
        
        foreach (var item in _myCart.Items)
        {
            if (item.ID == _id)
            {
                if (_wantedProduct .inStock>= item.Amount + 1)
                {
                    item.Amount++;
                    double pricetoAdd = item.Price;
                    item.TotalPrice += pricetoAdd;
                    _myCart.Price += pricetoAdd;
                    return _myCart;
                }
                else
                {
                    throw new Exception("not enough in stock");
                }
            }
        }
        #region product not in cart
        //check if product aggsists
        DO.Product _product = new DO.Product();

        try
        {
            _product = Dal.product.Get(_id);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        //check if product is inStock
        if (_product.inStock >= 1)
        {
            BO.OrderItem _myNeworderItem = new BO.OrderItem();
            _myNeworderItem.ID = _id;
            _myNeworderItem.Name = _product.productName;
            _myNeworderItem.Price = _product.productPrice;
            _myNeworderItem.Amount = 1;
            _myNeworderItem.TotalPrice = _product.productPrice;
            _myCart.Items.Add(_myNeworderItem);
            _myCart.Price += _product.productPrice;
            return _myCart;
        }
        else
            throw new Exception("not enough in stock");

        #endregion
        throw new NotImplementedException();
    }

    public void SubmitOrder(BO.Cart _myCart, string _cName, string _cEmail, string _cAddress)
    {
        #region check all data
        //check cart
        if (_myCart.Items.Count > 0)//cart isn't empty
        {
            DO.Product _tempProduct = new DO.Product();
            foreach (var item in _myCart.Items)
            {
                try
                {
                    _tempProduct = Dal.product.Get(item.ID);
                }
                catch (Exception ex)
                {
                    //product dosnt aggsist
                    throw ex;
                }
                if (item.Amount < 0)
                    throw new Exception("amount of items is illegal");
                if (_tempProduct.inStock < item.Amount)
                    throw new Exception("prodoct " + _tempProduct.productName + " not enough in stock. only enough for " + _tempProduct.inStock);
            }
        }
        else
        {
            throw new Exception("cart is empty");
        }
        #endregion
        #region check users data
        if (_cName == null)
        {
            throw new Exception("no name entered");
        }
        if (_cAddress == null)
        {
            throw new Exception("no address entered");
        }
        Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        Match match = regex.Match(_cEmail);
        if (!match.Success)
        {
            throw new Exception("email not good");
        }
        #endregion
        #region create new order
        DO.Order _myNewOrder = new DO.Order()
        {
            OrderNum = 0,
            costumerName = _cName,
            mail = _cEmail,
            address = _cAddress,
            OrderDate = DateTime.Now,
            shippingDate = DateTime.MinValue,
            arrivleDate = DateTime.MinValue
        };
        #endregion
        int _newOrderID = 0;
        try
        {
            _newOrderID = Dal.order.Add(_myNewOrder);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        foreach (var item in _myCart.Items)
        {
            Dal.orderItem.Add(new DO.OrderItem()
            {
                id = 0,
                orderId = _newOrderID,
                itemId = item.ID,
                amount = item.Amount,
                price = item.TotalPrice,
            });
            DO.Product _tempProduct = new DO.Product();
            try
            {
                _tempProduct = Dal.product.Get(item.ID);
                _tempProduct.inStock -= item.Amount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            try
            {
                Dal.product.Update(_tempProduct);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //throw new NotImplementedException();
    }

    public BO.Cart UpdateAmountOfItem(BO.Cart _myCart, int _id, int _newAmount)
    {
        foreach (var item in _myCart.Items)
        {
            if (item.ID == _id)
            {
                if (_newAmount == 0)
                {
                    _myCart.Items.Remove(item);
                }
                else if (item.Amount > _newAmount)
                {
                    if (Dal.product.Get(_id).inStock >= item.Amount + _newAmount)
                    {
                        item.Amount = _newAmount;
                        double pricetoAdd = item.Price * item.Amount;
                        item.TotalPrice = pricetoAdd;
                        _myCart.Price += pricetoAdd;
                        return _myCart;
                    }
                    else
                    {
                        throw new Exception("not enough in stock for new amount");
                    }
                }
                else if (item.Amount < _newAmount)
                {
                    item.Amount = _newAmount;
                    double _newPrice = item.TotalPrice - item.Price * _newAmount;
                    item.TotalPrice = item.Price * _newAmount;
                    _myCart.Price -= _newPrice;
                    return _myCart;
                }
                else//amounts are equal
                {
                    return _myCart;
                }
            }
        }
        throw new NotImplementedException();
    }
}
