using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace HoLaBus.Models
{
    public class News
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Tên Tin Tức")]
        [MaxLength(120)]
        public string Name { get; set; }
        [Display(Name = "Nội Dung")]
        public string Description { get; set; }
        [Display(Name = "Ngày Tạo")]
        [MaxLength(20)]
        public string CreatedAt { get; set; }
        [Display(Name = "Ngày Sửa")]
        [MaxLength(20)]
        public string UpdateAt { get; set; }

        [Display(Name = "Người Viết")]
        [MaxLength(50)] 
        public string AuthorName { get; set; }



    }

    
}