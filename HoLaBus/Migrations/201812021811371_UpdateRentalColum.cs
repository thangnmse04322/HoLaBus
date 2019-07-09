namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRentalColum : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RentalBusOrder", "Departure", c => c.String(maxLength: 200));
            AlterColumn("dbo.RentalBusOrder", "Stoplocation", c => c.String(maxLength: 200));
            AlterColumn("dbo.RentalBusOrder", "GettingOfBus", c => c.String(maxLength: 200));
            AlterColumn("dbo.RentalBusOrder", "DepartureDay", c => c.String(maxLength: 100));
            AlterColumn("dbo.RentalBusOrder", "ReturnDay", c => c.String(maxLength: 100));
            AlterColumn("dbo.RentalBusOrder", "TotalMoney", c => c.String(maxLength: 100));
            AlterColumn("dbo.RentalBusOrder", "NameGuest", c => c.String(maxLength: 70));
            AlterColumn("dbo.RentalBusOrder", "PhoneGuest", c => c.String(maxLength: 12));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RentalBusOrder", "PhoneGuest", c => c.String(maxLength: 25));
            AlterColumn("dbo.RentalBusOrder", "NameGuest", c => c.String(maxLength: 50));
            AlterColumn("dbo.RentalBusOrder", "TotalMoney", c => c.String(maxLength: 50));
            AlterColumn("dbo.RentalBusOrder", "ReturnDay", c => c.String(maxLength: 70));
            AlterColumn("dbo.RentalBusOrder", "DepartureDay", c => c.String(maxLength: 70));
            AlterColumn("dbo.RentalBusOrder", "GettingOfBus", c => c.String(maxLength: 70));
            AlterColumn("dbo.RentalBusOrder", "Stoplocation", c => c.String(maxLength: 70));
            AlterColumn("dbo.RentalBusOrder", "Departure", c => c.String(maxLength: 70));
        }
    }
}
