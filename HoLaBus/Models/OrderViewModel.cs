using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoLaBus.Models
{
    public class OrderViewModel
    {
       
        public string BusId { get; set; }
        public string Location { get; set; }
        public string Payment { get; set; }
        public List<int> Seat { get; set; }
      
    }

}