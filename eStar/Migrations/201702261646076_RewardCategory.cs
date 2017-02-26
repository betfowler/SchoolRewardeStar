namespace eStar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RewardCategory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RewardCategories",
                c => new
                    {
                        Reward_Category_ID = c.Int(nullable: false, identity: true),
                        Reward_Category = c.String(),
                    })
                .PrimaryKey(t => t.Reward_Category_ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RewardCategories");
        }
    }
}
