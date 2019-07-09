namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDatimeToRentalTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RentalBusOrder", "DepartureDay", c => c.String());
            AlterColumn("dbo.RentalBusOrder", "ReturnDay", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RentalBusOrder", "ReturnDay", c => c.DateTime(nullable: false));
            AlterColumn("dbo.RentalBusOrder", "DepartureDay", c => c.DateTime(nullable: false));
        }
    }
}
