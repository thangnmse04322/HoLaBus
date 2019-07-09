namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeType200Maxleng : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TicketBooking", "SeatName", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TicketBooking", "SeatName", c => c.String(maxLength: 140));
        }
    }
}
