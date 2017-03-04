namespace eStar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class selected : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "ToAward", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Accounts", "ToAward");
        }
    }
}
