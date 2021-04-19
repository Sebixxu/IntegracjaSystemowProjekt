namespace ISP.DatabaseAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameLaptopsTable2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.LaptopsDto", newName: "Laptops");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Laptops", newName: "LaptopsDto");
        }
    }
}
