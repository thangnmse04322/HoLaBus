namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddneuDatemit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RentalBusOrder", "DateBuy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RentalBusOrder", "DateBuy");
        }
    }
}
