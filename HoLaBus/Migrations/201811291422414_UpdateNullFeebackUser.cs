namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateNullFeebackUser : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Feedback", "FeedbackUser", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Feedback", "FeedbackUser", c => c.String());
        }
    }
}
