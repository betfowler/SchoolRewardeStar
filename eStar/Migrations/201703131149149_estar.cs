namespace eStar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class estar : DbMigration
    {
        public override void Up()
        {
            AddForeignKey("dbo.Awards", "Subject_ID", "dbo.Subjects", "Subject_ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
        }
    }
}
