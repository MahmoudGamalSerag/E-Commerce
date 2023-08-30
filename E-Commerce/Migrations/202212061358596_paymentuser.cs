namespace E_Commerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class paymentuser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payments", "userid", c => c.Int(nullable: false));
            CreateIndex("dbo.Payments", "userid");
            AddForeignKey("dbo.Payments", "userid", "dbo.Users", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payments", "userid", "dbo.Users");
            DropIndex("dbo.Payments", new[] { "userid" });
            DropColumn("dbo.Payments", "userid");
        }
    }
}
