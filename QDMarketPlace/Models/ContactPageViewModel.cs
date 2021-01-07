using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QDMarketPlace.Application.ViewModels.Common;

namespace QDMarketPlace.Models
{
    public class ContactPageViewModel
    {
        public ContactViewModel Contact { set; get; }

        public FeedbackViewModel Feedback { set; get; }
    }
}
