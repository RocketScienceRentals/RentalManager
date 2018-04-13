using System;
using System.Collections.Generic;
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
        [Required]
        public DateTime CreatedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        [Required]
        public string Subject { get; set; }
        public string RequestDetail { get; set; }
        public string StatusDetail { get; set; }
        public string FixDetail { get; set; }
        public int HoursSpent { get; set; }
    }
}