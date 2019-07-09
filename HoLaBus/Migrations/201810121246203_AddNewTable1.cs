namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewTable1 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.PaymentMethod");
            CreateTable(
                "dbo.PaymentStatus",
                c => new
                    {
                        PaymentStatusId = c.Int(nullable: false, identity: true),
                        PaymentStatusName = c.String(),
                    })
                .PrimaryKey(t => t.PaymentStatusId);
            
            CreateTable(
                "dbo.SeatInformation",
                c => new
                    {
                        SeatId = c.Int(nullable: false, identity: true),
                        SeatName = c.Int(nullable: false),
                        TicketId = c.Int(nullable: false),
                        BusId = c.String(maxLength: 128),
                        Status = c.Boolean(nullable: false),
                        TicketBooking_TicketId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.SeatId)
                .ForeignKey("dbo.Directions", t => t.BusId)
                .ForeignKey("dbo.TicketBooking", t => t.TicketBooking_TicketId)
                .Index(t => t.BusId)
                .Index(t => t.TicketBooking_TicketId);
            
            CreateTable(
                "dbo.TicketBooking",
                c => new
                    {
                        TicketId = c.String(nullable: false, maxLength: 128),
                        BusId = c.String(maxLength: 128),
                        UserId = c.String(maxLength: 128),
                        StopLocationNumber = c.Int(nullable: false),
                        TotalSeatNumber = c.Int(nullable: false),
                        PaymentMethodId = c.Int(nullable: false),
                        PaymentStatusId = c.Int(nullable: false),
                        DeliveryMoney = c.Double(nullable: false),
                        Total = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.TicketId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .ForeignKey("dbo.Directions", t => t.BusId)
                .ForeignKey("dbo.PaymentMethod", t => t.PaymentMethodId, cascadeDelete: true)
                .ForeignKey("dbo.PaymentStatus", t => t.PaymentStatusId, cascadeDelete: true)
                .Index(t => t.BusId)
                .Index(t => t.UserId)
                .Index(t => t.PaymentMethodId)
                .Index(t => t.PaymentStatusId);
            
            AlterColumn("dbo.PaymentMethod", "PaymentId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.PaymentMethod", "PaymentId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SeatInformation", "TicketBooking_TicketId", "dbo.TicketBooking");
            DropForeignKey("dbo.TicketBooking", "PaymentStatusId", "dbo.PaymentStatus");
            DropForeignKey("dbo.TicketBooking", "PaymentMethodId", "dbo.PaymentMethod");
            DropForeignKey("dbo.TicketBooking", "BusId", "dbo.Directions");
            DropForeignKey("dbo.TicketBooking", "UserId", "dbo.Users");
            DropForeignKey("dbo.SeatInformation", "BusId", "dbo.Directions");
            DropIndex("dbo.TicketBooking", new[] { "PaymentStatusId" });
            DropIndex("dbo.TicketBooking", new[] { "PaymentMethodId" });
            DropIndex("dbo.TicketBooking", new[] { "UserId" });
            DropIndex("dbo.TicketBooking", new[] { "BusId" });
            DropIndex("dbo.SeatInformation", new[] { "TicketBooking_TicketId" });
            DropIndex("dbo.SeatInformation", new[] { "BusId" });
            DropPrimaryKey("dbo.PaymentMethod");
            AlterColumn("dbo.PaymentMethod", "PaymentId", c => c.String(nullable: false, maxLength: 128));
            DropTable("dbo.TicketBooking");
            DropTable("dbo.SeatInformation");
            DropTable("dbo.PaymentStatus");
            AddPrimaryKey("dbo.PaymentMethod", "PaymentId");
        }
    }
}
