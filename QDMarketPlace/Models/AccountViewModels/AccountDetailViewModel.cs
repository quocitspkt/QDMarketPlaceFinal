using QDMarketPlace.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QDMarketPlace.Models.AccountViewModels
{
    public class AccountDetailViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage ="Họ tên không được để trống",AllowEmptyStrings =false)]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Ngày sinh không được để trống", AllowEmptyStrings = false)]
        public DateTime BirthDay { set; get; }
        [Required(ErrorMessage = "Số điện thoại không được để trống", AllowEmptyStrings = false)]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Email không được để trống", AllowEmptyStrings = false)]
        [EmailAddress(ErrorMessage ="Email không hợp lệ")]
        public string Email { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public Status Status { get; set; }
        public bool IsDeleted { get; set; }
    }
}
