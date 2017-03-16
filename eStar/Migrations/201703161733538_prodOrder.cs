namespace eStar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class prodOrder : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orders", "Quantity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Quantity", c => c.Int(nullable: false));
        }
    }
}
