namespace Appocal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_client_Id : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reviews", "Client_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Reviews", new[] { "Client_Id" });
            AlterColumn("dbo.Reviews", "Client_Id", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reviews", "Client_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Reviews", "Client_Id");
            AddForeignKey("dbo.Reviews", "Client_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
