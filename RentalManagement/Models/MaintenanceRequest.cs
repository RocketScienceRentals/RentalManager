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
        [ScaffoldColumn(false)]
        public Guid ID { get; set; }

        public Tenant Tenant { get; set; }

        public Asset Asset { get; set; }

        [Required(ErrorMessage = "A Created Date is Required!")]
        [DisplayName("Created Date")]
        public DateTime CreatedDate { get; set; }

        [DisplayName("Completed Date")]
        public DateTime? CompletedDate { get; set; }

        [Required(ErrorMessage = "A Subject is Required!")]
        [StringLength(100)]
        public string Subject { get; set; }

        [DisplayName("Request Details")]
        [StringLength(1000)]
        public string RequestDetail { get; set; }

        [DisplayName("Status Details")]
        [StringLength(1000)]
        public string StatusDetail { get; set; }

        [DisplayName("Fix Details")]
        [StringLength(1000)]
        public string FixDetail { get; set; }

        [DisplayName("Hours")]
        [Range(0, 100)]
        public int HoursSpent { get; set; }

        public virtual ICollection<Appliance> Appliances { get; set; }
    }
}