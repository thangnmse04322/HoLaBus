namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDataTypeVer2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SeatInformation", "BusName", c => c.String(maxLength: 70));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SeatInformation", "BusName", c => c.String());
        }
    }
}
