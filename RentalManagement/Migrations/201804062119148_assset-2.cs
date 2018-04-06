namespace RentalManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class assset2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appliances",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        Asset_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Assets", t => t.Asset_ID)
                .Index(t => t.Asset_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appliances", "Asset_ID", "dbo.Assets");
            DropIndex("dbo.Appliances", new[] { "Asset_ID" });
            DropTable("dbo.Appliances");
        }
    }
}
