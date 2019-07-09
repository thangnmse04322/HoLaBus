namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add3ColumToTicket : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TicketBooking", "Confim", c => c.Boolean(nullable: false));
            AddColumn("dbo.TicketBooking", "SeatName", c => c.String());
            AddColumn("dbo.TicketBooking", "DateBuy", c => c.String());
            AddColumn("dbo.TicketBooking", "ConfimDelivery", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TicketBooking", "ConfimDelivery");
            DropColumn("dbo.TicketBooking", "DateBuy");
            DropColumn("dbo.TicketBooking", "SeatName");
            DropColumn("dbo.TicketBooking", "Confim");
        }
    }
}
