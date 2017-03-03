namespace eStar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class classEnrolment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Enrolments",
                c => new
                    {
                        Enrolment_ID = c.Int(nullable: false, identity: true),
                        Class_Class_ID = c.Int(),
                        Student_User_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Enrolment_ID)
                .ForeignKey("dbo.Classes", t => t.Class_Class_ID)
                .ForeignKey("dbo.Accounts", t => t.Student_User_ID)
                .Index(t => t.Class_Class_ID)
                .Index(t => t.Student_User_ID);
            
            CreateTable(
                "dbo.Classes",
                c => new
                    {
                        Class_ID = c.Int(nullable: false, identity: true),
                        Class_Name = c.String(),
                    })
                .PrimaryKey(t => t.Class_ID);
            
            CreateTable(
                "dbo.ClassStaffs",
                c => new
                    {
                        ClassStaff_ID = c.Int(nullable: false, identity: true),
                        Class_Class_ID = c.Int(),
                        Staff_User_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ClassStaff_ID)
                .ForeignKey("dbo.Classes", t => t.Class_Class_ID)
                .ForeignKey("dbo.Accounts", t => t.Staff_User_ID)
                .Index(t => t.Class_Class_ID)
                .Index(t => t.Staff_User_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Enrolments", "Student_User_ID", "dbo.Accounts");
            DropForeignKey("dbo.Enrolments", "Class_Class_ID", "dbo.Classes");
            DropForeignKey("dbo.ClassStaffs", "Staff_User_ID", "dbo.Accounts");
            DropForeignKey("dbo.ClassStaffs", "Class_Class_ID", "dbo.Classes");
            DropIndex("dbo.ClassStaffs", new[] { "Staff_User_ID" });
            DropIndex("dbo.ClassStaffs", new[] { "Class_Class_ID" });
            DropIndex("dbo.Enrolments", new[] { "Student_User_ID" });
            DropIndex("dbo.Enrolments", new[] { "Class_Class_ID" });
            DropTable("dbo.ClassStaffs");
            DropTable("dbo.Classes");
            DropTable("dbo.Enrolments");
        }
    }
}
