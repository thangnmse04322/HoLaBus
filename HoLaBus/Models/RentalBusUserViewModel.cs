using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace HoLaBus.Models
{
    public class RentalBusUserViewModel
    {
        public string UserId { get; set; }

        [Display(Name ="Họ Tên Khách Hàng")]
        [StringLength(70, ErrorMessage = "Độ dài phải nhỏ hơn 70 ký tự!")]
        [Required(ErrorMessage = "Họ Tên không được để trống!")]
        public string NameUser { get; set; }

      
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Số điện thoại không được có ký tự đặc biệt!")]
        [Required(ErrorMessage = "Số điện thoại không được để trống!")]
        [StringLength(10, ErrorMessage = "Số điện thoại phải là 10 số!", MinimumLength = 10)]
        [Display(Name = "Số Điện Thoại")]
        public string Phone { get; set; }

        [Display (Name ="Email")]
        [StringLength(100, ErrorMessage = "Độ dài phải nhỏ hơn 100 ký tự!")]
        [Required(ErrorMessage = "Email không được để trống!")]
        public string Email { get; set; }

        [Display(Name = "Địa Chỉ")]
        [Required(ErrorMessage = "Địa Chỉ không được để trống!")]
        [StringLength(400, ErrorMessage = "Độ dài phải nhỏ hơn 400 ký tự!")]
        public string Address { get; set; }


        [Display(Name = "Ngày Tạo Đơn")]
        public string Datebyu { get; set; }
        [StringLength(25, ErrorMessage = "Độ dài phải nhỏ hơn 25 ký tự!")]
        [Required(ErrorMessage = "Loại xe không được để trống!")]
        [Display(Name = "Loại Xe (Chỗ)")]
        public string Bustype { get; set; }


        [Display(Name ="Mã Thuê Xe")]
        public string RentalBusId { get; set; }

        [Display(Name = "Điểm Khởi Hành")]
        [Required(ErrorMessage = "Điểm khỏi hành không được để trống!")]
        [StringLength(200, ErrorMessage = "Độ dài phải nhỏ hơn 200 ký tự!")]
        public string Departure { get; set; }

     
        [Required(ErrorMessage = "Điểm đến không được để trống!")]
        [StringLength(200, ErrorMessage = "Độ dài phải nhỏ hơn 200 ký tự!")]
        [Display(Name = "Điểm Đến")]
        public string StopLocation { get; set; }

        public int PaymentStatusId { get; set; }

        [Display(Name = "Điểm Trả Khách Ngày Về")]
        [StringLength(200, ErrorMessage = "Độ dài phải nhỏ hơn 200 ký tự!")]
        [Required(ErrorMessage = "Điểm trả khách không được để trống!")]
        public string GettingOfLocation { get; set; }

        [Display(Name = "Thời Gian Đi")]
        [StringLength(100, ErrorMessage = "Độ dài phải nhỏ hơn 100 ký tự!")]
        [Required(ErrorMessage = "Thời gian về không được để trống! ")]
        public string DepartureDay { get; set; }

        [Display(Name = "Thời Gian Về")]
        [StringLength(100, ErrorMessage = "Độ dài phải nhỏ hơn 100 ký tự!")]
        [Required(ErrorMessage ="Thời gian về không được để trống!")]
        public string ReturnDay { get; set; }

        [Display(Name = "Số Tiền Thanh Toán")]
        public string TotalMoney { get; set; }

        
        public string Confim { get; set; }

   

    }
}
