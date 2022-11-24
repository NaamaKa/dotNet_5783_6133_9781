﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
namespace DalApi
{
    public interface IDal
    {
        public IProduct product { get; }
        public IOrder order { get; }
        public IOrderItem orderItem { get; }
    }
}
