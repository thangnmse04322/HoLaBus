namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeNameColumToTableTicketBooking : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TicketBooking", "StopLocationNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TicketBooking", "StopLocationNumber", c => c.Int(nullable: false));
        }
    }
}
