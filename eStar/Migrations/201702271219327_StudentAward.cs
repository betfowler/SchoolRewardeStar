namespace eStar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentAward : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentAwards",
                c => new
                    {
                        StudentAward_ID = c.Int(nullable: false, identity: true),
                        Student_ID = c.Int(nullable: false),
                        Award_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StudentAward_ID);
            
            DropColumn("dbo.Awards", "Student_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Awards", "Student_ID", c => c.Int(nullable: false));
            DropTable("dbo.StudentAwards");
        }
    }
}
