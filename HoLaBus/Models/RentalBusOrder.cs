using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace HoLaBus.Models
{
    public class RentalBusOrder
    {
        [Key]
        [Display(Name = "Mã Thuê Xe")]
        [MaxLength(50)]
        public string RentalBusId { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserID { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Display(Name = "Ngày Tạo Đơn")]
        [MaxLength(20)]
        public string DateBuy { get; set; }
        [Display(Name = "Loại Xe (Chỗ)")]
        [MaxLength(20)]
        public string Bustype { get; set; }
        [ForeignKey("PaymentStatus")]
        public int PaymentStatusId { get; set; }
        public virtual PaymentStatus PaymentStatus { get; set; }

        [Display(Name = "Điểm Khởi Hành")]
        [MaxLength(200)]
        public string Departure { get; set; }

        [Display(Name = "Điểm Đến")]
        [MaxLength(200)]
        public string Stoplocation { get; set; }

        [Display(Name = "Điểm Trả Khách")]
        [MaxLength(200)]
        public string GettingOfBus { get; set; }

        [Display(Name = "Ngày Khởi Hành")]
        [MaxLength(100)]
        public string DepartureDay { get; set; }

        [Display(Name = "Ngày Về")]
        [MaxLength(100)]
        public string ReturnDay { get; set; }

        [Display(Name = "Số Tiền Thanh Toán")]
        [MaxLength(100)]
        public string TotalMoney { get; set; }

        [Display(Name = "Tình Trạng Thanh Toán")]
        [MaxLength(150)]
        public string ConfimDelivery { get; set; }

        [Display(Name = "Tên Khách Hàng")]
        [MaxLength(70)]
        public string NameGuest { get; set; }
        [Display(Name = "Địa Chỉ Khách Hàng")]
        [MaxLength(400)]
        public string AddGuest { get; set; }
        [Display(Name = "Số Điện Thoại Khách Hàng")]
        [MaxLength(12)]
        public string PhoneGuest { get; set; }
        [Display(Name = "Email Khách Hàng")]
        [MaxLength(100)]
        public string EmailGuest { get; set; }
    }

 
}