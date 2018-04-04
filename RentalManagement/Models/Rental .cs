using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RentalManagement.Models
{
    public class Rental
    {
        public int ID { get; set; }
        public Guid AssetID { get; set; }
        public Guid ClientID { get; set; }
        public DateTime NegotiatedOn { get; set; }
        public string Details { get; set; }
    }

    public class RentalDbContext : DbContext
    {
        public DbSet<Rental> Rentals { get; set; }
    }


}
