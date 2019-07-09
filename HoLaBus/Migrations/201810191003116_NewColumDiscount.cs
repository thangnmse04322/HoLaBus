namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewColumDiscount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TicketBooking", "Discount", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TicketBooking", "Discount");
        }
    }
}
