namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateApplicationUserTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Address", c => c.String());
            AddColumn("dbo.Users", "FullName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "FullName");
            DropColumn("dbo.Users", "Address");
        }
    }
}
