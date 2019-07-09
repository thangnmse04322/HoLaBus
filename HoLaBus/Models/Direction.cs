using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoLaBus.Models
{
    public class Direction
    {
        [Key]
        //[Required(ErrorMessage = "Mã tuyến xe không được để trống!")]update
        [Display(Name = "Mã Tuyến Xe")]
        public string BusId { get; set; }

        [Required(ErrorMessage = "Trạng thái xe không được để trống")]
        [Display(Name = "Trạng Thái Xe")]
        public bool BusStatus { get; set; }

        [Required(ErrorMessage = "Tên tuyến xe không được để trống!")]
        [MaxLength(60)]
        [Display(Name = "Tên Tuyến Xe")]
        public string RoadName { get; set; }

        [Required(ErrorMessage = "Giá vé xe không được để trống!")]
        [Display(Name = " Giá Vé ")]
        public double TicketPrice { get; set; }

        [Required(ErrorMessage = "Chiết khấu không được để trống!")]
        [Display(Name = " Giảm Giá (%)")]
        public double DiscountValue { get; set; }

        [Required(ErrorMessage = "Số lượng ghế xe không được để trống!")]
        [Display(Name ="Loại Xe (Ghế)")]
        public double NumberTicket { get; set; }

        [Required(ErrorMessage ="Không được để trống!")]
        [Display(Name = " Điểm Dừng 1 ")]
        [MaxLength(70)]
        public string StopLocation1 { get; set; }
        [Display(Name = " Điểm Dừng 2 ")]
        [MaxLength(70)]
        public string StopLocation2 { get; set; }
        [Display(Name = " Điểm Dừng 3 ")]
        [MaxLength(70)]
        public string StopLocation3 { get; set; }
        [Display(Name = " Điểm Dừng 4 ")]
        [MaxLength(70)]
        public string StopLocation4 { get; set; }
        [Display(Name = " Điểm Dừng 5 ")]
        [MaxLength(70)]
        public string StopLocation5 { get; set; }
        [Display(Name = " Điểm Dừng 6 ")]
        [MaxLength(70)]
        public string StopLocation6 { get; set; }
        public ICollection<SeatInformation> SeatInformation { get; set; }

        public double PriceFirst { get; set; }
    }
}