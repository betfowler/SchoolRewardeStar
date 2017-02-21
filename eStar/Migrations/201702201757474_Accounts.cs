namespace eStar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Accounts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "Admin", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Accounts", "Admin");
        }
    }
}
