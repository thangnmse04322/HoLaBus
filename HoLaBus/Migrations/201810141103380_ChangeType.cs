namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Directions", "NumberTicket", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Directions", "NumberTicket", c => c.String(nullable: false));
        }
    }
}
