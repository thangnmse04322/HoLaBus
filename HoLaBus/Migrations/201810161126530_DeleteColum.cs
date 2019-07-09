namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteColum : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.TicketBooking", "PaymentStatusName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TicketBooking", "PaymentStatusName", c => c.String());
        }
    }
}
