namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PaymentMethod",
                c => new
                    {
                        PaymentId = c.String(nullable: false, maxLength: 128),
                        Payment = c.String(nullable: false),
                        Monney = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PaymentId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PaymentMethod");
        }
    }
}
