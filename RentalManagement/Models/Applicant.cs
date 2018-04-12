using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalManagement.Models
{
    public class Applicant
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Details { get; set; }
        public Asset asset { get; set; }
    }
}