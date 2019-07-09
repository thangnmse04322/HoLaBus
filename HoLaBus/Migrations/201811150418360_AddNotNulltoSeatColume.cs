namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNotNulltoSeatColume : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SeatInformation", "CommentAdmin", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SeatInformation", "CommentAdmin", c => c.String(maxLength: 50));
        }
    }
}
