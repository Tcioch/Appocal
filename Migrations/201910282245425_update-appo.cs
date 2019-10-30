namespace Appocal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateappo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "Business_Id", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Appointments", "Business_Id");
        }
    }
}
