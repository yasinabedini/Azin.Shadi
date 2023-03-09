namespace Azin.Shadi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDataBase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Family = c.String(nullable: false),
                        Username = c.String(),
                        Password = c.String(nullable: false),
                        Eamil = c.String(),
                        Mobile = c.String(nullable: false),
                        RegisterDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Department = c.String(nullable: false),
                        LastLoginDate = c.DateTime(),
                        ImageName = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Category = c.String(nullable: false),
                        Price = c.Int(nullable: false),
                        Brand = c.String(),
                        Inventory = c.Int(nullable: false),
                        InventoryStatus = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        ImageName = c.String(),
                        RegisterDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Products");
            DropTable("dbo.Admins");
        }
    }
}
