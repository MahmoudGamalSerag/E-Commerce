namespace E_Commerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cart : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        userid = c.Int(nullable: false),
                        productid = c.Int(nullable: false),
                        orderid = c.Int(nullable: true),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Orders", t => t.orderid, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.productid, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.userid, cascadeDelete: false)
                .Index(t => t.userid)
                .Index(t => t.productid)
                .Index(t => t.orderid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Carts", "userid", "dbo.Users");
            DropForeignKey("dbo.Carts", "productid", "dbo.Products");
            DropForeignKey("dbo.Carts", "orderid", "dbo.Orders");
            DropIndex("dbo.Carts", new[] { "orderid" });
            DropIndex("dbo.Carts", new[] { "productid" });
            DropIndex("dbo.Carts", new[] { "userid" });
            DropTable("dbo.Carts");
        }
    }
}
