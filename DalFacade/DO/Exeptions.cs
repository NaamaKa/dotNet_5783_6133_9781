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

public class RequestedItemNotFoundException : Exception
{
    public string? RequestedItemNotFound { get; set; }

    public RequestedItemNotFoundException(string msg) : base(msg)
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


public class ItemAlreadyExistsException : Exception
{
    public string? ItemAlreadyExists { get; set; }

    public ItemAlreadyExistsException(string msg) : base(msg)
    {
    }
}
