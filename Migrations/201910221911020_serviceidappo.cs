namespace Appocal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class serviceidappo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Appointments", "Service_Id", "dbo.Services");
            DropIndex("dbo.Appointments", new[] { "Service_Id" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Appointments", "Service_Id");
            AddForeignKey("dbo.Appointments", "Service_Id", "dbo.Services", "Id");
        }
    }
}
