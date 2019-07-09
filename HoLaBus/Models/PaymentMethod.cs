using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HoLaBus.Models
{
    public class PaymentMethod
    {
        [Key]
        [Required(ErrorMessage = "Mã Phương thức thanh toán không được trống!")]
        [MaxLength(20)]
        [Display(Name = "Mã Phương Thức Thanh Toán")]
        public string PaymentId { get; set; }
        [Required (ErrorMessage ="Phương thức thanh toán không được trống!")]
        [Display (Name = "Phương Thức Thanh Toán")]
        [MaxLength(120)]
        public string Payment { get; set; }
        [Required (ErrorMessage ="Số tiền không được để trống!")]
        [Display (Name ="Phí Thanh Toán")]
        public double Monney { get; set; }


    }

    public class PaymentMethodUser
    {
        [Display (Name = "Chọn Phương Thức Thanh Toán")]
        public string paymentname { get; set; }
        public string paymentid { get; set;}
        public double paymentmoney { get; set; }
    }
}