using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RentalManagement.Models
{
    public class MaintenanceRequest
    {
        public Guid ID { get; set; }
        public Tenant Tenant { get; set; }
        public Asset Asset { get; set; }
        [DisplayName("Created Date")]
        public DateTime CreatedDate { get; set; }
        [DisplayName("Completed Date")]
        public DateTime ? CompletedDate { get; set; } // the ? makes it nullable
        [Required]
        public string Subject { get; set; }
        [DisplayName("Request Details")]
        public string RequestDetail { get; set; }
        [DisplayName("Status Details")]
        public string StatusDetail { get; set; }
        [DisplayName("Fix Details")]
        public string FixDetail { get; set; }
        [DisplayName("Fix Time")]
        public int HoursSpent { get; set; }
    }
}