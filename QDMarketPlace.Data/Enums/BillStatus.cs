using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace QDMarketPlace.Data.Enums
{
    public enum BillStatus
    {
        [Description("Đơn mới")]
        New,
        [Description("Đang xử lý")]
        InProgress,
        [Description("Trả lại")]
        Returned,
        [Description("Đã hủy")]
        Cancelled,
        [Description("Hoàn tất")]
        Completed
    }
}
