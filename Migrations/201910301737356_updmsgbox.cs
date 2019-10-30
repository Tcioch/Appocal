namespace Appocal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updmsgbox : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Conversations", "User1", c => c.String());
            AddColumn("dbo.Conversations", "User2", c => c.String());
            AddColumn("dbo.Messages", "Sender", c => c.String());
            AddColumn("dbo.Messages", "Receiver", c => c.String());
            DropColumn("dbo.Messages", "User_Id_1");
            DropColumn("dbo.Messages", "User_Id_2");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Messages", "User_Id_2", c => c.String());
            AddColumn("dbo.Messages", "User_Id_1", c => c.String());
            DropColumn("dbo.Messages", "Receiver");
            DropColumn("dbo.Messages", "Sender");
            DropColumn("dbo.Conversations", "User2");
            DropColumn("dbo.Conversations", "User1");
        }
    }
}
