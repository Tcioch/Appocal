namespace Appocal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class activeService : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Services", "Active", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Services", "Active");
        }
    }
}
