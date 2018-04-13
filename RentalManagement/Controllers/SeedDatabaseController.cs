using RentalManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

/**
 * THIS CONTROLLER SEEDS A DATABASE THAT HAS BEEN NUKED
 * 
 * 1. NUKE DB
 *    a. Menu View -> SQL Server Object Explorer -> Delete RentalManager DB
 *    
 * 2. UPDATE DB
 *    a. Menu Tools -> NuGet Package Manager -> Package Manager Console
 *    b. Run Enable-Migrations -EnableAutomaticMigrations -Force
 *    c. Run Update-Database -Force
 *    
 * 3. SEED DB
 *    a. go to localhost/seeddatabase
 */

namespace RentalManagement.Controllers
{
    public class SeedDatabaseController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            SeedUserRoles();
            SeedUserAccounts();

            SeedAssets();
            SeedAppliances();

            return Content("Seeded database.");
        }

        /**
         * Creates user roles: Admin, Manager, Staff, Tenant, Applicant
         */
        private void SeedUserRoles()
        {
            ExecuteQueries(new string[]
            {
                "DELETE FROM dbo.AspNetRoles"
                ,"INSERT INTO dbo.AspNetRoles(Id, Name) VALUES(1, 'Admin'), (2, 'Manager'), (3, 'Staff'), (4, 'Tenant'), (5, 'Applicant')"
            });
        }

        /**
         * Creates user accounts: manager@gmail.com      with Manager   role
         *                        staff@gmail.com        with Staff     role
         *                        tenant@gmail.com       with Tenant    role
         *                        applicant@gmail.com    with Applicant role
         *
         *                    ->  all with password:     Password1!
         */
        private void SeedUserAccounts()
        {
            ExecuteQueries(new string[]
            {
                "DELETE FROM dbo.AspNetUserRoles"
                ,"DELETE FROM dbo.AspNetUsers"

                ,"INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'4aff0be9-7729-41bc-abad-c4cb2c94c8f1', N'manager@gmail.com', 0, N'ANCiYwmVTprsEy2y/fLAf1ZUkMOwo1/TNiy3Z0xRajvOQ5efEt5WHIUBc1ZT2P0UQQ==', N'54b8c1ab-89b5-4c75-bba4-4e77b6b729bf', NULL, 0, 0, NULL, 1, 0, N'manager@gmail.com')"
                ,"INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'52a92cae-8175-4b67-a5b3-f03bfa052757', N'staff@gmail.com', 0, N'APifvkhuiE4q7qqT+qcs89DQe40/XL0pYoEphnLKEXD0qCcplGs49J0+5z+ss6kk2w==', N'a6fb26c4-ecd1-4e70-99c1-53adc8bbb0f7', NULL, 0, 0, NULL, 1, 0, N'staff@gmail.com')"
                ,"INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'9789fdc4-5834-4d0c-8930-d643ffb1f7ab', N'tenant@gmail.com', 0, N'AKapYYc3twptFli1PFKyK1f4oLCT68c9X+TXr8vKvJUW3pY9grQkz6aLl502fVnMvw==', N'80af3424-aaf0-4e13-8e6d-6aed0412419b', NULL, 0, 0, NULL, 1, 0, N'tenant@gmail.com')"
                ,"INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'9ac555ed-8592-4c8c-86e7-6eb22d7cf018', N'applicant@gmail.com', 0, N'ADChS5j9KSDTCTEL/BU63xkX5E1yz3oy3apDlCEg6zAslid/Ov5RHElEMimROGI9Mw==', N'ca643078-888b-478a-8eef-0d64c3933b0a', NULL, 0, 0, NULL, 1, 0, N'applicant@gmail.com')"

                ,"INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'4aff0be9-7729-41bc-abad-c4cb2c94c8f1', N'2')"
                ,"INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'52a92cae-8175-4b67-a5b3-f03bfa052757', N'3')"
                ,"INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'9789fdc4-5834-4d0c-8930-d643ffb1f7ab', N'4')"
                ,"INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'9ac555ed-8592-4c8c-86e7-6eb22d7cf018', N'5')"
            });
        }

        private void SeedAssets()
        {
            ExecuteQuery(@"
                SET IDENTITY_INSERT [dbo].[FullAddresses] ON;
                INSERT INTO [dbo].[FullAddresses] ([ID], [MyProperty], [StreetAddress], [Province], [Country], [PostalCode]) VALUES (1, 'e', N'420 High Street', N'BC', N'Canada', N'V5I 2M2');
                INSERT INTO [dbo].[FullAddresses] ([ID], [MyProperty], [StreetAddress], [Province], [Country], [PostalCode]) VALUES (2, 'e', N'123 Balling Ave', N'BC', N'Canada', N'V5O 1Q1');
                INSERT INTO [dbo].[FullAddresses] ([ID], [MyProperty], [StreetAddress], [Province], [Country], [PostalCode]) VALUES (3, 'e', N'123 Graduation Road', N'BC', N'Canada', N'B1B 1B1');
                INSERT INTO [dbo].[FullAddresses] ([ID], [MyProperty], [StreetAddress], [Province], [Country], [PostalCode]) VALUES (4, 'e', N'001 First Ave', N'BC', N'Canada', N'P1M 2Y1');
                SET IDENTITY_INSERT [dbo].[FullAddresses] OFF;

                INSERT INTO [dbo].[Assets] ([ID], [IsOccuppied], [Name], [Type], [AskingRent], [Address_ID]) VALUES (N'7f1fa6b5-3711-404b-ac85-4cfcb5444798', 0, N'Basement - 2 rooms', 0, 1300, 1);
                INSERT INTO [dbo].[Assets] ([ID], [IsOccuppied], [Name], [Type], [AskingRent], [Address_ID]) VALUES (N'a0ed702e-76cd-48d9-97b5-9ba3d55c1ef0', 0, N'Family - 2 Rooms', 2, 1200, 4);
                INSERT INTO [dbo].[Assets] ([ID], [IsOccuppied], [Name], [Type], [AskingRent], [Address_ID]) VALUES (N'e9244430-7296-4686-a1a0-af5d481ee4b6', 0, N'Garage House - 2 rooms', 0, 1700, 2);
                INSERT INTO [dbo].[Assets] ([ID], [IsOccuppied], [Name], [Type], [AskingRent], [Address_ID]) VALUES (N'7a1743e5-f821-4b88-912e-f4c49e1f6dc4', 0, N'Work Studio', 1, 700, 3);
            ");
        }

        private void SeedAppliances()
        {
            ExecuteQueries(new string[]
            {
                "INSERT INTO [dbo].[Appliances] ([ID], [Name], [Type], [Description], [BelongsToAsset_ID]) VALUES (N'4e7b5f1b-9b4b-477a-ab93-0583201e36bd', N'Samsung Washer', 1, N'Samsung  Samsung 5.2 Cu. Ft. Large Capacity Top-Load Washer - White', N'7f1fa6b5-3711-404b-ac85-4cfcb5444798')"
                ,"INSERT INTO [dbo].[Appliances] ([ID], [Name], [Type], [Description], [BelongsToAsset_ID]) VALUES (N'c8f71ff5-3eea-439e-bff9-07d0ae8db185', N'Amana Washer', 1, N'Amana NTW4755EW 4.1-cu ft High-Efficiency Top-Load Washer (White)', N'a0ed702e-76cd-48d9-97b5-9ba3d55c1ef0')"
                ,"INSERT INTO [dbo].[Appliances] ([ID], [Name], [Type], [Description], [BelongsToAsset_ID]) VALUES (N'f1f912c9-8262-4bff-a4d4-0b997a277a24', N'Panasonic Microwave', 3, N'Panasonic 1.3 cu-ft 1100-Watt Countertop Microwave (White)', N'7f1fa6b5-3711-404b-ac85-4cfcb5444798')"
                ,"INSERT INTO [dbo].[Appliances] ([ID], [Name], [Type], [Description], [BelongsToAsset_ID]) VALUES (N'2ab62498-83de-4f5f-953d-2e89a1b92d18', N'Samsung Dish Washer', 0, N'24-inch Built-In Dishwasher with Stainless Steel Tub in White', N'e9244430-7296-4686-a1a0-af5d481ee4b6')"
                ,"INSERT INTO [dbo].[Appliances] ([ID], [Name], [Type], [Description], [BelongsToAsset_ID]) VALUES (N'9bb7baea-978e-4789-8f57-369036242562', N'GE Dryer', 2, N'White 7 Cu.Feet. Capacity Electric Dryer', NULL)"
                ,"INSERT INTO [dbo].[Appliances] ([ID], [Name], [Type], [Description], [BelongsToAsset_ID]) VALUES (N'a0291a8c-aeac-4eaf-8626-39d30099248d', N'GE Dishwasher', 0, N'24-inch 46 dBA Top Control Built-In Tall Tub Dishwasher with Stainless Steel Tub and Steam Cleaning', NULL)"
                ,"INSERT INTO [dbo].[Appliances] ([ID], [Name], [Type], [Description], [BelongsToAsset_ID]) VALUES (N'a2713c9d-32b6-4714-881d-55f85078136f', N'KitchenAid Stove', 4, N'5.1 cu. ft. Dual Fuel Free-Standing 6-Burner Commercial Style Range in Stainless Steel', N'a0ed702e-76cd-48d9-97b5-9ba3d55c1ef0')"
                ,"INSERT INTO [dbo].[Appliances] ([ID], [Name], [Type], [Description], [BelongsToAsset_ID]) VALUES (N'70bb32e5-1f09-423a-afa3-606fedb46828', N'Maytag Dishwasher', 0, N'24-inch Dishwasher with Stainless Steel Tub and Large Capacity in White', NULL)"
                ,"INSERT INTO [dbo].[Appliances] ([ID], [Name], [Type], [Description], [BelongsToAsset_ID]) VALUES (N'01c4580a-8540-4967-a7de-63e511846287', N'LG Microwave', 3, N'LG 1.5-cu ft 1200-Watt Countertop Microwave (Stainless Steel)', N'a0ed702e-76cd-48d9-97b5-9ba3d55c1ef0')"
                ,"INSERT INTO [dbo].[Appliances] ([ID], [Name], [Type], [Description], [BelongsToAsset_ID]) VALUES (N'6647ab2b-d34e-48ee-a520-65d092c12df7', N'Whirlpool Dishwasher', 0, N'Whirlpool Tall Tub Built-In Dishwasher with Adaptive Wash Technology', NULL)"
                ,"INSERT INTO [dbo].[Appliances] ([ID], [Name], [Type], [Description], [BelongsToAsset_ID]) VALUES (N'f93bab0c-a224-4020-b3a6-67e6627940a4', N'KitchenAid Microwave', 3, N'2.0 cu. ft. 950 W Microwave with Sensor Functions in Stainless Steel', NULL)"
                ,"INSERT INTO [dbo].[Appliances] ([ID], [Name], [Type], [Description], [BelongsToAsset_ID]) VALUES (N'5447146a-1f0a-47a3-8cd6-6952fd035916', N'Maytag Dryer', 2, N'Maytag YMEDC415EW 7-cu ft Electric Dryer (White)', NULL)"
                ,"INSERT INTO [dbo].[Appliances] ([ID], [Name], [Type], [Description], [BelongsToAsset_ID]) VALUES (N'77c6bc65-8692-41df-8571-73cde179693c', N'Danby Microwave', 3, N'Counter Top Microwave Oven, 0.9 cu. ft. - S.Steel - 900 W', N'e9244430-7296-4686-a1a0-af5d481ee4b6')"
                ,"INSERT INTO [dbo].[Appliances] ([ID], [Name], [Type], [Description], [BelongsToAsset_ID]) VALUES (N'0cff935b-4821-4332-8050-9ec6fabc3ab9', N'Whirlpool Washer', 1, N'24-inch Built-In Dishwasher with Stainless Steel Tub in White', N'e9244430-7296-4686-a1a0-af5d481ee4b6')"
                ,"INSERT INTO [dbo].[Appliances] ([ID], [Name], [Type], [Description], [BelongsToAsset_ID]) VALUES (N'f3cbc12c-721d-4212-ac32-b0a9f65a6db3', N'Hamilton Microwave', 3, N'Hamilton Beach 0.7 cu.ft. Stainless Steel Microwave', NULL)"
                ,"INSERT INTO [dbo].[Appliances] ([ID], [Name], [Type], [Description], [BelongsToAsset_ID]) VALUES (N'069acf70-9159-493e-bd17-bbdc892977e2', N'Amana Dryer', 2, N'Amana YNED5800DW 7.4-cu-ft Electric Dryer', N'a0ed702e-76cd-48d9-97b5-9ba3d55c1ef0')"
                ,"INSERT INTO [dbo].[Appliances] ([ID], [Name], [Type], [Description], [BelongsToAsset_ID]) VALUES (N'bb9e66c7-f20a-41b9-b82f-c3a2063afa2a', N'GE Stove', 4, N'GE 24-inch Slide-In Electric Range- Stainless Steel', N'7f1fa6b5-3711-404b-ac85-4cfcb5444798')"
                ,"INSERT INTO [dbo].[Appliances] ([ID], [Name], [Type], [Description], [BelongsToAsset_ID]) VALUES (N'6a2ee04a-c644-42ae-ac1d-c43fd800dc48', N'Frigidaire Stove', 4, N'Frigidaire CFEF3016TS Range Electric Range 30 inch Self Clean Coil Burners (Electric) 1 Ovens Free Standing Compare to Teppermans Canada YWFC310S0ES', NULL)"
                ,"INSERT INTO [dbo].[Appliances] ([ID], [Name], [Type], [Description], [BelongsToAsset_ID]) VALUES (N'1099a240-ff93-4386-b517-cbad7a4f9f37', N'Samsung Dryer', 2, N'Samsung DV42H5200EP/AC 7.5-cu ft Electric Dryer', N'7f1fa6b5-3711-404b-ac85-4cfcb5444798')"
                ,"INSERT INTO [dbo].[Appliances] ([ID], [Name], [Type], [Description], [BelongsToAsset_ID]) VALUES (N'3014ede5-c87b-4353-bbdc-dbe6772aa5d0', N'Samsung Stove', 4, N'30-inch 5.8 cu. ft. Slide-In Gas Range with Self-Cleaning Convection Oven in Stainless Steel', NULL)"
                ,"INSERT INTO [dbo].[Appliances] ([ID], [Name], [Type], [Description], [BelongsToAsset_ID]) VALUES (N'fd5f2b07-478e-42f1-8f8a-ddd287c89f2b', N'LG Washer', 1, N'LG 5.2-cu ft High-Efficiency Stackable Front-Load Washer (White) ENERGY STAR', NULL)"
                ,"INSERT INTO [dbo].[Appliances] ([ID], [Name], [Type], [Description], [BelongsToAsset_ID]) VALUES (N'e6009791-2738-4bc0-a508-eb3f5f3eb7a1', N'GE Dishwasher', 0, N'24-inch 46 dBA Top Control Built-In Tall Tub Dishwasher with Stainless Steel Tub and Steam Cleaning', NULL)"
                ,"INSERT INTO [dbo].[Appliances] ([ID], [Name], [Type], [Description], [BelongsToAsset_ID]) VALUES (N'062e5fbf-6691-4ad7-b3ac-f416f31f44cb', N'Whirlpool Stove', 4, N'4.8 cu. ft. Free-Standing Electric Range with Accubake System in Stainless Steel', N'e9244430-7296-4686-a1a0-af5d481ee4b6')"
                ,"INSERT INTO [dbo].[Appliances] ([ID], [Name], [Type], [Description], [BelongsToAsset_ID]) VALUES (N'a4976f46-a4dd-4c79-818a-fc6cba23b16d', N'Whirlpool Dryer', 2, N'Whirlpool 7.4-cu ft Stackable Electric Dryer Steam Cycle (Chrome Shadow) ENERGY STAR', N'e9244430-7296-4686-a1a0-af5d481ee4b6')"
                ,"INSERT INTO [dbo].[Appliances] ([ID], [Name], [Type], [Description], [BelongsToAsset_ID]) VALUES (N'6000e2a4-2619-412b-91d8-ff9196e8556b', N'Samsung Washer', 1, N'Samsung 5.2-Cu Ft High-Efficiency Stackable Front-Load Washer (Platinum) ENERGY STAR', NULL)"
            });
        }

        private void ExecuteQueries(string[] queries)
        {
            foreach (string query in queries)
                ExecuteQuery(query);
        }

        private void ExecuteQuery(string query)
        {
            db.Database.ExecuteSqlCommand(query);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
