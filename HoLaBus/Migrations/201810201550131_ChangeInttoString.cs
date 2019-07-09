namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeInttoString : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SeatInformation", "BusId", "dbo.Directions");
            DropForeignKey("dbo.TicketBooking", "BusId", "dbo.Directions");
            DropIndex("dbo.SeatInformation", new[] { "BusId" });
            DropIndex("dbo.TicketBooking", new[] { "BusId" });
            DropPrimaryKey("dbo.Directions");
            AlterColumn("dbo.Directions", "BusId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.SeatInformation", "BusId", c => c.String(maxLength: 128));
            AlterColumn("dbo.TicketBooking", "BusId", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.Directions", "BusId");
            CreateIndex("dbo.SeatInformation", "BusId");
            CreateIndex("dbo.TicketBooking", "BusId");
            AddForeignKey("dbo.SeatInformation", "BusId", "dbo.Directions", "BusId");
            AddForeignKey("dbo.TicketBooking", "BusId", "dbo.Directions", "BusId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TicketBooking", "BusId", "dbo.Directions");
            DropForeignKey("dbo.SeatInformation", "BusId", "dbo.Directions");
            DropIndex("dbo.TicketBooking", new[] { "BusId" });
            DropIndex("dbo.SeatInformation", new[] { "BusId" });
            DropPrimaryKey("dbo.Directions");
            AlterColumn("dbo.TicketBooking", "BusId", c => c.Int(nullable: false));
            AlterColumn("dbo.SeatInformation", "BusId", c => c.Int(nullable: false));
            AlterColumn("dbo.Directions", "BusId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Directions", "BusId");
            CreateIndex("dbo.TicketBooking", "BusId");
            CreateIndex("dbo.SeatInformation", "BusId");
            AddForeignKey("dbo.TicketBooking", "BusId", "dbo.Directions", "BusId", cascadeDelete: true);
            AddForeignKey("dbo.SeatInformation", "BusId", "dbo.Directions", "BusId", cascadeDelete: true);
        }
    }
}
