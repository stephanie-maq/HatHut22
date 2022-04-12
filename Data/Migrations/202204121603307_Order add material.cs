namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Orderaddmaterial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Material", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Material");
        }
    }
}
