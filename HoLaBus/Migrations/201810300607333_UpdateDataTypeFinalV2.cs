namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDataTypeFinalV2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SeatInformation", "SeatName", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SeatInformation", "SeatName", c => c.String());
        }
    }
}
