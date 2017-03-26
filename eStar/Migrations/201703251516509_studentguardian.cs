namespace eStar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class studentguardian : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentGuardians",
                c => new
                    {
                        StudentGuardianID = c.Int(nullable: false, identity: true),
                        Student_User_ID = c.Int(nullable: false),
                        Guardian_User_ID = c.Int(nullable: false),
                        Guardians_User_ID = c.Int(),
                        Students_User_ID = c.Int(),
                    })
                .PrimaryKey(t => t.StudentGuardianID)
                .ForeignKey("dbo.Accounts", t => t.Guardians_User_ID)
                .ForeignKey("dbo.Accounts", t => t.Students_User_ID)
                .Index(t => t.Guardians_User_ID)
                .Index(t => t.Students_User_ID);
            
            AddColumn("dbo.Accounts", "StudentCount", c => c.Int());
            AddColumn("dbo.Accounts", "GuardianCount", c => c.Int());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentGuardians", "Students_User_ID", "dbo.Accounts");
            DropForeignKey("dbo.StudentGuardians", "Guardians_User_ID", "dbo.Accounts");
            DropIndex("dbo.StudentGuardians", new[] { "Students_User_ID" });
            DropIndex("dbo.StudentGuardians", new[] { "Guardians_User_ID" });
            DropColumn("dbo.Accounts", "GuardianCount");
            DropColumn("dbo.Accounts", "StudentCount");
            DropTable("dbo.StudentGuardians");
        }
    }
}
