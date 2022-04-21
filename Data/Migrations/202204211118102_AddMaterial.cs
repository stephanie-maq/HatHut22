namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMaterial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Materials",
                c => new
                    {
                        MaterialId = c.Int(nullable: false, identity: true),
                        MaterialName = c.String(),
                        Color = c.String(),
                    })
                .PrimaryKey(t => t.MaterialId);
            
            AddColumn("dbo.Orders", "OrderMaterialId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "OrderMaterialId");
            AddForeignKey("dbo.Orders", "OrderMaterialId", "dbo.Materials", "MaterialId", cascadeDelete: true);
            DropColumn("dbo.Orders", "Material");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Material", c => c.String());
            DropForeignKey("dbo.Orders", "OrderMaterialId", "dbo.Materials");
            DropIndex("dbo.Orders", new[] { "OrderMaterialId" });
            DropColumn("dbo.Orders", "OrderMaterialId");
            DropTable("dbo.Materials");
        }
    }
}
