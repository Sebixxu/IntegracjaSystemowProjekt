namespace ISP.DatabaseAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLaptopsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LaptopsDto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ManufacturerName = c.String(),
                        ScreenDiagonal = c.String(),
                        Resolution = c.String(),
                        ScreenSurfaceType = c.String(),
                        IsTouchable = c.Boolean(nullable: false),
                        ProcessorName = c.String(),
                        NumberOfPhysicalCores = c.Int(),
                        Frequency = c.Int(),
                        Ram = c.String(),
                        DiskSize = c.String(),
                        DiskType = c.String(),
                        Gpu = c.String(),
                        Vram = c.String(),
                        Os = c.String(),
                        Drive = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LaptopsDto");
        }
    }
}
