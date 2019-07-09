namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewRentalTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RentalBusOrder",
                c => new
                    {
                        RentalBusId = c.String(nullable: false, maxLength: 128),
                        UserID = c.String(maxLength: 128),
                        PaymentStatusId = c.Int(nullable: false),
                        Departure = c.String(),
                        Stoplocation = c.String(),
                        GettingOfBus = c.String(),
                        DepartureDay = c.DateTime(nullable: false),
                        ReturnDay = c.DateTime(nullable: false),
                        ConfimDelivery = c.String(),
                    })
                .PrimaryKey(t => t.RentalBusId)
                .ForeignKey("dbo.Users", t => t.UserID)
                .ForeignKey("dbo.PaymentStatus", t => t.PaymentStatusId, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.PaymentStatusId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RentalBusOrder", "PaymentStatusId", "dbo.PaymentStatus");
            DropForeignKey("dbo.RentalBusOrder", "UserID", "dbo.Users");
            DropIndex("dbo.RentalBusOrder", new[] { "PaymentStatusId" });
            DropIndex("dbo.RentalBusOrder", new[] { "UserID" });
            DropTable("dbo.RentalBusOrder");
        }
    }
}
