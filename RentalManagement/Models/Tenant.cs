using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace RentalManagement.Models
{
    //This class is subjected to pos
    public class Tenant
    {
        public Guid ID { get; set; }

        [Required]
        public FullAddress HomeAddress { get; set; }

        [Required(ErrorMessage = "PhoneNumber is required")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; } 

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(500, ErrorMessage = "Cannot be more than 500 characters")]
        public string Details { get; set; }

        //Restrict the user to only apply for 1 asset at a time for simplicity's sake for now
        [Required]
        public Asset RequestedAssets { get; set; }
    }

    public class TenantDbContext : DbContext
    {
        public DbSet<Tenant> Applicants { get; set; }
    }
}