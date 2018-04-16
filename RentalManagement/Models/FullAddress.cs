using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalManagement.Models
{
    public class FullAddress
    {
        [Key]
        public int ID { get; set; }
        public string StreetAddress { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }

        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }
    }
}