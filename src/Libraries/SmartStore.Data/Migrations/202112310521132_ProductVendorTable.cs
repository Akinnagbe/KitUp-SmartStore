namespace SmartStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductVendorTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductVendor",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.String(nullable: false, maxLength: 450),
                        BusinessName = c.String(nullable: false, maxLength: 225),
                        Address = c.String(nullable: false, maxLength: 450),
                        PhoneNumber = c.String(nullable: false, maxLength: 11),
                        Email = c.String(nullable: false, maxLength: 225),
                        CreatedOnUtc = c.DateTime(nullable: false),
                        UpdatedOnUtc = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        SubjectToAcl = c.Boolean(nullable: false),
                        LimitedToStores = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Deleted)
                .Index(t => t.SubjectToAcl);
            
            AddColumn("dbo.Product", "ProductVendorId", c => c.Int());
           // AddColumn("dbo.Product", "ProductVendor_Id", c => c.Int());
            CreateIndex("dbo.Product", "ProductVendorId");
           // CreateIndex("dbo.Product", "ProductVendor_Id");
           // AddForeignKey("dbo.Product", "ProductVendor_Id", "dbo.ProductVendor", "Id");
            AddForeignKey("dbo.Product", "ProductVendorId", "dbo.ProductVendor", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Product", "ProductVendorId", "dbo.ProductVendor");
           // DropForeignKey("dbo.Product", "ProductVendor_Id", "dbo.ProductVendor");
            DropIndex("dbo.ProductVendor", new[] { "SubjectToAcl" });
            DropIndex("dbo.ProductVendor", new[] { "Deleted" });
            DropIndex("dbo.Product", new[] { "ProductVendor_Id" });
            DropIndex("dbo.Product", new[] { "ProductVendorId" });
          //  DropColumn("dbo.Product", "ProductVendor_Id");
            DropColumn("dbo.Product", "ProductVendorId");
            DropTable("dbo.ProductVendor");
        }
    }
}
