namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDatimee : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.News", "CreatedAt", c => c.String());
            AlterColumn("dbo.News", "UpdateAt", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.News", "UpdateAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.News", "CreatedAt", c => c.DateTime(nullable: false));
        }
    }
}
