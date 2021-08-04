using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace QDMarketPlace.Data.Enums
{
    public enum PaymentMethod
    {
        
        [Description("PayPal")]
        PayPal,
        [Description("Thanh toán khi nhận hàng")]
        MoMo
    }
}
