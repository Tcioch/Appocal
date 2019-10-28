namespace Appocal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Servies : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Duration = c.Int(nullable: false),
                        Business_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Businesses", t => t.Business_Id)
                .Index(t => t.Business_Id);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Services", "Business_Id", "dbo.Businesses");
            DropIndex("dbo.Services", new[] { "Business_Id" });
            DropTable("dbo.Services");
        }
    }
}
