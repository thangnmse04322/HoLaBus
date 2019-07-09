namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewGuestNaemToFeedback : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Feedback", "GuestName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Feedback", "GuestName");
        }
    }
}
