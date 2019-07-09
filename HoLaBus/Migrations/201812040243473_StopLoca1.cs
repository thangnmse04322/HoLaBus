namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StopLoca1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Directions", "StopLocation1", c => c.String(nullable: false, maxLength: 70));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Directions", "StopLocation1", c => c.String(maxLength: 70));
        }
    }
}
