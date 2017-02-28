namespace eStar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Awards : DbMigration
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
                        Student_User_ID = c.Int(),
                    })
                .PrimaryKey(t => t.StudentAward_ID)
                .ForeignKey("dbo.Awards", t => t.Award_ID, cascadeDelete: true)
                .ForeignKey("dbo.Accounts", t => t.Student_User_ID)
                .Index(t => t.Award_ID)
                .Index(t => t.Student_User_ID);
            
            CreateTable(
                "dbo.Awards",
                c => new
                    {
                        Award_ID = c.Int(nullable: false, identity: true),
                        Staff_ID = c.Int(nullable: false),
                        Num_Points = c.Int(nullable: false),
                        Reward_Category_ID = c.Int(nullable: false),
                        Reward_Comment = c.String(),
                        RewardCategory_Reward_Category_ID = c.Int(),
                        Staff_User_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Award_ID)
                .ForeignKey("dbo.RewardCategories", t => t.RewardCategory_Reward_Category_ID)
                .ForeignKey("dbo.Accounts", t => t.Staff_User_ID)
                .Index(t => t.RewardCategory_Reward_Category_ID)
                .Index(t => t.Staff_User_ID);
            
            CreateTable(
                "dbo.RewardCategories",
                c => new
                    {
                        Reward_Category_ID = c.Int(nullable: false, identity: true),
                        Reward_Category = c.String(),
                        Award_Award_ID = c.Int(),
                        Award_Award_ID1 = c.Int(),
                    })
                .PrimaryKey(t => t.Reward_Category_ID)
                .ForeignKey("dbo.Awards", t => t.Award_Award_ID)
                .ForeignKey("dbo.Awards", t => t.Award_Award_ID1)
                .Index(t => t.Award_Award_ID)
                .Index(t => t.Award_Award_ID1);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentAwards", "Student_User_ID", "dbo.Accounts");
            DropForeignKey("dbo.StudentAwards", "Award_ID", "dbo.Awards");
            DropForeignKey("dbo.Awards", "Staff_User_ID", "dbo.Accounts");
            DropForeignKey("dbo.Awards", "RewardCategory_Reward_Category_ID", "dbo.RewardCategories");
            DropForeignKey("dbo.RewardCategories", "Award_Award_ID1", "dbo.Awards");
            DropForeignKey("dbo.RewardCategories", "Award_Award_ID", "dbo.Awards");
            DropIndex("dbo.RewardCategories", new[] { "Award_Award_ID1" });
            DropIndex("dbo.RewardCategories", new[] { "Award_Award_ID" });
            DropIndex("dbo.Awards", new[] { "Staff_User_ID" });
            DropIndex("dbo.Awards", new[] { "RewardCategory_Reward_Category_ID" });
            DropIndex("dbo.StudentAwards", new[] { "Student_User_ID" });
            DropIndex("dbo.StudentAwards", new[] { "Award_ID" });
            DropTable("dbo.RewardCategories");
            DropTable("dbo.Awards");
            DropTable("dbo.StudentAwards");
        }
    }
}
