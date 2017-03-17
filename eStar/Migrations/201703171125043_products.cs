namespace eStar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class products : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductOrders", "Order_ID", "dbo.Orders");
            DropIndex("dbo.ProductOrders", new[] { "Order_ID" });
            AddColumn("dbo.ProductOrders", "Orders_Order_ID", c => c.Int());
            AddColumn("dbo.ProductOrders", "Order_Order_ID", c => c.Int());
            AddColumn("dbo.ProductOrders", "Order_Order_ID1", c => c.Int());
            CreateIndex("dbo.ProductOrders", "Orders_Order_ID");
            CreateIndex("dbo.ProductOrders", "Order_Order_ID");
            CreateIndex("dbo.ProductOrders", "Order_Order_ID1");
            AddForeignKey("dbo.ProductOrders", "Order_Order_ID1", "dbo.Orders", "Order_ID");
            AddForeignKey("dbo.ProductOrders", "Order_Order_ID", "dbo.Orders", "Order_ID");
            AddForeignKey("dbo.ProductOrders", "Orders_Order_ID", "dbo.Orders", "Order_ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductOrders", "Orders_Order_ID", "dbo.Orders");
            DropForeignKey("dbo.ProductOrders", "Order_Order_ID", "dbo.Orders");
            DropForeignKey("dbo.ProductOrders", "Order_Order_ID1", "dbo.Orders");
            DropIndex("dbo.ProductOrders", new[] { "Order_Order_ID1" });
            DropIndex("dbo.ProductOrders", new[] { "Order_Order_ID" });
            DropIndex("dbo.ProductOrders", new[] { "Orders_Order_ID" });
            DropColumn("dbo.ProductOrders", "Order_Order_ID1");
            DropColumn("dbo.ProductOrders", "Order_Order_ID");
            DropColumn("dbo.ProductOrders", "Orders_Order_ID");
            CreateIndex("dbo.ProductOrders", "Order_ID");
            AddForeignKey("dbo.ProductOrders", "Order_ID", "dbo.Orders", "Order_ID", cascadeDelete: true);
        }
    }
}
