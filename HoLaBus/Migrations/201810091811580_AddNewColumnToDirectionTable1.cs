namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewColumnToDirectionTable1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Directions", "StopLocation6", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Directions", "StopLocation6");
        }
    }
}
