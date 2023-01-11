using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    #region product exceptions

    public class NegativeIdException : Exception
    {
        public string? NegativeId { get; set; }

        public NegativeIdException(string msg) : base(msg) { }
    }

    public class EmptyNameException : Exception
    {
        public string? EmptyName { get; set; }

        public EmptyNameException(string msg) : base(msg) { }
    }


    public class NegativePriceException : Exception
    {
        public string? NegativePrice { get; set; }

        public NegativePriceException(string msg) : base(msg) { }
    }



    public class NegativeStockException : Exception
    {
        public string? NegativeStock { get; set; }

        public NegativeStockException(string msg) : base(msg) { }
    }


    public class ProductAlreadyExistsException : Exception
    {
        public string? ProductAlreadyExists { get; set; }

        public ProductAlreadyExistsException(string msg) : base(msg) { }
    }



    public class ProductInUseException : Exception
    {
        public string? ProductInUse { get; set; }

        public ProductInUseException(string msg) : base(msg) { }
    }

    public class ProductNotExistsException : Exception
    {
        public string? ProductNotExists { get; set; }

        public ProductNotExistsException(string msg) : base(msg) { }
    }

    #endregion


    #region cart exceptions
    public class ItemAlreadyExistsException : Exception
    {
        public string? ItemAlreadyExists { get; set; }

        public ItemAlreadyExistsException(string msg) : base(msg) { }
    }


    public class NotEnoughInStockException : Exception
    {
        public string? NotEnoughInStock { get; set; }

        public NotEnoughInStockException(string msg) : base(msg) { }
    }


    public class ProductNotInStockException : Exception
    {
        public string? ProductNotInStock { get; set; }

        public ProductNotInStockException(string msg) : base(msg) { }
    }


    public class ItemNotInCartException : Exception
    {
        public string? ItemNotInCart { get; set; }

        public ItemNotInCartException(string msg) : base(msg) { }
    }


    public class NegativeAmountException : Exception
    {
        public string? NegativeAmount { get; set; }

        public NegativeAmountException(string msg) : base(msg) { }
    }


    public class NameIsNullException : Exception
    {
        public string? NameIsNull { get; set; }

        public NameIsNullException(string msg) : base(msg) { }
    }


    public class AdressIsNullException : Exception
    {
        public string? AdressIsNull { get; set; }

        public AdressIsNullException(string msg) : base(msg) { }
    }


    public class ItemInCartNotExistsAsProductException : Exception
    {
        public string? ItemInCartNotExistsAsProduct { get; set; }

        public ItemInCartNotExistsAsProductException(string msg) : base(msg) { }
    }


    public class UncorrectEmailException : Exception
    {
        public string? UncorrectEmail { get; set; }

        public UncorrectEmailException(string msg) : base(msg) { }
    }


    public class FieldToGetProductException : Exception
    {
        public string? FieldToGetProduct { get; set; }

        public FieldToGetProductException(string msg) : base(msg) { }
    }
    public class SendEmptyCartException : Exception
    {
        public string? SendEmptyCart { get; set; }
        public SendEmptyCartException(string msg) : base(msg) { }

    }
    #endregion
    #region order exception

    public class OrderNotExistsException : Exception
    {
        public string? OrderNotExists { get; set; }

        public OrderNotExistsException(string msg) : base(msg) { }
    }


    public class UpdateOrderNotSucceedException : Exception
    {
        public string? UpdateOrderNotSucceed { get; set; }

        public UpdateOrderNotSucceedException(string msg) : base(msg) { }
    }


    public class OrderHasAlreadySentException : Exception
    {
        public string? OrderHasAlreadySent { get; set; }

        public OrderHasAlreadySentException(string msg) : base(msg) { }
    }

    public class OrderHasAlreadyProvidedException : Exception
    {
        public string? OrderHasAlreadyProvided { get; set; }

        public OrderHasAlreadyProvidedException(string msg) : base(msg) { }
    }
    public class NoOrdersForListExeption : Exception
    {
        public NoOrdersForListExeption(string msg) : base(msg) { }
    }
    public class EmptyAddressException : Exception
    {
        public string? EmptyAddress { get; set; }

        public EmptyAddressException(string msg) : base(msg) { }
    }
    public class EmptyEmailException : Exception
    {
        public string? EmptyEmail { get; set; }

        public EmptyEmailException(string msg) : base(msg) { }
    }
    #endregion


}
