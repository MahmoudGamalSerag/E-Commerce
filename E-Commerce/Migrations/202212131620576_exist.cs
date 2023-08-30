namespace E_Commerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class exist : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Exist", c => c.Boolean(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Exist");
        }
    }
}
