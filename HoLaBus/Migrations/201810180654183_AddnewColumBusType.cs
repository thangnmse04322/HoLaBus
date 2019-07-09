namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddnewColumBusType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RentalBusOrder", "Bustype", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RentalBusOrder", "Bustype");
        }
    }
}
