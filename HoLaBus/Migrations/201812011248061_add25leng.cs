namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add25leng : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RentalBusOrder", "PhoneGuest", c => c.String(maxLength: 25));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RentalBusOrder", "PhoneGuest", c => c.String(nullable: false, maxLength: 10));
        }
    }
}
