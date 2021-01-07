using System;
using System.Collections.Generic;
using System.Text;

namespace QDMarketPlace.Data.Interfaces
{
    public interface IHasOwner<T>
    {
        T OwnerId { set; get; }


    }
}
