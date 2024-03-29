﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QDMarketPlace.Application.ViewModels.Product
{
    public class PurchaseHistoryViewModel
    {
        public string ProductName { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public int Quantity { set; get; }
        public DateTime DateCreated { set; get; }
    }
}
