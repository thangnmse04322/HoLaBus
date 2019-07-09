namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDateCheck : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TicketBooking", "DateCheck", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TicketBooking", "DateCheck");
        }
    }
}
