

using System.Diagnostics;
using System.Xml.Linq;

namespace DO;
public struct Product
{
    public int barkode { get; set; }
    public string? productName{ get; set; }
    public Enums.Category? productCategory{ get; set; }
    public double productPrice{ get; set; }
    public int inStock { get; set; }
    public override string ToString() => $@"
        Product ID={barkode}: {productName}, 
        category - {productCategory}
    	Price: {productPrice}
    	Amount in stock: {inStock}
    ";

}
