namespace Appocal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_models : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AppointmentDate = c.DateTime(nullable: false),
                        Duration = c.Int(nullable: false),
                        Available = c.Boolean(),
                        IsConfirmed = c.Boolean(nullable: false),
                        Client_Id = c.String(maxLength: 128),
                        Schedule_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Client_Id)
                .ForeignKey("dbo.Schedules", t => t.Schedule_Id)
                .Index(t => t.Client_Id)
                .Index(t => t.Schedule_Id);
            
            CreateTable(
                "dbo.Businesses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BusinessPage_Id = c.Int(),
                        Schedule_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BusinessPages", t => t.BusinessPage_Id)
                .ForeignKey("dbo.Schedules", t => t.Schedule_Id)
                .Index(t => t.BusinessPage_Id)
                .Index(t => t.Schedule_Id);
            
            CreateTable(
                "dbo.BusinessPages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PageContent_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PageContents", t => t.PageContent_Id)
                .Index(t => t.PageContent_Id);
            
            CreateTable(
                "dbo.PageContents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReviewDate = c.DateTime(nullable: false),
                        Rating = c.Single(nullable: false),
                        Contents = c.String(),
                        Client_Id = c.String(maxLength: 128),
                        Business_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Client_Id)
                .ForeignKey("dbo.Businesses", t => t.Business_Id)
                .Index(t => t.Client_Id)
                .Index(t => t.Business_Id);
            
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Conversations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        User1_Id = c.String(maxLength: 128),
                        User2_Id = c.String(maxLength: 128),
                        MessageBox_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User1_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User2_Id)
                .ForeignKey("dbo.MessageBoxes", t => t.MessageBox_Id)
                .Index(t => t.User1_Id)
                .Index(t => t.User2_Id)
                .Index(t => t.MessageBox_Id);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MessageDate = c.DateTime(nullable: false),
                        Contents = c.String(),
                        Conversation_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Conversations", t => t.Conversation_Id)
                .Index(t => t.Conversation_Id);
            
            CreateTable(
                "dbo.MessageBoxes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Conversations", "MessageBox_Id", "dbo.MessageBoxes");
            DropForeignKey("dbo.Conversations", "User2_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Conversations", "User1_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "Conversation_Id", "dbo.Conversations");
            DropForeignKey("dbo.Businesses", "Schedule_Id", "dbo.Schedules");
            DropForeignKey("dbo.Appointments", "Schedule_Id", "dbo.Schedules");
            DropForeignKey("dbo.Reviews", "Business_Id", "dbo.Businesses");
            DropForeignKey("dbo.Reviews", "Client_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Businesses", "BusinessPage_Id", "dbo.BusinessPages");
            DropForeignKey("dbo.BusinessPages", "PageContent_Id", "dbo.PageContents");
            DropForeignKey("dbo.Appointments", "Client_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Messages", new[] { "Conversation_Id" });
            DropIndex("dbo.Conversations", new[] { "MessageBox_Id" });
            DropIndex("dbo.Conversations", new[] { "User2_Id" });
            DropIndex("dbo.Conversations", new[] { "User1_Id" });
            DropIndex("dbo.Reviews", new[] { "Business_Id" });
            DropIndex("dbo.Reviews", new[] { "Client_Id" });
            DropIndex("dbo.BusinessPages", new[] { "PageContent_Id" });
            DropIndex("dbo.Businesses", new[] { "Schedule_Id" });
            DropIndex("dbo.Businesses", new[] { "BusinessPage_Id" });
            DropIndex("dbo.Appointments", new[] { "Schedule_Id" });
            DropIndex("dbo.Appointments", new[] { "Client_Id" });
            DropTable("dbo.MessageBoxes");
            DropTable("dbo.Messages");
            DropTable("dbo.Conversations");
            DropTable("dbo.Schedules");
            DropTable("dbo.Reviews");
            DropTable("dbo.PageContents");
            DropTable("dbo.BusinessPages");
            DropTable("dbo.Businesses");
            DropTable("dbo.Appointments");
        }
    }
}
