

using System.Diagnostics;
using System.Xml.Linq;

namespace DO;

public struct Product
{
    static int counter = 10000;
    public int barkode { get { return barkode; } set { counter++; } }
    public string productName{ get; set; }
    public string productCategory{ get; set; }
    public float productPrice{ get; set; }
    public int inStock { get; set; }
    public override string ToString() => $@"
        Product ID={barkode}: {productName}, 
        category - {productCategory}
    	Price: {productPrice}
    	Amount in stock: {inStock}
    ";

}
