namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateErroEmailGuest : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RentalBusOrder", "EmailGuest", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RentalBusOrder", "EmailGuest", c => c.String(maxLength: 50));
        }
    }
}
