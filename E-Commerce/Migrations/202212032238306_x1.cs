namespace E_Commerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class x1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Carts", "orderid", "dbo.Orders");
            DropIndex("dbo.Carts", new[] { "orderid" });
            AlterColumn("dbo.Carts", "orderid", c => c.Int());
            CreateIndex("dbo.Carts", "orderid");
            AddForeignKey("dbo.Carts", "orderid", "dbo.Orders", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Carts", "orderid", "dbo.Orders");
            DropIndex("dbo.Carts", new[] { "orderid" });
            AlterColumn("dbo.Carts", "orderid", c => c.Int(nullable: false));
            CreateIndex("dbo.Carts", "orderid");
            AddForeignKey("dbo.Carts", "orderid", "dbo.Orders", "ID", cascadeDelete: true);
        }
    }
}
