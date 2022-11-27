using System;
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
        foreach (var item in _myCart.Items)
        {
            if (item.ProductId == _id)
            {
                if (GetProduct(_id).InStock >= item.Amount + 1)
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
        BO.Product _product = new BO.Product();

        try
        {
            _product = GetProduct(_id);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        //check if product is inStock
        if (_product.InStock >= 1)
        {
            BO.OrderItem _myNeworderItem = new BO.OrderItem();
            _myNeworderItem.ProductId = _id;
            _myNeworderItem.Name = _product.Name;
            _myNeworderItem.Price = _product.Price;
            _myNeworderItem.Amount = 1;
            _myNeworderItem.TotalPrice = _product.Price;
            _myCart.Items.Add(_myNeworderItem);
            _myCart.Price += _product.Price;
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
            BO.Product _tempProduct = new BO.Product();
            foreach (var item in _myCart.Items)
            {
                try
                {
                    _tempProduct = GetProduct(item.ProductId);
                }
                catch (Exception ex)
                {
                    //product dosnt aggsist
                    throw ex;
                }
                if (item.Amount < 0)
                    throw new Exception("amount of items is illegal");
                if (_tempProduct.InStock < item.Amount)
                    throw new Exception("prodoct " + _tempProduct.Name + " not enough in stock. only enough for " + _tempProduct.InStock);
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
                itemId = item.ProductId,
                amount = item.Amount,
                price = item.TotalPrice,
            });
            DO.Product _tempProduct = new DO.Product();
            try
            {
                _tempProduct = GetProduct(item.ProductId);
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
            if (item.ProductId == _id)
            {
                if (_newAmount == 0)
                {
                    _myCart.Items.Remove(item);
                }
                else if (item.Amount > _newAmount)
                {
                    if (GetProduct(_id).InStock >= item.Amount + _newAmount)
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
