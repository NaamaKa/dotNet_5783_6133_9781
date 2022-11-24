using BL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class Cart
{
    public string CostumerName{get; set;}
    public string CostumerEmail{get; set;}
    public string CostumerAddress { get; set; }
    public List <OrderItem> Items { get; set; }
    public double Price { get; set; }
    public override string ToString() => $@"
        name :{CostumerName},
        email :{CostumerEmail}, 
        address : {CostumerAddress},
    	items: {Items},
        tatal price :{Price}
    ";
}
