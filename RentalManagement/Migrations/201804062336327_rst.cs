namespace RentalManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rst : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MaintenanceRequests",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CompletedDate = c.DateTime(),
                        Subject = c.String(nullable: false),
                        RequestDetail = c.String(),
                        StatusDetail = c.String(),
                        FixDetail = c.String(),
                        HoursSpent = c.Int(nullable: false),
                        Asset_ID = c.Guid(),
                        Tenant_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Assets", t => t.Asset_ID)
                .ForeignKey("dbo.Tenants", t => t.Tenant_ID)
                .Index(t => t.Asset_ID)
                .Index(t => t.Tenant_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MaintenanceRequests", "Tenant_ID", "dbo.Tenants");
            DropForeignKey("dbo.MaintenanceRequests", "Asset_ID", "dbo.Assets");
            DropIndex("dbo.MaintenanceRequests", new[] { "Tenant_ID" });
            DropIndex("dbo.MaintenanceRequests", new[] { "Asset_ID" });
            DropTable("dbo.MaintenanceRequests");
        }
    }
}
