using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RentalManagement.Models
{
    public class Applicant
    {
        [Key]
        public int ApplicantID { get; set; }

        [Required]
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [StringLength(20, ErrorMessage = "Cannot be more than 20 characters")]
        public string Details { get; set; }

        [DataType(DataType.PhoneNumber)]
        public int PhoneNumber { get; set; }

        public Guid AssetID { get; set; }
    }
}