namespace Appocal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class apposervice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "service_Id", c => c.Int());
            CreateIndex("dbo.Appointments", "service_Id");
            AddForeignKey("dbo.Appointments", "service_Id", "dbo.Services", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "service_Id", "dbo.Services");
            DropIndex("dbo.Appointments", new[] { "service_Id" });
            DropColumn("dbo.Appointments", "service_Id");
        }
    }
}
