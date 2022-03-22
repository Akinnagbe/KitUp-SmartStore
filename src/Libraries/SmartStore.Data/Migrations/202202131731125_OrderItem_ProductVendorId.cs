namespace SmartStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderItem_ProductVendorId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderItem", "ProductVendorId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderItem", "ProductVendorId");
        }
    }
}
