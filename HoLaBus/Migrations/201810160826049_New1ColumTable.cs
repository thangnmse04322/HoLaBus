namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New1ColumTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TicketBooking", "UserName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TicketBooking", "UserName");
        }
    }
}
