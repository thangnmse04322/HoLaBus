namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletecolumvr2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.TicketBooking", "BusName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TicketBooking", "BusName", c => c.String());
        }
    }
}
