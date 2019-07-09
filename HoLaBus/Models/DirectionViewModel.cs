using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace HoLaBus.Models
{

    public class DirectionViewModel
    {
        [Display(Name = "Tên Tuyến Xe")]
        public string BusId { get; set; }
        public string RoadName { get; set; }
        public bool BusStatus { get; set; }
    }
}