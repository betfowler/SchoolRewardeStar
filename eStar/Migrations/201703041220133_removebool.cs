namespace eStar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removebool : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Accounts", "ToAward");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Accounts", "ToAward", c => c.Boolean());
        }
    }
}
