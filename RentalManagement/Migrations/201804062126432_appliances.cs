namespace RentalManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appliances : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Appliances", name: "Asset_ID", newName: "BelongsToAsset_ID");
            RenameIndex(table: "dbo.Appliances", name: "IX_Asset_ID", newName: "IX_BelongsToAsset_ID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Appliances", name: "IX_BelongsToAsset_ID", newName: "IX_Asset_ID");
            RenameColumn(table: "dbo.Appliances", name: "BelongsToAsset_ID", newName: "Asset_ID");
        }
    }
}
