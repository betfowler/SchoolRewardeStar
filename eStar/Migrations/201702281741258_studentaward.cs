namespace eStar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class studentaward : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Accounts", "Award_Award_ID", "dbo.Awards");
            DropIndex("dbo.Accounts", new[] { "Award_Award_ID" });
            DropColumn("dbo.Accounts", "Award_Award_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Accounts", "Award_Award_ID", c => c.Int());
            CreateIndex("dbo.Accounts", "Award_Award_ID");
            AddForeignKey("dbo.Accounts", "Award_Award_ID", "dbo.Awards", "Award_ID");
        }
    }
}