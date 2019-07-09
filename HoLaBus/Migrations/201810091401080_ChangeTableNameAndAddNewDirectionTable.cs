namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTableNameAndAddNewDirectionTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AspNetRoles", newName: "Roles");
            RenameTable(name: "dbo.AspNetUserRoles", newName: "UserRoles");
            RenameTable(name: "dbo.AspNetUsers", newName: "Users");
            RenameTable(name: "dbo.AspNetUserClaims", newName: "UserClaims");
            RenameTable(name: "dbo.AspNetUserLogins", newName: "UserLogins");
            CreateTable(
                "dbo.Directions",
                c => new
                    {
                        BusId = c.String(nullable: false, maxLength: 128),
                        RoadName = c.String(nullable: false),
                        TicketPrice = c.String(nullable: false),
                        NumberTicket = c.String(nullable: false),
                        StopLocation1 = c.String(),
                        StopLocation2 = c.String(),
                        StopLocation3 = c.String(),
                        StopLocation4 = c.String(),
                        StopLocation5 = c.String(),
                    })
                .PrimaryKey(t => t.BusId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Directions");
            RenameTable(name: "dbo.UserLogins", newName: "AspNetUserLogins");
            RenameTable(name: "dbo.UserClaims", newName: "AspNetUserClaims");
            RenameTable(name: "dbo.Users", newName: "AspNetUsers");
            RenameTable(name: "dbo.UserRoles", newName: "AspNetUserRoles");
            RenameTable(name: "dbo.Roles", newName: "AspNetRoles");
        }
    }
}
