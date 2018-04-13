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

            SeedAppliances();
            return Content("Seeded database.");
        }

        /**
         * Creates user roles: Admin, Manager, Staff, Tenant, Applicant
         */
        private void SeedUserRoles()
        {
            string[] queries = new string[]
            {
                "DELETE FROM dbo.AspNetRoles",
                "INSERT INTO dbo.AspNetRoles(Id, Name) VALUES(1, 'Admin'), (2, 'Manager'), (3, 'Staff'), (4, 'Tenant'), (5, 'Applicant')"
            };
            foreach (string query in queries)
                db.Database.ExecuteSqlCommand(query);
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
            string[] queries = new string[]
            {
                "DELETE FROM dbo.AspNetUserRoles",
                "DELETE FROM dbo.AspNetUsers",

                "INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'4aff0be9-7729-41bc-abad-c4cb2c94c8f1', N'manager@gmail.com', 0, N'ANCiYwmVTprsEy2y/fLAf1ZUkMOwo1/TNiy3Z0xRajvOQ5efEt5WHIUBc1ZT2P0UQQ==', N'54b8c1ab-89b5-4c75-bba4-4e77b6b729bf', NULL, 0, 0, NULL, 1, 0, N'manager@gmail.com')",
                "INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'52a92cae-8175-4b67-a5b3-f03bfa052757', N'staff@gmail.com', 0, N'APifvkhuiE4q7qqT+qcs89DQe40/XL0pYoEphnLKEXD0qCcplGs49J0+5z+ss6kk2w==', N'a6fb26c4-ecd1-4e70-99c1-53adc8bbb0f7', NULL, 0, 0, NULL, 1, 0, N'staff@gmail.com')",
                "INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'9789fdc4-5834-4d0c-8930-d643ffb1f7ab', N'tenant@gmail.com', 0, N'AKapYYc3twptFli1PFKyK1f4oLCT68c9X+TXr8vKvJUW3pY9grQkz6aLl502fVnMvw==', N'80af3424-aaf0-4e13-8e6d-6aed0412419b', NULL, 0, 0, NULL, 1, 0, N'tenant@gmail.com')",
                "INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'9ac555ed-8592-4c8c-86e7-6eb22d7cf018', N'applicant@gmail.com', 0, N'ADChS5j9KSDTCTEL/BU63xkX5E1yz3oy3apDlCEg6zAslid/Ov5RHElEMimROGI9Mw==', N'ca643078-888b-478a-8eef-0d64c3933b0a', NULL, 0, 0, NULL, 1, 0, N'applicant@gmail.com')",

                "INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'4aff0be9-7729-41bc-abad-c4cb2c94c8f1', N'2')",
                "INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'52a92cae-8175-4b67-a5b3-f03bfa052757', N'3')",
                "INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'9789fdc4-5834-4d0c-8930-d643ffb1f7ab', N'4')",
                "INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'9ac555ed-8592-4c8c-86e7-6eb22d7cf018', N'5')"
            };
            foreach (string query in queries)
                db.Database.ExecuteSqlCommand(query);
        }

        private void SeedAppliances()
        {
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
