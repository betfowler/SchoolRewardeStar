namespace eStar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orders : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "AdminID", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "AdminID", c => c.Int(nullable: false));
        }
    }
}
