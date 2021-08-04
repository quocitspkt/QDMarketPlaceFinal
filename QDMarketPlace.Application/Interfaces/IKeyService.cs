using System;
using System.Collections.Generic;
using System.Text;

namespace QDMarketPlace.Application.Interfaces
{
    public interface IKeyService :IDisposable
    {
        string GetById(int productId, int quantity);
        void Save();
    }
}
