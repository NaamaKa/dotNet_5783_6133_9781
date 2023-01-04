using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO;


public class GetPredictNullException : Exception
{
    public string? GetPredictNull { get; set; }

    public GetPredictNullException(string msg) : base(msg)
    {
    }
}
[Serializable]
public class DalConfigException : Exception
{
    public DalConfigException(string msg) : base(msg) { }
    public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
}

public class RequestedItemNotFoundException : Exception
{
    public string? RequestedItemNotFound { get; set; }

    public RequestedItemNotFoundException(string msg) : base(msg)
    {
    }

}
public class RequestedOrderNotFoundException : Exception
{
    public string? RequestedOrderNotFound { get; set; }

    public RequestedOrderNotFoundException(string msg) : base(msg)
    {
    }

}
public class RequestedOrdersItemNotFoundException : Exception
{
    public RequestedOrdersItemNotFoundException(string msg) : base(msg)
    {
    }

}
public class RequestedOrderItemNotFoundException : Exception
{
    public string? RequestedOrderItemNotFound { get; set; }

    public RequestedOrderItemNotFoundException(string msg) : base(msg)
    {
    }

}
public class RequestedUpdateItemNotFoundException : Exception
{
    public string? RequestedUpdateItemNotFound { get; set; }

    public RequestedUpdateItemNotFoundException(string msg) : base(msg)
    {
    }

}
public class NoStatusExeption : Exception
{
    public string? NoStatus { get; set; }

    public NoStatusExeption(string msg) : base(msg)
    {
    }

}

