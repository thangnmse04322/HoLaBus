namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddtwoColumtoTicketTaclbe : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TicketBooking", "PaymentMethodName", c => c.String());
            AddColumn("dbo.TicketBooking", "PaymentStatusName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TicketBooking", "PaymentStatusName");
            DropColumn("dbo.TicketBooking", "PaymentMethodName");
        }
    }
}
