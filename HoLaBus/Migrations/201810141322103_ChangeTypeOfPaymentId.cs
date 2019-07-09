namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTypeOfPaymentId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TicketBooking", "PaymentMethodId", "dbo.PaymentMethod");
            DropIndex("dbo.TicketBooking", new[] { "PaymentMethodId" });
            DropPrimaryKey("dbo.PaymentMethod");
            AlterColumn("dbo.PaymentMethod", "PaymentId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.TicketBooking", "PaymentMethodId", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.PaymentMethod", "PaymentId");
            CreateIndex("dbo.TicketBooking", "PaymentMethodId");
            AddForeignKey("dbo.TicketBooking", "PaymentMethodId", "dbo.PaymentMethod", "PaymentId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TicketBooking", "PaymentMethodId", "dbo.PaymentMethod");
            DropIndex("dbo.TicketBooking", new[] { "PaymentMethodId" });
            DropPrimaryKey("dbo.PaymentMethod");
            AlterColumn("dbo.TicketBooking", "PaymentMethodId", c => c.Int(nullable: false));
            AlterColumn("dbo.PaymentMethod", "PaymentId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.PaymentMethod", "PaymentId");
            CreateIndex("dbo.TicketBooking", "PaymentMethodId");
            AddForeignKey("dbo.TicketBooking", "PaymentMethodId", "dbo.PaymentMethod", "PaymentId", cascadeDelete: true);
        }
    }
}
