﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
namespace HoLaBus.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(400)]
        [Display (Name ="Địa Chỉ Giao Vé")]
        public string Address { get; set; }
        [MaxLength(80)]
        [Display(Name = "Họ và Tên")]
        public string FullName { get; set; }
        [MaxLength(12)]
        [Display(Name = "Số điện thoại")]
        [Phone(ErrorMessage = "Số điện thoại không được có ký tự đặc biệt!")]
        public override string PhoneNumber { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}