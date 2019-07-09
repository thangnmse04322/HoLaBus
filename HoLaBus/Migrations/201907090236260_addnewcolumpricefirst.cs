namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnewcolumpricefirst : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Directions", "PriceFirst", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Directions", "PriceFirst");
        }
    }
}
