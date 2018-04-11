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
        public Asset AssetID { get; set; }
        public Tenant ClientID { get; set; }
        public DateTime NegotiatedOn { get; set; }
        public string Details { get; set; }
    }

    //public class RentalManagerDbContext : DbContext
    //{
    //    public DbSet<Rental> Rentals { get; set; }
    //    public DbSet<Tenant> Applicants { get; set; }
    //    public DbSet<Occupancy> Occupancies { get; set; }
    //    public DbSet<FullAddress> FullAddresses { get; set; }
    //    public DbSet<Asset> Assets { get; set; }
    //}
}
