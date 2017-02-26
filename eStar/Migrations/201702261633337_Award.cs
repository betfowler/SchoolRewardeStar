namespace eStar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Award : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Awards",
                c => new
                    {
                        Award_ID = c.Int(nullable: false, identity: true),
                        Student_ID = c.Int(nullable: false),
                        Staff_ID = c.Int(nullable: false),
                        Num_Points = c.Int(nullable: false),
                        Reward_Category_ID = c.Int(nullable: false),
                        Reward_Comment = c.String(),
                    })
                .PrimaryKey(t => t.Award_ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Awards");
        }
    }
}
