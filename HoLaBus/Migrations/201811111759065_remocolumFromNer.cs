namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remocolumFromNer : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.News", "AuthorName", c => c.String());
            DropColumn("dbo.News", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.News", "Image", c => c.String());
            AlterColumn("dbo.News", "AuthorName", c => c.String(maxLength: 50));
        }
    }
}
