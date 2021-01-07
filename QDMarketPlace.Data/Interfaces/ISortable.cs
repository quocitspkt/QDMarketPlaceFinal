using System;
using System.Collections.Generic;
using System.Text;

namespace QDMarketPlace.Data.Interfaces
{
    public interface ISortable
    {
        int SortOrder { set; get; }
    }
}
