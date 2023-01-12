using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BlApi;
using BO;
using DalApi;

namespace Bllmplementation;

internal class Cart : ICart
{
    DalApi.IDal? Dal = DalApi.Factory.Get();
    /// <summary>
    /// gets a cart and trues to add a wanted product
    /// </summary>
    /// <param name="_myCart">cart to add</param>
    /// <param name="_id">id of wanted product</param>
    /// <returns>the cart after the try</returns>
    /// <exception cref="SendEmptyCartException">the cart was empty</exception>
    /// <exception cref="NotEnoughInStockException">not enough of product in stock</exception>
    /// <exception cref="FieldToGetProductException">product does not exsist</exception>
    /// <exception cref="ProductNotInStockException"></exception>

    public void OpenCart(BO.Cart myCart)
    {

        CheckCorrectData(myCart!.CostumerName, myCart.CostumerAddress, myCart.CostumerEmail);
    }
    public void CheckCorrectData( string name, string address, string Email)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new BO.EmptyNameException("empty name") { EmptyName = name!.ToString() };
        }
        if (string.IsNullOrEmpty(address))
        {
            throw new BO.EmptyAddressException("empty address") { EmptyAddress = name!.ToString() };
        }
        if (string.IsNullOrEmpty(Email))
        {
            throw new BO.EmptyEmailException("empty Email") { EmptyEmail = name!.ToString() };
        }
        return;

    }
    public BO.Cart AddItemToCart(BO.Cart _myCart, OrderItem item)
    {
        if (_myCart == null)
        {
            throw new SendEmptyCartException("try to add to an empty cart") { SendEmptyCart = item.ID.ToString() };
        }
        DO.Product _wantedProduct = Dal!.product.Get(e => e?.barkode == item.ID);
        //var wantedItem=from OrderItem item in _myCart!.Items!
        //               where(item != null && item.ID == _id&& _wantedProduct.inStock >= item.Amount + 1)
        //               select();

        foreach (var itemInCart in _myCart!.Items!)
        {
            if (itemInCart != null)
            {
                if (itemInCart.ID == item.ID)
                {
                    if (_wantedProduct.inStock >= item.Amount + 1)
                    {
                        itemInCart.Amount++;
                        double pricetoAdd = itemInCart.Price;
                        itemInCart.TotalPrice += pricetoAdd;
                        _myCart.Price += pricetoAdd;
                        return _myCart;
                    }
                    else
                    {
                        throw new NotEnoughInStockException("not enoughf in stock") { NotEnoughInStock = item.ID.ToString() };
                    }
                }
            }
            
        }
        #region product not in cart
        //check if product aggsists
        DO.Product _product = new DO.Product();
        try
        {
            _product = Dal.product.Get(e => e?.barkode == item.ID);
        }
        catch (Exception)
        {
            //product dosnt aggsist
            throw new FieldToGetProductException("product dosnt axist") { FieldToGetProduct = item.ID.ToString() };
        }
        //check if product is inStock
        if (_product.inStock >= 1)
        {
            BO.OrderItem _myNeworderItem = new()
            {
                ID = item.ID,
                Name = _product.productName,
                Price = _product.productPrice,
                Amount = 1,
                TotalPrice = _product.productPrice
            };
            _myCart.Items.Add(_myNeworderItem);
            _myCart.Price += _product.productPrice;
            return _myCart;
        }
        else
            throw new ProductNotInStockException("not enough in stock") { ProductNotInStock = item.ID.ToString() };

        #endregion
    }

    public void SubmitOrder(BO.Cart _myCart, string _cName, string _cEmail, string _cAddress)
    {
        #region check all data
        //check cart

        if (_myCart.Items != null)//cart isn't empty
        {
            DO.Product _tempProduct = new();

            foreach (var item in _myCart.Items)
            {
                if (item != null)
                {
                    try
                    {
                        _tempProduct = Dal!.product.Get(e => e?.barkode == item.ID);
                    }
                    catch (Exception)
                    {
                        //product dosnt aggsist
                        throw new Exception("product dosnt axist");
                    }
                    if (item.Amount < 0)
                        throw new Exception("amount of items is illegal");
                    if (_tempProduct.inStock < item.Amount)
                        throw new Exception("prodoct " + _tempProduct.productName + " not enough in stock. only enough for " + _tempProduct.inStock);
                }
                else
                {
                    throw new Exception("no item");
                }
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
            _newOrderID = Dal!.order.Add(_myNewOrder);
        }
        catch (Exception)
        {
            throw new Exception("order dosnt axist");
        }
        foreach (var item in _myCart.Items)
        {
            if (item != null)
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
                    _tempProduct = Dal.product.Get(e => e?.barkode == item.ID);
                    _tempProduct.inStock -= item.Amount;
                }
                catch (Exception)
                {
                    throw new Exception("Error");
                }
                try
                {
                    Dal.product.Update(_tempProduct);
                }
                catch (Exception)
                {
                    throw new Exception("Error");
                }
            }
            else
            {
                throw new Exception("no item");
            }
        }

        //throw new NotImplementedException();
    }

    public BO.Cart UpdateAmountOfItem(BO.Cart _myCart, int _id, int _newAmount)
    {
        if (_myCart.Items != null)
            foreach (var item in _myCart.Items)
            {
                if (item != null)
                {
                    if (item.ID == _id)
                    {
                        if (_newAmount == 0)
                        {
                            _myCart.Items.Remove(item);
                        }
                        else if (item.Amount > _newAmount)
                        {
                            if (Dal!.product.Get(e => e?.barkode == item.ID).inStock >= item.Amount + _newAmount)
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
                    else
                    {
                        throw new Exception("no item");
                    }
                }
            }
        throw new NotImplementedException();
    }

    void ICart.OpenCart(BO.Cart myCart)
    {
        throw new NotImplementedException();
    }

    BO.Cart? ICart.AddItemToCart(BO.Cart _myCart, int _id)
    {
        throw new NotImplementedException();
    }

    BO.Cart? ICart.UpdateAmountOfItem(BO.Cart _myCart, int _id, int _newAmount)
    {
        throw new NotImplementedException();
    }

    void ICart.SubmitOrder(BO.Cart _myCart, string _cName, string _cEmail, string _Address)
    {
        throw new NotImplementedException();
    }
}
