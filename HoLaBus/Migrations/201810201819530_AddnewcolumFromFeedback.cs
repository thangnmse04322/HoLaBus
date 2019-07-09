namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddnewcolumFromFeedback : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Feedback", "DateCreate", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Feedback", "DateCreate");
        }
    }
}
