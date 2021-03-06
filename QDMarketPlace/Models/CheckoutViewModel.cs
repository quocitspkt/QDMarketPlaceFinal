﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QDMarketPlace.Application.ViewModels.Common;
using QDMarketPlace.Application.ViewModels.Product;
using QDMarketPlace.Data.Enums;
using QDMarketPlace.Utilities.Extensions;

namespace QDMarketPlace.Models
{
    public class CheckoutViewModel : BillViewModel
    {
        public List<ShoppingCartViewModel> Carts { get; set; }
        public List<EnumModel> PaymentMethods
        {
            get
            {
                return ((PaymentMethod[])Enum.GetValues(typeof(PaymentMethod)))
                    .Select(c => new EnumModel
                    {
                        Value = (int)c,
                        Name = c.GetDescription()
                    }).ToList();
            }
        }
    }
}
