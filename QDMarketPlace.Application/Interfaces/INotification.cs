using System;
using System.Collections.Generic;
using System.Text;

namespace QDMarketPlace.Application.Interfaces
{
    public interface INotification
    {
        public void SendNotification(string subject, string body, string to);
    }
}
