namespace ISP.DatabaseAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotNullcolumnsInLaptopsTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LaptopsDto", "ManufacturerName", c => c.String(nullable: false));
            AlterColumn("dbo.LaptopsDto", "ScreenDiagonal", c => c.String(nullable: false));
            AlterColumn("dbo.LaptopsDto", "Resolution", c => c.String(nullable: false));
            AlterColumn("dbo.LaptopsDto", "ScreenSurfaceType", c => c.String(nullable: false));
            AlterColumn("dbo.LaptopsDto", "ProcessorName", c => c.String(nullable: false));
            AlterColumn("dbo.LaptopsDto", "Ram", c => c.String(nullable: false));
            AlterColumn("dbo.LaptopsDto", "DiskSize", c => c.String(nullable: false));
            AlterColumn("dbo.LaptopsDto", "DiskType", c => c.String(nullable: false));
            AlterColumn("dbo.LaptopsDto", "Gpu", c => c.String(nullable: false));
            AlterColumn("dbo.LaptopsDto", "Vram", c => c.String(nullable: false));
            AlterColumn("dbo.LaptopsDto", "Os", c => c.String(nullable: false));
            AlterColumn("dbo.LaptopsDto", "Drive", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LaptopsDto", "Drive", c => c.String());
            AlterColumn("dbo.LaptopsDto", "Os", c => c.String());
            AlterColumn("dbo.LaptopsDto", "Vram", c => c.String());
            AlterColumn("dbo.LaptopsDto", "Gpu", c => c.String());
            AlterColumn("dbo.LaptopsDto", "DiskType", c => c.String());
            AlterColumn("dbo.LaptopsDto", "DiskSize", c => c.String());
            AlterColumn("dbo.LaptopsDto", "Ram", c => c.String());
            AlterColumn("dbo.LaptopsDto", "ProcessorName", c => c.String());
            AlterColumn("dbo.LaptopsDto", "ScreenSurfaceType", c => c.String());
            AlterColumn("dbo.LaptopsDto", "Resolution", c => c.String());
            AlterColumn("dbo.LaptopsDto", "ScreenDiagonal", c => c.String());
            AlterColumn("dbo.LaptopsDto", "ManufacturerName", c => c.String());
        }
    }
}
