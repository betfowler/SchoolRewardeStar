namespace eStar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class enrolment2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Enrolments", "Student_User_ID", "dbo.Accounts");
            DropIndex("dbo.Enrolments", new[] { "Student_User_ID" });
            AddColumn("dbo.Enrolments", "Student_User_ID1", c => c.Int());
            AlterColumn("dbo.Enrolments", "Student_User_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.Enrolments", "Student_User_ID1");
            AddForeignKey("dbo.Enrolments", "Student_User_ID1", "dbo.Accounts", "User_ID");
            DropColumn("dbo.Enrolments", "Student_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Enrolments", "Student_ID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Enrolments", "Student_User_ID1", "dbo.Accounts");
            DropIndex("dbo.Enrolments", new[] { "Student_User_ID1" });
            AlterColumn("dbo.Enrolments", "Student_User_ID", c => c.Int());
            DropColumn("dbo.Enrolments", "Student_User_ID1");
            CreateIndex("dbo.Enrolments", "Student_User_ID");
            AddForeignKey("dbo.Enrolments", "Student_User_ID", "dbo.Accounts", "User_ID");
        }
    }
}
