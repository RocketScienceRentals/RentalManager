using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RentalManagement.Models
{
    public class FullAddress
    {
        public int ID { get; set; }
        public string MyProperty { get; set; }
        public string StreetAddress { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
    }

    public class FullAddressDbContext : DbContext
    {
        public DbSet<FullAddress> FullAddresses { get; set; }
    }
}