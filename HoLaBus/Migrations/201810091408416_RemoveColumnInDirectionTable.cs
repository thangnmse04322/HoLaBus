namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveColumnInDirectionTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Directions", "StopLocation6");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Directions", "StopLocation6", c => c.String());
        }
    }
}
