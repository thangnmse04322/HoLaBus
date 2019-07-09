namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLenght100FromEmailGuest : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RentalBusOrder", "EmailGuest", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RentalBusOrder", "EmailGuest", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
