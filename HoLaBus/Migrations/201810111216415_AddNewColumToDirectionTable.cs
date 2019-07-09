namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewColumToDirectionTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Directions", "BusStatus", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Directions", "BusStatus");
        }
    }
}
