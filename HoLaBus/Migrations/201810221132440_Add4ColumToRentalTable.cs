namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add4ColumToRentalTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RentalBusOrder", "NameGuest", c => c.String());
            AddColumn("dbo.RentalBusOrder", "AddGuest", c => c.String());
            AddColumn("dbo.RentalBusOrder", "PhoneGuest", c => c.String());
            AddColumn("dbo.RentalBusOrder", "EmailGuest", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RentalBusOrder", "EmailGuest");
            DropColumn("dbo.RentalBusOrder", "PhoneGuest");
            DropColumn("dbo.RentalBusOrder", "AddGuest");
            DropColumn("dbo.RentalBusOrder", "NameGuest");
        }
    }
}
