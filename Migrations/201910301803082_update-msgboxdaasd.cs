namespace Appocal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatemsgboxdaasd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Conversations", "SeenBy1", c => c.Boolean(nullable: false));
            AddColumn("dbo.Conversations", "SeenBy2", c => c.Boolean(nullable: false));
            DropColumn("dbo.Conversations", "WasSeen");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Conversations", "WasSeen", c => c.Boolean(nullable: false));
            DropColumn("dbo.Conversations", "SeenBy2");
            DropColumn("dbo.Conversations", "SeenBy1");
        }
    }
}
