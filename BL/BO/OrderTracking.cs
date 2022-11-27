using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BO;

public  class OrderTracking
{
    public int ID { get; set; }
    public Enums.OrderStatus Status { get; set; }
    public List<DateTime> StatusDate { get; set; }

    public override string ToString() => $@"
        id={ID},
        status= {Status}
    ";
}
