using System;
using System.Collections.Generic;
using System.Text;

namespace QDMarketPlace.Data.Interfaces
{
    public interface IDateTracking
    {
        DateTime DateCreated { set; get; }

        DateTime DateModified { set; get; }
    }
}
