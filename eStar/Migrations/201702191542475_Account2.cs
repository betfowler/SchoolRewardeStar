namespace eStar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Account2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Accounts", "Email", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Accounts", "Email", c => c.String(nullable: false));
        }
    }
}
