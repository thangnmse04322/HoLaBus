using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace HoLaBus.Models
{
    public class CartViewModel
    {
        public string UserId { get; set; }
        [Display(Name ="Email")]
        public string EmailUser { get; set; }
        [Display(Name = "Số Điện Thoại")]
        public string PhoneUser { get; set; }
        [Display(Name = "Họ Và Tên")]
        public string FullName { get; set; }
        [Display(Name = "Phí Giao Vé")]
        public double DeliveryMoney { get; set; }
        [Display(Name = "Địa Chỉ Thanh Toán Khách Hàng")]
        public string AddressUser { get; set; }
        [Display(Name = "Điển Dừng Khách Hàng")]
        public string StopLoaction { get; set; }
        [Display(Name = "Mã Tuyến Xe")]
        public string BusId { get; set; }
        [Display(Name ="Tên Tuyến Xe")]
        public string RoadName { get; set; }
        [Display(Name = "Phương Thức Thanh Toán")]
        public string PaymentMethod { get; set; }
        public string PaymentId { get; set; }
        public List<SeatInformationViewModel> Seats { get; set; } = new List<SeatInformationViewModel>();
        public List<int> SeatsSelected { get; set; } = new List<int>();
        [Display(Name = "Số Tiền Thanh Toán")]
        public double TotalMoney { get; set; }
        [Display(Name = "Trạng Thái Thanh Toán")]
        public string PaymentStatusName { get; set; }
        public int PaymentStatusId { get; set; }
        [Display(Name = "Mã Vé")]
        public string TicketId { get; set; }
        [Display(Name = "Tổng Số Ghế Đã Chọn")]
        public int TotalSeat { get; set; }
        [Display(Name ="Ngày Đặt Vé")]
        public string BusDate { get; set; }
        [Display(Name = "Được Giảm Giá")]
        public double Discount { get; set; }
        [Display(Name = "Tên Ghế")]
        public string NameSeat { get; set; } //not using

        [Display(Name = "Date Check")]
        public DateTime DateCheck { get; set; }
        //[Required(ErrorMessage = "Bạn phải chọn xác nhận để hoàn tất!")]

        [Display(Name ="Xác Nhận")]
        public bool Confim { get; set; }
       
        [Display(Name = "Xác Nhận Thanh Toán")]
        public string ConfimDelivery { get; set; }

        [Display(Name ="Giá Vé")]
        public double PriceTekit { get; set; }

    }


}