﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BlApi;

public interface ICart
{
    public void OpenCart(Cart myCart);
    public Cart? AddItemToCart(Cart _myCart, int itemId,OrderItem item);
    public Cart? UpdateAmountOfItem(Cart _myCart, int _id, int _newAmount);
    public void SubmitOrder(Cart _myCart);
    public int ReturnAmountOfItemInCart(BO.Cart _myCart, int _id);
}
