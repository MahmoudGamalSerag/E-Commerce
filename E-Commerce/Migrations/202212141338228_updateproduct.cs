namespace E_Commerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateproduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "updated", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "updated");
        }
    }
}
