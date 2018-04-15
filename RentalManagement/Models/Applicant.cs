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
        [StringLength(10,MinimumLength =3)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(500, ErrorMessage = "Cannot be more than 500 characters")]
        public string Details { get; set; }
        public Guid AssetID { get; set; }
    }
}