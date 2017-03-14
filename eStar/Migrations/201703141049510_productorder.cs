namespace eStar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class productorder : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "Orders_Order_ID", "dbo.Orders");
            DropIndex("dbo.Products", new[] { "Orders_Order_ID" });
            CreateTable(
                "dbo.ProductOrders",
                c => new
                    {
                        ProductOrder_ID = c.Int(nullable: false, identity: true),
                        Product_ID = c.Int(nullable: false),
                        Order_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductOrder_ID)
                .ForeignKey("dbo.Orders", t => t.Order_ID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_ID, cascadeDelete: true)
                .Index(t => t.Product_ID)
                .Index(t => t.Order_ID);
            
            AddColumn("dbo.Orders", "ProductCount", c => c.Int(nullable: false));
            DropColumn("dbo.Orders", "Product_ID");
            DropColumn("dbo.Products", "Orders_Order_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Orders_Order_ID", c => c.Int());
            AddColumn("dbo.Orders", "Product_ID", c => c.Int(nullable: false));
            DropForeignKey("dbo.ProductOrders", "Product_ID", "dbo.Products");
            DropForeignKey("dbo.ProductOrders", "Order_ID", "dbo.Orders");
            DropIndex("dbo.ProductOrders", new[] { "Order_ID" });
            DropIndex("dbo.ProductOrders", new[] { "Product_ID" });
            DropColumn("dbo.Orders", "ProductCount");
            DropTable("dbo.ProductOrders");
            CreateIndex("dbo.Products", "Orders_Order_ID");
            AddForeignKey("dbo.Products", "Orders_Order_ID", "dbo.Orders", "Order_ID");
        }
    }
}
