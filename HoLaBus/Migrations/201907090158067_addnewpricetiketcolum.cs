namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnewpricetiketcolum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TicketBooking", "PriceTicket", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TicketBooking", "PriceTicket");
        }
    }
}
