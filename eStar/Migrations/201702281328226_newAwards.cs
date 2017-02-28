namespace eStar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newAwards : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RewardCategories", "Award_Award_ID", "dbo.Awards");
            DropForeignKey("dbo.RewardCategories", "Award_Award_ID1", "dbo.Awards");
            DropForeignKey("dbo.Awards", "RewardCategory_Reward_Category_ID", "dbo.RewardCategories");
            DropForeignKey("dbo.Awards", "Staff_User_ID", "dbo.Accounts");
            DropForeignKey("dbo.StudentAwards", "Award_ID", "dbo.Awards");
            DropForeignKey("dbo.StudentAwards", "Student_User_ID", "dbo.Accounts");
            DropIndex("dbo.StudentAwards", new[] { "Award_ID" });
            DropIndex("dbo.StudentAwards", new[] { "Student_User_ID" });
            DropIndex("dbo.Awards", new[] { "RewardCategory_Reward_Category_ID" });
            DropIndex("dbo.Awards", new[] { "Staff_User_ID" });
            DropIndex("dbo.RewardCategories", new[] { "Award_Award_ID" });
            DropIndex("dbo.RewardCategories", new[] { "Award_Award_ID1" });
            DropTable("dbo.StudentAwards");
            DropTable("dbo.Awards");
            DropTable("dbo.RewardCategories");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RewardCategories",
                c => new
                    {
                        Reward_Category_ID = c.Int(nullable: false, identity: true),
                        Reward_Category = c.String(),
                        Award_Award_ID = c.Int(),
                        Award_Award_ID1 = c.Int(),
                    })
                .PrimaryKey(t => t.Reward_Category_ID);
            
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
                .PrimaryKey(t => t.Award_ID);
            
            CreateTable(
                "dbo.StudentAwards",
                c => new
                    {
                        StudentAward_ID = c.Int(nullable: false, identity: true),
                        Student_ID = c.Int(nullable: false),
                        Award_ID = c.Int(nullable: false),
                        Student_User_ID = c.Int(),
                    })
                .PrimaryKey(t => t.StudentAward_ID);
            
            CreateIndex("dbo.RewardCategories", "Award_Award_ID1");
            CreateIndex("dbo.RewardCategories", "Award_Award_ID");
            CreateIndex("dbo.Awards", "Staff_User_ID");
            CreateIndex("dbo.Awards", "RewardCategory_Reward_Category_ID");
            CreateIndex("dbo.StudentAwards", "Student_User_ID");
            CreateIndex("dbo.StudentAwards", "Award_ID");
            AddForeignKey("dbo.StudentAwards", "Student_User_ID", "dbo.Accounts", "User_ID");
            AddForeignKey("dbo.StudentAwards", "Award_ID", "dbo.Awards", "Award_ID", cascadeDelete: true);
            AddForeignKey("dbo.Awards", "Staff_User_ID", "dbo.Accounts", "User_ID");
            AddForeignKey("dbo.Awards", "RewardCategory_Reward_Category_ID", "dbo.RewardCategories", "Reward_Category_ID");
            AddForeignKey("dbo.RewardCategories", "Award_Award_ID1", "dbo.Awards", "Award_ID");
            AddForeignKey("dbo.RewardCategories", "Award_Award_ID", "dbo.Awards", "Award_ID");
        }
    }
}
