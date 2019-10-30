namespace Appocal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class messagebox : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "MessageBox_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "MessageBox_Id");
            AddForeignKey("dbo.AspNetUsers", "MessageBox_Id", "dbo.MessageBoxes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "MessageBox_Id", "dbo.MessageBoxes");
            DropIndex("dbo.AspNetUsers", new[] { "MessageBox_Id" });
            DropColumn("dbo.AspNetUsers", "MessageBox_Id");
        }
    }
}
