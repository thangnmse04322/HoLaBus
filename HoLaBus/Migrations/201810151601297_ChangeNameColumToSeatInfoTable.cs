namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeNameColumToSeatInfoTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SeatInformation", "CommentAdmin", c => c.String());
            DropColumn("dbo.SeatInformation", "comment");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SeatInformation", "comment", c => c.String());
            DropColumn("dbo.SeatInformation", "CommentAdmin");
        }
    }
}
