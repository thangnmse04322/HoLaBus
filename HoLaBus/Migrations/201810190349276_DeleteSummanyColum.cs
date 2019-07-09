namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteSummanyColum : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.News", "Summary");
        }
        
        public override void Down()
        {
            AddColumn("dbo.News", "Summary", c => c.String());
        }
    }
}
