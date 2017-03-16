namespace eStar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class productOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "TotalCost", c => c.Int(nullable: false));
            AddColumn("dbo.ProductOrders", "ProductName", c => c.String());
            AddColumn("dbo.ProductOrders", "ProductDesc", c => c.String());
            AddColumn("dbo.ProductOrders", "ProductCategory", c => c.String());
            AddColumn("dbo.ProductOrders", "ProductPrice", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductOrders", "ProductPrice");
            DropColumn("dbo.ProductOrders", "ProductCategory");
            DropColumn("dbo.ProductOrders", "ProductDesc");
            DropColumn("dbo.ProductOrders", "ProductName");
            DropColumn("dbo.Orders", "TotalCost");
        }
    }
}
