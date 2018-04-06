namespace RentalManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class assets1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Assets", "AskingRent", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Assets", "AskingRent", c => c.String());
        }
    }
}
