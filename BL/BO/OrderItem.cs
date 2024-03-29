﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class OrderItem
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public int NumInOrder { get; set; }
    public double Price { get; set; }
    public int Amount { get; set; }
    public double TotalPrice { get; set; }
    public override string ToString() => $@"
        id={ID},
        name :{Name}, 
        num in order : {NumInOrder},
    	Price : {Price},
    	Amount: {Amount},
        tatal price :{TotalPrice}
    ";
}
