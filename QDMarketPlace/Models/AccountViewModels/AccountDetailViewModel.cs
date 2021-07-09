using QDMarketPlace.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QDMarketPlace.Models.AccountViewModels
{
    public class AccountDetailViewModel
    {
        public string FullName { get; set; }

        public DateTime? BirthDay { set; get; }

        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public Status Status { get; set; }
        public bool IsDeleted { get; set; }
    }
}
