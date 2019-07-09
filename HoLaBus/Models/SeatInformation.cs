using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HoLaBus.Models
{
    public class SeatInformation
    {
        [Key]
        public int SeatId { get; set; }

        [Display(Name = "Số Ghế")]
        [MaxLength(10)]
        public string SeatName { get; set; }

        [ForeignKey("TicketBooking")]
        
        public string TicketId { get; set; }
        public virtual TicketBooking TicketBooking { get; set; }

        [ForeignKey("Direction")]
        [Display(Name = "Mã Tuyến")]
        public string BusId { get; set; }
        public virtual Direction Direction {get;set;}

        [Display(Name = "Đóng / Mở ")]
        public bool Status { get; set; }

        [Display(Name ="Tên Tuyến Xe")]
        [MaxLength(70)]
        public string BusName { get; set; }

        [Display(Name ="Trạng Thái Ghế")]
        [MaxLength(50)]
        [Required(ErrorMessage ="Không được để trống!")]
        public string CommentAdmin { get; set; }

      
    }
     public class ViewSeatBuy
    {
        [Display(Name = "Mã Tuyến")]
        public string Idbus { get; set; }
        [Display(Name = "Tên Tuyến")]
        public string Busname { get; set; }
        [Display(Name = "Giá Vé")]
        public double TicketPrice { get; set; }
        [Display(Name = "Tổng Số Ghế")]
        public double TotalSeat { get; set; }
        [Display(Name = "Tổng Ghế Đã Bán")]
        public double SeatBuy { get; set; }
        [Display(Name = "Số Ghế Còn Lại")]
        public double SeatExist { get; set; }
        [Display(Name = "Giảm Giá %")]
        public double Discount { get; set; }
        [Display(Name = "Trạng Thái Xe")]
        public bool Status { get; set; }
    }

     public class MoneyManagent {
         [Display(Name = "Tổng Số Tiền Toàn Bộ Xe")]
        public double totalmoney { get; set; }
        [Display(Name = "Tổng Số Ghế")]
        public double totalseat { get; set; }
        [Display(Name = "Tên Tuyến")]
        public string namebus { get; set; }
        [Display(Name = "Số Tiền Của Số Ghế Đã Bán")]
        public double monneycount { get; set; }
        [Display(Name = "Số Tiền Của Số Ghế Còn Lại Chưa Bán")]
        public double moneyconlai { get; set; }
        [Display(Name = "Tình Trạng")]
        public double moneylo { get; set; }

        public  double moneylai { get; set; }
     }
}