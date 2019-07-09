namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewTotalMoneyToRentalTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RentalBusOrder", "TotalMoney", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RentalBusOrder", "TotalMoney");
        }
    }
}
