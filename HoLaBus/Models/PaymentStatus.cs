using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace HoLaBus.Models
{
    public class PaymentStatus
    {
        [Key]
        public int PaymentStatusId { get; set; }
        [Display (Name ="HoLa Bus Xác Nhận")]
        [MaxLength(30)]
        public string PaymentStatusName { get; set; }
    }
}