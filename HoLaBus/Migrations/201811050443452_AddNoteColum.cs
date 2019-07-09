namespace HoLaBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNoteColum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TicketBooking", "NoteAdmin", c => c.String(maxLength: 400));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TicketBooking", "NoteAdmin");
        }
    }
}
