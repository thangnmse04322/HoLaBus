using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace HoLaBus.Models
{
    public class LocationViewModel
    {
        [Display(Name = "Mã Tuyến")]
        public string BusId { get; set; }
        [Display(Name = "Tên Tuyến Xe")]
        public string RoadName { get; set; }
        [Display(Name = "Giá Vé")]
        public double PriceTicket { get; set; }
        [Display(Name = "Loại Xe (Ghế)")]
        [Required]
        public int TotalSeat { get; set; }

        [Required(ErrorMessage ="Phương thức thanh toán không được để trống!")]
        [Display(Name = "Chọn Phương Thức Thanh Toán")] //using to display
        public string Payment { get; set; }
        [Display(Name = "Chú Thích:")] //using to display
        public string BusType { get; set; }

        [Display(Name = "Chọn Điểm Dừng")]
        [Required]
        public List<SelectListItem> LocationsList { get; set; } = new List<SelectListItem>();

        [Display(Name = "Chọn Ghế: ")]
        public List<SeatInformationViewModel> SeatInformation { get; set; } = new List<SeatInformationViewModel>();
    }
}