using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace HoLaBus.Models
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public bool IsAdmin { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mật Khẩu Cũ")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Độ dài mật khẩu phải lớn hơn 6 và có đủ các ký tự đặc biệt!", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật Khẩu Mới")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Xác Nhận Mật khẩu")]
        [Compare("NewPassword", ErrorMessage = "Mật khẩu xác nhận không đúng!")]
        public string ConfirmPassword { get; set; }
    }

    public class AddPhoneNumberViewModel
    {
        [Required(ErrorMessage = "Số điện thoại không được để trống!")]
        [Phone]
        [Display(Name = "Số Điện Thoại")]
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }

    public class UpdateUserInfoViewModel
    {
        [Required(ErrorMessage = "Tên khách hàng không được để trống!")]
        [MaxLength(100)]
        [Display(Name = "Tên Khách Hàng")]
        public string NameUser { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống!")]
        [StringLength(10, ErrorMessage = "Số điện thoại phải là 10 số!", MinimumLength = 10)]
        [Phone (ErrorMessage ="Số điện thoại không được có ký tự đặc biệt!")]
        [Display(Name = "Số Điện Thoại")]
        public string Number { get; set; }

        [Required(ErrorMessage = "Địa chỉ không được để trống!")]
        [MaxLength(100)]
        [Display(Name = "Địa Chỉ Giao Vé")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Tên Tài Khoản không được để trống!")]
        [MaxLength(100)]
        [Display(Name = "Tên Tài Khoản")]
        public string AcccountName { get; set; }

    }

    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}