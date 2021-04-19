namespace ISP.DatabaseAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixNullableInLaptopsTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Laptops", "ManufacturerName", c => c.String());
            AlterColumn("dbo.Laptops", "ScreenDiagonal", c => c.String());
            AlterColumn("dbo.Laptops", "Resolution", c => c.String());
            AlterColumn("dbo.Laptops", "ScreenSurfaceType", c => c.String());
            AlterColumn("dbo.Laptops", "ProcessorName", c => c.String());
            AlterColumn("dbo.Laptops", "Ram", c => c.String());
            AlterColumn("dbo.Laptops", "DiskSize", c => c.String());
            AlterColumn("dbo.Laptops", "DiskType", c => c.String());
            AlterColumn("dbo.Laptops", "Gpu", c => c.String());
            AlterColumn("dbo.Laptops", "Vram", c => c.String());
            AlterColumn("dbo.Laptops", "Os", c => c.String());
            AlterColumn("dbo.Laptops", "Drive", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Laptops", "Drive", c => c.String(nullable: false));
            AlterColumn("dbo.Laptops", "Os", c => c.String(nullable: false));
            AlterColumn("dbo.Laptops", "Vram", c => c.String(nullable: false));
            AlterColumn("dbo.Laptops", "Gpu", c => c.String(nullable: false));
            AlterColumn("dbo.Laptops", "DiskType", c => c.String(nullable: false));
            AlterColumn("dbo.Laptops", "DiskSize", c => c.String(nullable: false));
            AlterColumn("dbo.Laptops", "Ram", c => c.String(nullable: false));
            AlterColumn("dbo.Laptops", "ProcessorName", c => c.String(nullable: false));
            AlterColumn("dbo.Laptops", "ScreenSurfaceType", c => c.String(nullable: false));
            AlterColumn("dbo.Laptops", "Resolution", c => c.String(nullable: false));
            AlterColumn("dbo.Laptops", "ScreenDiagonal", c => c.String(nullable: false));
            AlterColumn("dbo.Laptops", "ManufacturerName", c => c.String(nullable: false));
        }
    }
}
