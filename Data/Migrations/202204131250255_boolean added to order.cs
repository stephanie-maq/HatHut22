namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class booleanaddedtoorder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "IsSent", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orders", "HaveMaterials", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "HaveMaterials");
            DropColumn("dbo.Orders", "IsSent");
        }
    }
}
