namespace eStar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Accounts2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Accounts", "First_Name", c => c.String());
            AlterColumn("dbo.Accounts", "Surname", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Accounts", "Surname", c => c.String(nullable: false));
            AlterColumn("dbo.Accounts", "First_Name", c => c.String(nullable: false));
        }
    }
}
