namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changetypeNameSeat : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TicketBooking", "SeatName", c => c.String(maxLength: 140));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TicketBooking", "SeatName", c => c.String(maxLength: 10));
        }
    }
}
