namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AttributeFix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "IsHatFinnished", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orders", "Price", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "Price", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "IsCostumerMeasuredProduct", c => c.Boolean(nullable: false));
            DropColumn("dbo.Orders", "IsFinnished");
            DropColumn("dbo.Products", "IsCostumerProduct");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "IsCostumerProduct", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orders", "IsFinnished", c => c.Boolean(nullable: false));
            DropColumn("dbo.Products", "IsCostumerMeasuredProduct");
            DropColumn("dbo.Products", "Price");
            DropColumn("dbo.Orders", "Price");
            DropColumn("dbo.Orders", "IsHatFinnished");
        }
    }
}
