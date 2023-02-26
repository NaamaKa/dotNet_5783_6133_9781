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

        CheckCorrectData(myCart!.CostumerName!, myCart.CostumerAddress!, myCart.CostumerEmail!);
    }
    /// <summary>
    /// checks if all data from user is ok and sends mtching exeptions
    /// </summary>
    /// <param name="name"></param>
    /// <param name="address"></param>
    /// <param name="Email"></param>
    /// <exception cref="BO.EmptyNameException"></exception>
    /// <exception cref="BO.EmptyAddressException"></exception>
    /// <exception cref="BO.EmptyEmailException"></exception>
    public void CheckCorrectData( string? name, string? address, string? Email)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new BO.EmptyNameException("empty name") { EmptyName = name };
        }
        if (string.IsNullOrEmpty(address))
        {
            throw new BO.EmptyAddressException("empty address") { EmptyAddress = address };
        }
        if (string.IsNullOrEmpty(Email))
        {
            throw new BO.EmptyEmailException("empty Email") { EmptyEmail = Email };
        }
        return;

    }
    /// <summary>
    /// gets a cart and wanted item and adds it  to cart
    /// </summary>
    /// <param name="_myCart"></param>
    /// <param name="item"></param>
    /// <returns></returns>
    /// <exception cref="SendEmptyCartException"></exception>
    /// <exception cref="NotEnoughInStockException"></exception>
    /// <exception cref="FieldToGetProductException"></exception>
    /// <exception cref="ProductNotInStockException"></exception>
    public BO.Cart AddItemToCart(BO.Cart _myCart, int itemId,OrderItem item)
    {
        if (_myCart == null)
        {
            throw new BO.SendEmptyCartException("try to add to an empty cart") { SendEmptyCart = itemId.ToString() };
        }
        else
        {
            if (_myCart.Items == null)
                _myCart.Items = new List<OrderItem?>();
                /*throw new ItemNotInCartException("item list not exsist") { ItemNotInCart = _myCart.ToString() };*/
            var exist = _myCart.Items
                .Where(e => e?.ID == itemId)
                .Select(e => (BO.OrderItem?)e).FirstOrDefault();
            if (exist is not null)
            {
                //BO.OrderItem BOI = cart.ItemList.Find(e => e?.ID == itemId) ?? new BO.OrderItem();
                var BOI = _myCart.Items
                    .Where(e => e?.ID == itemId)
                    .Select(e => (BO.OrderItem?)e!).FirstOrDefault();
                if (BOI != null)
                {
                    DO.Product DP;
                    if (Dal != null)
                    {
                        DP = Dal.product.Get(e => e.Value.barkode == itemId);
                    }
                    else
                    {
                        throw new BO.GetDulNullException("product not exists") { GetDulNull = itemId.ToString() };
                    }

                    if (BOI.Amount < DP.inStock)
                    {
                        BOI.Amount++;
                        BOI.TotalPrice += BOI.Price;
                        _myCart.Price += BOI.Price;
                        return _myCart;
                    }
                    else
                    {
                        throw new BO.NotEnoughInStockException("Not enough in stock") { NotEnoughInStock = itemId.ToString() };
                    }
                }
                else
                {
                    return _myCart;
                }
            }
            else
            {
                try
                {
                    DO.Product DP;
                    try
                    {
                        DP = Dal.product.Get(e => e.Value.barkode == itemId);
                    }
                    catch
                    {
                        throw new BO.GetDulNullException("product not exists") { GetDulNull = itemId.ToString() };
                    }
                    if (DP.inStock > 0)
                    {
                        _myCart.Items.Add(new BO.OrderItem()
                        {
                            NumInOrder = _myCart.Items.Count + 1,
                            ID = DP.barkode,
                            Name = DP.productName,
                            Price = DP.productPrice,
                            Amount = item.Amount,
                            TotalPrice = DP.productPrice*item.Amount

                        });
                        _myCart.Price += DP.productPrice;
                        return _myCart;
                    }
                    else
                    {
                        throw new BO.ProductNotInStockException("product not in stock") { ProductNotInStock = itemId.ToString() };
                    }

                }
                catch (DO.RequestedItemNotFoundException)
                {
                    throw new BO.ProductNotExistsException("product not exists") { ProductNotExists = itemId.ToString() };
                }
            }

        }
        //if (_myCart == null)
        //{
        //    throw new SendEmptyCartException("try to add to an empty cart") { SendEmptyCart = itemId.ToString() };
        //}
        //DO.Product _wantedProduct = Dal!.product.Get(e => e?.barkode == itemId);

        //if (_myCart.Items != null)
        //{
        //    var item1 = _myCart.Items
        //                .Where(e => e?.ID == itemId)
        //                .Select(e => (BO.OrderItem?)e!).FirstOrDefault();
        //    foreach (var itemInCart in _myCart!.Items!)
        //    {
        //        if (itemInCart != null)
        //        {
        //            if (itemInCart.ID == itemId)
        //            {
        //                if (_wantedProduct.inStock >= item1.Amount + 1)
        //                {
        //                    itemInCart.Amount += item1.Amount;
        //                    double pricetoAdd = itemInCart.Price;
        //                    itemInCart.TotalPrice += pricetoAdd;
        //                    _myCart.Price += pricetoAdd;
        //                    _wantedProduct.inStock -= item1.Amount;
        //                    Dal.product.Update(_wantedProduct);
        //                    return _myCart;
        //                }
        //                else
        //                {
        //                    throw new NotEnoughInStockException("not enoughf in stock") { NotEnoughInStock = itemId.ToString() };
        //                }
        //            }
        //        }

        //    }
        //}
        //#region product not in cart
        ////check if product aggsists
        //DO.Product _product = new DO.Product();
        //try
        //{
        //    _product = Dal.product.Get(e => e?.barkode == itemId);
        //}
        //catch (Exception)
        //{
        //    //product dosnt aggsist
        //    throw new FieldToGetProductException("product dosnt axist") { FieldToGetProduct = itemId.ToString() };
        //}
        ////check if product is inStock


        //if (_product.inStock >= item.Amount)
        //{
        //    BO.OrderItem _myNeworderItem = new()
        //    {
        //        ID = item.ID,
        //        Name = _product.productName,
        //        Price = _product.productPrice,
        //        Amount = item.Amount,
        //        TotalPrice = _product.productPrice,
        //        NumInOrder = item.NumInOrder
        //    };
        //    if (_myCart!.Items == null)
        //        _myCart.Items = new List<BO.OrderItem>();
        //    _myCart.Items.Add(_myNeworderItem);
        //    _myCart.Price += _product.productPrice;
        //    _product.inStock -= item.Amount;
        //    Dal.product.Update(_product);
        //    return _myCart;
        //}
        //else
        //    throw new ProductNotInStockException("not enough in stock") { ProductNotInStock = item.ID.ToString() };

        //#endregion
    }
    /// <summary>
    /// gets all order and deels with the submitting process
    /// </summary>
    /// <param name="_myCart"></param>
    /// <exception cref="Exception"></exception>
    /// <exception cref="WrongEmailException"></exception>
    public void SubmitOrder(BO.Cart _myCart)
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
        Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        Match match = regex.Match(_myCart.CostumerEmail);
        if (!match.Success)
        {
            throw new WrongEmailException("email not good") { WrongEmail= _myCart.CostumerEmail };
        }
        #endregion
        #region create new order
        DO.Order _myNewOrder = new DO.Order()
        {
            OrderNum = 0,
            costumerName = _myCart.CostumerName,
            mail = _myCart.CostumerEmail,
            address = _myCart.CostumerAddress,
            OrderDate = DateTime.Now,
            shippingDate = null,
            arrivleDate = null
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

    }
    /// <summary>
    /// updates amount of wanted item in cart that user updated from his cart
    /// </summary>
    /// <param name="_myCart"></param>
    /// <param name="_id"></param>
    /// <param name="_newAmount"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    /// <exception cref="NotImplementedException"></exception>
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
}
