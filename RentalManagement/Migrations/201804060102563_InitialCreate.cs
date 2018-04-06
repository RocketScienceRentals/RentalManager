namespace RentalManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tenants",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Details = c.String(maxLength: 500),
                        RequestedAssets_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Assets", t => t.RequestedAssets_ID)
                .Index(t => t.RequestedAssets_ID);
            
            CreateTable(
                "dbo.Assets",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                        Type = c.String(),
                        AskingRent = c.String(),
                        Address_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FullAddresses", t => t.Address_ID)
                .Index(t => t.Address_ID);
            
            CreateTable(
                "dbo.FullAddresses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MyProperty = c.String(),
                        StreetAddress = c.String(),
                        Province = c.String(),
                        Country = c.String(),
                        PostalCode = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Occupancies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Detail = c.String(),
                        AssetID_ID = c.Guid(),
                        ClientID_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Assets", t => t.AssetID_ID)
                .ForeignKey("dbo.Tenants", t => t.ClientID_ID)
                .Index(t => t.AssetID_ID)
                .Index(t => t.ClientID_ID);
            
            CreateTable(
                "dbo.Rentals",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NegotiatedOn = c.DateTime(nullable: false),
                        Details = c.String(),
                        AssetID_ID = c.Guid(),
                        ClientID_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Assets", t => t.AssetID_ID)
                .ForeignKey("dbo.Tenants", t => t.ClientID_ID)
                .Index(t => t.AssetID_ID)
                .Index(t => t.ClientID_ID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Tenants", "RequestedAssets_ID", "dbo.Assets");
            DropForeignKey("dbo.Rentals", "ClientID_ID", "dbo.Tenants");
            DropForeignKey("dbo.Rentals", "AssetID_ID", "dbo.Assets");
            DropForeignKey("dbo.Occupancies", "ClientID_ID", "dbo.Tenants");
            DropForeignKey("dbo.Occupancies", "AssetID_ID", "dbo.Assets");
            DropForeignKey("dbo.Assets", "Address_ID", "dbo.FullAddresses");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Rentals", new[] { "ClientID_ID" });
            DropIndex("dbo.Rentals", new[] { "AssetID_ID" });
            DropIndex("dbo.Occupancies", new[] { "ClientID_ID" });
            DropIndex("dbo.Occupancies", new[] { "AssetID_ID" });
            DropIndex("dbo.Assets", new[] { "Address_ID" });
            DropIndex("dbo.Tenants", new[] { "RequestedAssets_ID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Rentals");
            DropTable("dbo.Occupancies");
            DropTable("dbo.FullAddresses");
            DropTable("dbo.Assets");
            DropTable("dbo.Tenants");
        }
    }
}
