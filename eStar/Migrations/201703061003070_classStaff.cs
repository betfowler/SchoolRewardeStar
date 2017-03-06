namespace eStar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class classStaff : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ClassStaffs", "Staff_User_ID", "dbo.Accounts");
            DropForeignKey("dbo.Enrolments", "Student_User_ID1", "dbo.Accounts");
            DropForeignKey("dbo.ClassStaffs", "Class_Class_ID", "dbo.Classes");
            DropIndex("dbo.Enrolments", new[] { "Student_User_ID1" });
            DropIndex("dbo.ClassStaffs", new[] { "Class_Class_ID" });
            DropIndex("dbo.ClassStaffs", new[] { "Staff_User_ID" });
            RenameColumn(table: "dbo.ClassStaffs", name: "Staff_User_ID", newName: "User_ID");
            RenameColumn(table: "dbo.Enrolments", name: "Student_User_ID1", newName: "User_ID");
            RenameColumn(table: "dbo.ClassStaffs", name: "Class_Class_ID", newName: "Class_ID");
            AlterColumn("dbo.Enrolments", "User_ID", c => c.Int(nullable: false));
            AlterColumn("dbo.ClassStaffs", "Class_ID", c => c.Int(nullable: false));
            AlterColumn("dbo.ClassStaffs", "User_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.Enrolments", "User_ID");
            CreateIndex("dbo.ClassStaffs", "Class_ID");
            CreateIndex("dbo.ClassStaffs", "User_ID");
            AddForeignKey("dbo.ClassStaffs", "User_ID", "dbo.Accounts", "User_ID", cascadeDelete: true);
            AddForeignKey("dbo.Enrolments", "User_ID", "dbo.Accounts", "User_ID", cascadeDelete: true);
            AddForeignKey("dbo.ClassStaffs", "Class_ID", "dbo.Classes", "Class_ID", cascadeDelete: true);
            DropColumn("dbo.Enrolments", "Student_User_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Enrolments", "Student_User_ID", c => c.Int(nullable: false));
            DropForeignKey("dbo.ClassStaffs", "Class_ID", "dbo.Classes");
            DropForeignKey("dbo.Enrolments", "User_ID", "dbo.Accounts");
            DropForeignKey("dbo.ClassStaffs", "User_ID", "dbo.Accounts");
            DropIndex("dbo.ClassStaffs", new[] { "User_ID" });
            DropIndex("dbo.ClassStaffs", new[] { "Class_ID" });
            DropIndex("dbo.Enrolments", new[] { "User_ID" });
            AlterColumn("dbo.ClassStaffs", "User_ID", c => c.Int());
            AlterColumn("dbo.ClassStaffs", "Class_ID", c => c.Int());
            AlterColumn("dbo.Enrolments", "User_ID", c => c.Int());
            RenameColumn(table: "dbo.ClassStaffs", name: "Class_ID", newName: "Class_Class_ID");
            RenameColumn(table: "dbo.Enrolments", name: "User_ID", newName: "Student_User_ID1");
            RenameColumn(table: "dbo.ClassStaffs", name: "User_ID", newName: "Staff_User_ID");
            CreateIndex("dbo.ClassStaffs", "Staff_User_ID");
            CreateIndex("dbo.ClassStaffs", "Class_Class_ID");
            CreateIndex("dbo.Enrolments", "Student_User_ID1");
            AddForeignKey("dbo.ClassStaffs", "Class_Class_ID", "dbo.Classes", "Class_ID");
            AddForeignKey("dbo.Enrolments", "Student_User_ID1", "dbo.Accounts", "User_ID");
            AddForeignKey("dbo.ClassStaffs", "Staff_User_ID", "dbo.Accounts", "User_ID");
        }
    }
}
