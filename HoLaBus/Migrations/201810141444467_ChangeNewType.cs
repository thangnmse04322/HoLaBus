namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeNewType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SeatInformation", "SeatName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SeatInformation", "SeatName", c => c.Int(nullable: false));
        }
    }
}
