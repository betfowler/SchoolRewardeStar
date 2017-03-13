namespace eStar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orders : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Order_ID = c.Int(nullable: false, identity: true),
                        User_ID = c.Int(nullable: false),
                        Product_ID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        OrderStatus_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Order_ID)
                .ForeignKey("dbo.OrderStatus", t => t.OrderStatus_ID, cascadeDelete: true)
                .ForeignKey("dbo.Accounts", t => t.User_ID, cascadeDelete: true)
                .Index(t => t.User_ID)
                .Index(t => t.OrderStatus_ID);
            
            CreateTable(
                "dbo.OrderStatus",
                c => new
                    {
                        OrderStatus_ID = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.OrderStatus_ID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Product_ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.Int(nullable: false),
                        Image = c.String(),
                        ProductCategory_ID = c.Int(nullable: false),
                        Orders_Order_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Product_ID)
                .ForeignKey("dbo.Orders", t => t.Orders_Order_ID)
                .ForeignKey("dbo.ProductCategories", t => t.ProductCategory_ID, cascadeDelete: true)
                .Index(t => t.ProductCategory_ID)
                .Index(t => t.Orders_Order_ID);
            
            CreateTable(
                "dbo.ProductCategories",
                c => new
                    {
                        ProductCategory_ID = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.ProductCategory_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "User_ID", "dbo.Accounts");
            DropForeignKey("dbo.Products", "ProductCategory_ID", "dbo.ProductCategories");
            DropForeignKey("dbo.Products", "Orders_Order_ID", "dbo.Orders");
            DropForeignKey("dbo.Orders", "OrderStatus_ID", "dbo.OrderStatus");
            DropIndex("dbo.Products", new[] { "Orders_Order_ID" });
            DropIndex("dbo.Products", new[] { "ProductCategory_ID" });
            DropIndex("dbo.Orders", new[] { "OrderStatus_ID" });
            DropIndex("dbo.Orders", new[] { "User_ID" });
            DropTable("dbo.ProductCategories");
            DropTable("dbo.Products");
            DropTable("dbo.OrderStatus");
            DropTable("dbo.Orders");
        }
    }
}
