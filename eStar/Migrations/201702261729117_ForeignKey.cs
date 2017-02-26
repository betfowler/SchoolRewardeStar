namespace eStar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForeignKey : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Awards", "Reward_Category_ID");
            AddForeignKey("dbo.Awards", "Reward_Category_ID", "dbo.RewardCategories", "Reward_Category_ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Awards", "Reward_Category_ID", "dbo.RewardCategories");
            DropIndex("dbo.Awards", new[] { "Reward_Category_ID" });
        }
    }
}
