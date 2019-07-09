namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDataTypeFinal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Directions", "StopLocation1", c => c.String(maxLength: 70));
            AlterColumn("dbo.Directions", "StopLocation2", c => c.String(maxLength: 70));
            AlterColumn("dbo.Directions", "StopLocation3", c => c.String(maxLength: 70));
            AlterColumn("dbo.Directions", "StopLocation4", c => c.String(maxLength: 70));
            AlterColumn("dbo.Directions", "StopLocation5", c => c.String(maxLength: 70));
            AlterColumn("dbo.Directions", "StopLocation6", c => c.String(maxLength: 70));
            AlterColumn("dbo.TicketBooking", "StopLocationNumber", c => c.String(maxLength: 100));
            AlterColumn("dbo.TicketBooking", "SeatName", c => c.String(maxLength: 10));
            AlterColumn("dbo.TicketBooking", "UserName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Users", "Address", c => c.String(maxLength: 400));
            AlterColumn("dbo.Users", "FullName", c => c.String(maxLength: 80));
            AlterColumn("dbo.Users", "PhoneNumber", c => c.String(maxLength: 12));
            AlterColumn("dbo.Feedback", "GuestName", c => c.String(maxLength: 50));
            AlterColumn("dbo.News", "AuthorName", c => c.String(maxLength: 50));
            AlterColumn("dbo.RentalBusOrder", "ConfimDelivery", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RentalBusOrder", "ConfimDelivery", c => c.String());
            AlterColumn("dbo.News", "AuthorName", c => c.String());
            AlterColumn("dbo.Feedback", "GuestName", c => c.String());
            AlterColumn("dbo.Users", "PhoneNumber", c => c.String());
            AlterColumn("dbo.Users", "FullName", c => c.String());
            AlterColumn("dbo.Users", "Address", c => c.String());
            AlterColumn("dbo.TicketBooking", "UserName", c => c.String());
            AlterColumn("dbo.TicketBooking", "SeatName", c => c.String());
            AlterColumn("dbo.TicketBooking", "StopLocationNumber", c => c.String());
            AlterColumn("dbo.Directions", "StopLocation6", c => c.String(maxLength: 50));
            AlterColumn("dbo.Directions", "StopLocation5", c => c.String(maxLength: 50));
            AlterColumn("dbo.Directions", "StopLocation4", c => c.String(maxLength: 50));
            AlterColumn("dbo.Directions", "StopLocation3", c => c.String(maxLength: 50));
            AlterColumn("dbo.Directions", "StopLocation2", c => c.String(maxLength: 50));
            AlterColumn("dbo.Directions", "StopLocation1", c => c.String(maxLength: 50));
        }
    }
}
