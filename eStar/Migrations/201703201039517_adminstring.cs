namespace eStar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adminstring : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Admin", c => c.String());
            DropColumn("dbo.Orders", "AdminID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "AdminID", c => c.Int());
            DropColumn("dbo.Orders", "Admin");
        }
    }
}
