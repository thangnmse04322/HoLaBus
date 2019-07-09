namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddnewFeeback2Time : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Feedback",
                c => new
                    {
                        FeedbackId = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        FeedbackUser = c.String(),
                        FeedbackAdmin = c.String(),
                    })
                .PrimaryKey(t => t.FeedbackId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Feedback", "UserId", "dbo.Users");
            DropIndex("dbo.Feedback", new[] { "UserId" });
            DropTable("dbo.Feedback");
        }
    }
}
