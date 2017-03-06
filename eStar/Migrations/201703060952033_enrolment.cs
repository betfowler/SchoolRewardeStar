namespace eStar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class enrolment : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Enrolments", "Class_Class_ID", "dbo.Classes");
            DropIndex("dbo.Enrolments", new[] { "Class_Class_ID" });
            RenameColumn(table: "dbo.Enrolments", name: "Class_Class_ID", newName: "Class_ID");
            AddColumn("dbo.Enrolments", "Student_ID", c => c.Int(nullable: false));
            AlterColumn("dbo.Enrolments", "Class_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.Enrolments", "Class_ID");
            AddForeignKey("dbo.Enrolments", "Class_ID", "dbo.Classes", "Class_ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Enrolments", "Class_ID", "dbo.Classes");
            DropIndex("dbo.Enrolments", new[] { "Class_ID" });
            AlterColumn("dbo.Enrolments", "Class_ID", c => c.Int());
            DropColumn("dbo.Enrolments", "Student_ID");
            RenameColumn(table: "dbo.Enrolments", name: "Class_ID", newName: "Class_Class_ID");
            CreateIndex("dbo.Enrolments", "Class_Class_ID");
            AddForeignKey("dbo.Enrolments", "Class_Class_ID", "dbo.Classes", "Class_ID");
        }
    }
}
