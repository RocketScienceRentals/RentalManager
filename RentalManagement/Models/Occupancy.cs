using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RentalManagement.Models
{
    public class Occupancy
    {
        public int ID { get; set; }
        public Asset AssetID { get; set; }
        public Tenant ClientID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Detail { get; set; }
    }

    public class OccupancyDBContext : DbContext
    {
        public DbSet<Occupancy> Occupancies { get; set; }
    }
}