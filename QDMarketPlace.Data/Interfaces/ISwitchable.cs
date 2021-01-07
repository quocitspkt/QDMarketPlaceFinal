using System;
using System.Collections.Generic;
using System.Text;
using QDMarketPlace.Data.Enums;

namespace QDMarketPlace.Data.Interfaces
{
    public interface ISwitchable
    {
        Status Status { set; get; }
    }
}
