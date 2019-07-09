namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDatatype : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SeatInformation", "BusId", "dbo.Directions");
            DropForeignKey("dbo.TicketBooking", "BusId", "dbo.Directions");
            DropForeignKey("dbo.TicketBooking", "PaymentMethodId", "dbo.PaymentMethod");
            DropIndex("dbo.SeatInformation", new[] { "BusId" });
            DropIndex("dbo.TicketBooking", new[] { "BusId" });
            DropIndex("dbo.TicketBooking", new[] { "PaymentMethodId" });
            DropPrimaryKey("dbo.Directions");
            DropPrimaryKey("dbo.PaymentMethod");
            DropPrimaryKey("dbo.RentalBusOrder");
            AlterColumn("dbo.Directions", "BusId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Directions", "RoadName", c => c.String(nullable: false, maxLength: 60));
            AlterColumn("dbo.Directions", "StopLocation1", c => c.String(maxLength: 50));
            AlterColumn("dbo.Directions", "StopLocation2", c => c.String(maxLength: 50));
            AlterColumn("dbo.Directions", "StopLocation3", c => c.String(maxLength: 50));
            AlterColumn("dbo.Directions", "StopLocation4", c => c.String(maxLength: 50));
            AlterColumn("dbo.Directions", "StopLocation5", c => c.String(maxLength: 50));
            AlterColumn("dbo.Directions", "StopLocation6", c => c.String(maxLength: 50));
            AlterColumn("dbo.SeatInformation", "BusId", c => c.String(maxLength: 128));
            AlterColumn("dbo.SeatInformation", "CommentAdmin", c => c.String(maxLength: 50));
            AlterColumn("dbo.TicketBooking", "BusId", c => c.String(maxLength: 128));
            AlterColumn("dbo.TicketBooking", "PaymentMethodId", c => c.String(maxLength: 20));
            AlterColumn("dbo.TicketBooking", "DateBuy", c => c.String(maxLength: 20));
            AlterColumn("dbo.TicketBooking", "ConfimDelivery", c => c.String(maxLength: 150));
            AlterColumn("dbo.PaymentMethod", "PaymentId", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.PaymentMethod", "Payment", c => c.String(nullable: false, maxLength: 120));
            AlterColumn("dbo.PaymentStatus", "PaymentStatusName", c => c.String(maxLength: 30));
            AlterColumn("dbo.Feedback", "DateCreate", c => c.String(maxLength: 20));
            AlterColumn("dbo.News", "Name", c => c.String(maxLength: 120));
            AlterColumn("dbo.News", "CreatedAt", c => c.String(maxLength: 20));
            AlterColumn("dbo.News", "UpdateAt", c => c.String(maxLength: 20));
            AlterColumn("dbo.RentalBusOrder", "RentalBusId", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.RentalBusOrder", "DateBuy", c => c.String(maxLength: 20));
            AlterColumn("dbo.RentalBusOrder", "Bustype", c => c.String(maxLength: 20));
            AlterColumn("dbo.RentalBusOrder", "Departure", c => c.String(maxLength: 70));
            AlterColumn("dbo.RentalBusOrder", "Stoplocation", c => c.String(maxLength: 70));
            AlterColumn("dbo.RentalBusOrder", "GettingOfBus", c => c.String(maxLength: 70));
            AlterColumn("dbo.RentalBusOrder", "DepartureDay", c => c.String(maxLength: 70));
            AlterColumn("dbo.RentalBusOrder", "ReturnDay", c => c.String(maxLength: 70));
            AlterColumn("dbo.RentalBusOrder", "TotalMoney", c => c.String(maxLength: 50));
            AlterColumn("dbo.RentalBusOrder", "NameGuest", c => c.String(maxLength: 50));
            AlterColumn("dbo.RentalBusOrder", "AddGuest", c => c.String(maxLength: 400));
            AlterColumn("dbo.RentalBusOrder", "PhoneGuest", c => c.String(maxLength: 12));
            AlterColumn("dbo.RentalBusOrder", "EmailGuest", c => c.String(maxLength: 50));
            AddPrimaryKey("dbo.Directions", "BusId");
            AddPrimaryKey("dbo.PaymentMethod", "PaymentId");
            AddPrimaryKey("dbo.RentalBusOrder", "RentalBusId");
            CreateIndex("dbo.SeatInformation", "BusId");
            CreateIndex("dbo.TicketBooking", "BusId");
            CreateIndex("dbo.TicketBooking", "PaymentMethodId");
            AddForeignKey("dbo.SeatInformation", "BusId", "dbo.Directions", "BusId");
            AddForeignKey("dbo.TicketBooking", "BusId", "dbo.Directions", "BusId");
            AddForeignKey("dbo.TicketBooking", "PaymentMethodId", "dbo.PaymentMethod", "PaymentId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TicketBooking", "PaymentMethodId", "dbo.PaymentMethod");
            DropForeignKey("dbo.TicketBooking", "BusId", "dbo.Directions");
            DropForeignKey("dbo.SeatInformation", "BusId", "dbo.Directions");
            DropIndex("dbo.TicketBooking", new[] { "PaymentMethodId" });
            DropIndex("dbo.TicketBooking", new[] { "BusId" });
            DropIndex("dbo.SeatInformation", new[] { "BusId" });
            DropPrimaryKey("dbo.RentalBusOrder");
            DropPrimaryKey("dbo.PaymentMethod");
            DropPrimaryKey("dbo.Directions");
            AlterColumn("dbo.RentalBusOrder", "EmailGuest", c => c.String());
            AlterColumn("dbo.RentalBusOrder", "PhoneGuest", c => c.String());
            AlterColumn("dbo.RentalBusOrder", "AddGuest", c => c.String());
            AlterColumn("dbo.RentalBusOrder", "NameGuest", c => c.String());
            AlterColumn("dbo.RentalBusOrder", "TotalMoney", c => c.String());
            AlterColumn("dbo.RentalBusOrder", "ReturnDay", c => c.String());
            AlterColumn("dbo.RentalBusOrder", "DepartureDay", c => c.String());
            AlterColumn("dbo.RentalBusOrder", "GettingOfBus", c => c.String());
            AlterColumn("dbo.RentalBusOrder", "Stoplocation", c => c.String());
            AlterColumn("dbo.RentalBusOrder", "Departure", c => c.String());
            AlterColumn("dbo.RentalBusOrder", "Bustype", c => c.String());
            AlterColumn("dbo.RentalBusOrder", "DateBuy", c => c.String());
            AlterColumn("dbo.RentalBusOrder", "RentalBusId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.News", "UpdateAt", c => c.String());
            AlterColumn("dbo.News", "CreatedAt", c => c.String());
            AlterColumn("dbo.News", "Name", c => c.String());
            AlterColumn("dbo.Feedback", "DateCreate", c => c.String());
            AlterColumn("dbo.PaymentStatus", "PaymentStatusName", c => c.String());
            AlterColumn("dbo.PaymentMethod", "Payment", c => c.String(nullable: false));
            AlterColumn("dbo.PaymentMethod", "PaymentId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.TicketBooking", "ConfimDelivery", c => c.String());
            AlterColumn("dbo.TicketBooking", "DateBuy", c => c.String());
            AlterColumn("dbo.TicketBooking", "PaymentMethodId", c => c.String(maxLength: 128));
            AlterColumn("dbo.TicketBooking", "BusId", c => c.String(maxLength: 30));
            AlterColumn("dbo.SeatInformation", "CommentAdmin", c => c.String());
            AlterColumn("dbo.SeatInformation", "BusId", c => c.String(maxLength: 30));
            AlterColumn("dbo.Directions", "StopLocation6", c => c.String());
            AlterColumn("dbo.Directions", "StopLocation5", c => c.String());
            AlterColumn("dbo.Directions", "StopLocation4", c => c.String());
            AlterColumn("dbo.Directions", "StopLocation3", c => c.String());
            AlterColumn("dbo.Directions", "StopLocation2", c => c.String());
            AlterColumn("dbo.Directions", "StopLocation1", c => c.String());
            AlterColumn("dbo.Directions", "RoadName", c => c.String(nullable: false));
            AlterColumn("dbo.Directions", "BusId", c => c.String(nullable: false, maxLength: 30));
            AddPrimaryKey("dbo.RentalBusOrder", "RentalBusId");
            AddPrimaryKey("dbo.PaymentMethod", "PaymentId");
            AddPrimaryKey("dbo.Directions", "BusId");
            CreateIndex("dbo.TicketBooking", "PaymentMethodId");
            CreateIndex("dbo.TicketBooking", "BusId");
            CreateIndex("dbo.SeatInformation", "BusId");
            AddForeignKey("dbo.TicketBooking", "PaymentMethodId", "dbo.PaymentMethod", "PaymentId");
            AddForeignKey("dbo.TicketBooking", "BusId", "dbo.Directions", "BusId");
            AddForeignKey("dbo.SeatInformation", "BusId", "dbo.Directions", "BusId");
        }
    }
}
