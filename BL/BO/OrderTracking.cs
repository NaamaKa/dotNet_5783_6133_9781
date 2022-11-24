﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public  class OrderTracking
{
    public int ID { get; set; }
    public OrderStatus Status { get; set; }

    public override string ToString() => $@"
        id={ID},
        name :{Name}, 
        product id : {ProductId},
    	Price : {Price},
    	Amount: {Amount},
        tatal price :{TotalPrice}
    ";
}
