namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewColumComment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SeatInformation", "comment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SeatInformation", "comment");
        }
    }
}
