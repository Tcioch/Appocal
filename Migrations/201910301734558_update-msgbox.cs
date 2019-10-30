namespace Appocal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatemsgbox : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Conversations", "MessageBox_Id", "dbo.MessageBoxes");
            DropForeignKey("dbo.Conversations", "User1_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Conversations", "User2_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Conversations", new[] { "MessageBox_Id" });
            DropIndex("dbo.Conversations", new[] { "User1_Id" });
            DropIndex("dbo.Conversations", new[] { "User2_Id" });
            CreateTable(
                "dbo.MessageBoxConversations",
                c => new
                    {
                        MessageBox_Id = c.Int(nullable: false),
                        Conversation_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MessageBox_Id, t.Conversation_Id })
                .ForeignKey("dbo.MessageBoxes", t => t.MessageBox_Id, cascadeDelete: true)
                .ForeignKey("dbo.Conversations", t => t.Conversation_Id, cascadeDelete: true)
                .Index(t => t.MessageBox_Id)
                .Index(t => t.Conversation_Id);
            
            AddColumn("dbo.Conversations", "WasSeen", c => c.Boolean(nullable: false));
            AddColumn("dbo.Messages", "User_Id_1", c => c.String());
            AddColumn("dbo.Messages", "User_Id_2", c => c.String());
            DropColumn("dbo.Conversations", "MessageBox_Id");
            DropColumn("dbo.Conversations", "User1_Id");
            DropColumn("dbo.Conversations", "User2_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Conversations", "User2_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Conversations", "User1_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Conversations", "MessageBox_Id", c => c.Int());
            DropForeignKey("dbo.MessageBoxConversations", "Conversation_Id", "dbo.Conversations");
            DropForeignKey("dbo.MessageBoxConversations", "MessageBox_Id", "dbo.MessageBoxes");
            DropIndex("dbo.MessageBoxConversations", new[] { "Conversation_Id" });
            DropIndex("dbo.MessageBoxConversations", new[] { "MessageBox_Id" });
            DropColumn("dbo.Messages", "User_Id_2");
            DropColumn("dbo.Messages", "User_Id_1");
            DropColumn("dbo.Conversations", "WasSeen");
            DropTable("dbo.MessageBoxConversations");
            CreateIndex("dbo.Conversations", "User2_Id");
            CreateIndex("dbo.Conversations", "User1_Id");
            CreateIndex("dbo.Conversations", "MessageBox_Id");
            AddForeignKey("dbo.Conversations", "User2_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Conversations", "User1_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Conversations", "MessageBox_Id", "dbo.MessageBoxes", "Id");
        }
    }
}
