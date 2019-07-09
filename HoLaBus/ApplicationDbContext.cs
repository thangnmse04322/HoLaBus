using HoLaBus.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HoLaBus
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<News> News { get; set; }
        public DbSet<Direction> Directions { get; set; }
        public DbSet<PaymentMethod> PaymentMethod { get; set; }
        public DbSet<PaymentStatus> PaymentStatus { get; set; }
        public DbSet<TicketBooking> TicketBooking { get; set; }
        public DbSet<SeatInformation> SeatInformation { get; set; }
        public DbSet<RentalBusOrder> RentalBusOrder { get; set; }
        public DbSet<Feedback> Feedback { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<News>().ToTable("News");
            modelBuilder.Entity<Direction>().ToTable("Directions");
            modelBuilder.Entity<PaymentMethod>().ToTable(" PaymentMethod");
            modelBuilder.Entity<PaymentStatus>().ToTable("PaymentStatus");
            modelBuilder.Entity<TicketBooking>().ToTable("TicketBooking");
            modelBuilder.Entity<SeatInformation>().ToTable("SeatInformation");
            modelBuilder.Entity<RentalBusOrder>().ToTable("RentalBusOrder");
            modelBuilder.Entity<Feedback>().ToTable("Feedback");
        }

        
    }
}