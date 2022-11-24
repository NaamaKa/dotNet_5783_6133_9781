using BlApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bllmplementation;

sealed public class Bl : IBl
{
    public IOrder Order => new Order();
    public IProduct Product => new Product();
    public ICart Cart => new Cart();

}
