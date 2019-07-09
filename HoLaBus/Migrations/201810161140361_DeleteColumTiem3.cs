namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteColumTiem3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.TicketBooking", "PaymentMethodName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TicketBooking", "PaymentMethodName", c => c.String());
        }
    }
}
