namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class materialwip : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Materials", "Color");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Materials", "Color", c => c.String());
        }
    }
}
