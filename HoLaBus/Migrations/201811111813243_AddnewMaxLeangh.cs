namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddnewMaxLeangh : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.News", "AuthorName", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.News", "AuthorName", c => c.String());
        }
    }
}
