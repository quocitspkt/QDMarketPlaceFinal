using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QDMarketPlace.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Họ tên không được trống", AllowEmptyStrings = false)]
        [Display(Name = "Full name")]
        public string FullName { set; get; }

        [Display(Name = "DOB")]
        public DateTime? BirthDay { set; get; }

        [Required(ErrorMessage = "Email không được để trống", AllowEmptyStrings = false)]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Mật khẩu không được để trống",AllowEmptyStrings =false)]
        [StringLength(100, ErrorMessage = "{0} phải ít nhất {2} và nhiều nhất {1} kí tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Mật khẩu và mật khẩu xác nhận không khớp.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Phone number")]
        public string PhoneNumber { set; get; }

        [Display(Name = "Avatar")]
        public string Avatar { get; set; }
    }
}
