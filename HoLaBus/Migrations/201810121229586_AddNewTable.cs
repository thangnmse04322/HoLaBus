namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Directions", "TicketPrice", c => c.Double(nullable: false));
            AlterColumn("dbo.PaymentMethod", "Monney", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PaymentMethod", "Monney", c => c.Int(nullable: false));
            AlterColumn("dbo.Directions", "TicketPrice", c => c.String(nullable: false));
        }
    }
}
