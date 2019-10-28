namespace Appocal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userID : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Appointments", "Client_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Appointments", new[] { "Client_Id" });
            AlterColumn("dbo.Appointments", "Client_Id", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Appointments", "Client_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Appointments", "Client_Id");
            AddForeignKey("dbo.Appointments", "Client_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
