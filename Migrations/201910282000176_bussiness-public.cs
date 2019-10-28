namespace Appocal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bussinesspublic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Businesses", "Public", c => c.Boolean(nullable: false));
            AddColumn("dbo.Businesses", "ShortDescription", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Businesses", "ShortDescription");
            DropColumn("dbo.Businesses", "Public");
        }
    }
}
