namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnewcolum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Directions", "BusStatus", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Directions", "BusStatus");
        }
    }
}
