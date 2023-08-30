namespace E_Commerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class leng : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Payments", "FullName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Payments", "FullName", c => c.String(nullable: false, maxLength: 9));
        }
    }
}
