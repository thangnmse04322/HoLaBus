namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOneColumToTicketTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TicketBooking", "BusName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TicketBooking", "BusName");
        }
    }
}
