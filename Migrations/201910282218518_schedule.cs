namespace Appocal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class schedule : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Appointments", "Schedule_Id", "dbo.Schedules");
            DropIndex("dbo.Appointments", new[] { "Schedule_Id" });
            CreateTable(
                "dbo.ScheduleAppointments",
                c => new
                    {
                        Schedule_Id = c.Int(nullable: false),
                        Appointment_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Schedule_Id, t.Appointment_Id })
                .ForeignKey("dbo.Schedules", t => t.Schedule_Id, cascadeDelete: true)
                .ForeignKey("dbo.Appointments", t => t.Appointment_Id, cascadeDelete: true)
                .Index(t => t.Schedule_Id)
                .Index(t => t.Appointment_Id);
            
            DropColumn("dbo.Appointments", "Schedule_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Appointments", "Schedule_Id", c => c.Int());
            DropForeignKey("dbo.ScheduleAppointments", "Appointment_Id", "dbo.Appointments");
            DropForeignKey("dbo.ScheduleAppointments", "Schedule_Id", "dbo.Schedules");
            DropIndex("dbo.ScheduleAppointments", new[] { "Appointment_Id" });
            DropIndex("dbo.ScheduleAppointments", new[] { "Schedule_Id" });
            DropTable("dbo.ScheduleAppointments");
            CreateIndex("dbo.Appointments", "Schedule_Id");
            AddForeignKey("dbo.Appointments", "Schedule_Id", "dbo.Schedules", "Id");
        }
    }
}
