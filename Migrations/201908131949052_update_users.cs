namespace Appocal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_users : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Business_Id", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Schedule_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Business_Id");
            CreateIndex("dbo.AspNetUsers", "Schedule_Id");
            AddForeignKey("dbo.AspNetUsers", "Business_Id", "dbo.Businesses", "Id");
            AddForeignKey("dbo.AspNetUsers", "Schedule_Id", "dbo.Schedules", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Schedule_Id", "dbo.Schedules");
            DropForeignKey("dbo.AspNetUsers", "Business_Id", "dbo.Businesses");
            DropIndex("dbo.AspNetUsers", new[] { "Schedule_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Business_Id" });
            DropColumn("dbo.AspNetUsers", "Schedule_Id");
            DropColumn("dbo.AspNetUsers", "Business_Id");
        }
    }
}
