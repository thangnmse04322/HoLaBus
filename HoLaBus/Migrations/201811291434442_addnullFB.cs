namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnullFB : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Feedback", "FeedbackUser", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Feedback", "FeedbackUser", c => c.String(nullable: false));
        }
    }
}
