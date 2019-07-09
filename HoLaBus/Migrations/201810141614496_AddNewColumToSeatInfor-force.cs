namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewColumToSeatInforforce : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SeatInformation", "BusName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SeatInformation", "BusName");
        }
    }
}
