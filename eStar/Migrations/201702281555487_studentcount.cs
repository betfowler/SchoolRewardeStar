namespace eStar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class studentcount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Awards", "StudentCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Awards", "StudentCount");
        }
    }
}
