using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class ProductForList
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public Enums.Category Category { get; set; }
    public override string ToString() => $@"
        id={Id},
        name :{Name}, 
    	Price : {Price},
        category: {Category}
    ";
}
