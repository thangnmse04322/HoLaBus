namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewColumDiscountValue : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Directions", "DiscountValue", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Directions", "DiscountValue");
        }
    }
}
