using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace RentalManagement.Models
{
    //This class is subjected to pos
    public class Tenant
    {
        public Guid ID { get; set; }

        public string Name { get; set; }
        //[Required(ErrorMessage = "PhoneNumber is required")]
        //[DataType(DataType.PhoneNumber)]
        //[Display(Name = "PhoneNumber")]
        //public string PhoneNumber { get; set; } 

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(500, ErrorMessage = "Cannot be more than 500 characters")]
        public string Details { get; set; }

        //Restrict the user to only apply for 1 asset at a time for simplicity's sake for now
        
        public Asset RequestedAssets { get; set; }

        public virtual TenantDetails TenantDetails { get; set; }
    }

    // Tenant Detaila is a 1-0 : 1 relationship
    // Where it might have the information below
    public class TenantDetails
    {
        public Guid ID { get; set; }

        [Phone]
        public string TenantPhoneNumber { get; set; }

        // A number between 300-900 
        [Range(300,900,ErrorMessage = "Credit Score must be in a range between 300-900")]
        public int CreditScore { get; set; }

        // Number that is more 0+
        [Range(0, int.MaxValue, ErrorMessage = "Monthly earning must be a positive number")]
        public int MonthlyEarning { get; set; }

        public virtual Tenant Tenant { get; set; }
    }

}