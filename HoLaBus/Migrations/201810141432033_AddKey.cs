namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddKey : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.SeatInformation", new[] { "TicketBooking_TicketId" });
            DropColumn("dbo.SeatInformation", "TicketId");
            RenameColumn(table: "dbo.SeatInformation", name: "TicketBooking_TicketId", newName: "TicketId");
            AlterColumn("dbo.SeatInformation", "TicketId", c => c.String(maxLength: 128));
            CreateIndex("dbo.SeatInformation", "TicketId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.SeatInformation", new[] { "TicketId" });
            AlterColumn("dbo.SeatInformation", "TicketId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.SeatInformation", name: "TicketId", newName: "TicketBooking_TicketId");
            AddColumn("dbo.SeatInformation", "TicketId", c => c.Int(nullable: false));
            CreateIndex("dbo.SeatInformation", "TicketBooking_TicketId");
        }
    }
}
