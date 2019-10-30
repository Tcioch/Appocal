namespace Appocal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "Business_Name", c => c.String());
            DropColumn("dbo.Appointments", "Business_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Appointments", "Business_Id", c => c.String());
            DropColumn("dbo.Appointments", "Business_Name");
        }
    }
}
