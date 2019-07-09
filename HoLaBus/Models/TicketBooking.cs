using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HoLaBus.Models
{
    public class TicketBooking
    {
        [Key]
        [Display(Name = "Mã Vé")]
        public string TicketId { get; set; }

        [ForeignKey("Direction")]
        public string BusId { get; set; }
        public virtual Direction Direction { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Display(Name = "Điểm Dừng")]
        [MaxLength(100)]
        public string StopLocationNumber { get; set; }
        [Display(Name = "Tổng Số Ghế")]
        public int TotalSeatNumber { get; set; }

        [ForeignKey("PaymentMethod")]
        [Display(Name = "Phương Thức Thanh Toán")]
        public string PaymentMethodId { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }

        [ForeignKey("PaymentStatus")]
        [Display(Name = "Admin Xác Nhận Thanh Toán")]
        public int PaymentStatusId { get; set; }
        public virtual PaymentStatus PaymentStatus { get; set; }

        [Display(Name = "Check Send Mail")]
        public bool Confim { get; set; }
        [Display(Name = "Tên Ghế")]
        [MaxLength(200)]
        public string SeatName { get; set; }
        [Display(Name = "Ngày Mua")]
        [MaxLength(20)]
        public string DateBuy { get; set; }
        [Display(Name = "Tình Trạng Thanh Toán")]
        [MaxLength(150)]
        public string ConfimDelivery { get; set; }

        [Display(Name = "Khách Hàng")]
        [MaxLength(50)]
        public string UserName { get; set; }
        [Display(Name = "Phí Giao Vé")]
        public double DeliveryMoney { get; set; }

        [Display(Name = "Được Giảm Giá")]
        public double Discount { get; set; }

        [Display(Name = "Ghi Chú")]
        [MaxLength(400)]
        public string NoteAdmin { get; set; }

        [Display(Name ="Date Check")]
        public DateTime DateCheck { get; set; }

        [Display(Name = "Số Tiền Thanh Toán")]
        public double Total { get; set; }
        public virtual ICollection<SeatInformation> SeatInformations { get; set; }

        [Display(Name = "Giá Vé")]
        public double PriceTicket { get; set; }

    }

    public class ImportEX
    {
   
        [Display(Name = "Mã Vé")]
        public string TicketId { get; set; }

        [Display(Name = "Tên Tuyến")]
        public string BusName { get; set; }

        [Display(Name = "Mã Tuyến")]
        public string BusId { get; set; }
      
        [ForeignKey("Tên Khách Hàng")]
        public string UserName { get; set; }
      
        [Display(Name = "Điểm Dừng")]
        public string StopLocationNumber { get; set; }

        [Display(Name = "Tổng Số Ghế")]
        public int TotalSeatNumber { get; set; }

       
        [Display(Name = "Phương Thức Thanh Toán")]
        public string PaymentMethodId { get; set; }
        

        
        [Display(Name = " Xác Nhận Thanh Toán")]
        public string PaymentStatusName { get; set; }
       

        [Display(Name = "Ngày Mua")]
        public string DateBuy { get; set; }
      

        [Display(Name = "Phí Giao Vé")]
        public double DeliveryMoney { get; set; }

        [Display(Name = "Được Giảm Giá")]
        public double Discount { get; set; }

        [Display(Name = "Ghi Chú")]
        public string NoteAdmin { get; set; }

        [Display(Name = "Số Tiền Thanh Toán")]
        public double Total { get; set; }

        public string NameSeat { get; set; }
       
    }

  
}