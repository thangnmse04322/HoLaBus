using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HoLaBus.Models
{
    public class Feedback
    {
        [Key]
        public int FeedbackId { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Display(Name ="Khách Hàng Đánh Giá")]
        public string FeedbackUser { get; set; }

        [Display(Name ="Hola Bus Phản Hồi")]
        public string FeedbackAdmin { get; set; }
        [Display(Name = "Ngày Viết")]
        [MaxLength(20)]
        public string DateCreate { get; set; }
        [Display(Name ="Tên Khách Hàng")]
        [MaxLength(50)]
        public string GuestName { get;set;}

    }

    public class GetFeedback
    {

        public string UserId { get; set; }
        [Display(Name ="Tên Khách Hàng")]
        [Required(ErrorMessage ="Tên không được để trống!")]
        public string UserName { get; set; }

        [Display(Name = "Viết Đánh Giá")]
        [Required(ErrorMessage = "Không được để trống!")]
        public string Feedback { get; set; }

        [Display(Name = "Tên Khách Hàng")]
        [Required(ErrorMessage = "Không được để trống!")]
        public string Name { get; set; }

        [Display(Name = "Ngày Viết")]
        public string DateCreate { get; set; }

    }

    public class SendRequest
    {
        [Required(ErrorMessage ="Không được để trống!")]
        [Display(Name = "Họ Và Tên!")]
        public string name { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống!")]
        [StringLength(10, ErrorMessage = "Số điện thoại phải là 10 số!", MinimumLength = 10)]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Số điện thoại không được có ký tự đặc biệt!")]
        [Display(Name = "Số Điện Thoại")]
        public string phone { get; set; }

        [Display(Name = "Email!")]
        [Required(ErrorMessage = "Không được để trống!")]
        public string email { get; set; }

        [Required(ErrorMessage = "Không được để trống!")]
        [Display(Name = "Viết Tin Nhắn!")]
        public string requetguest { get; set; }

    }


}