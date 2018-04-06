using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RentalManagement.Models
{
    public class Asset
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public FullAddress Address { get; set; }
        public int AskingRent { get; set; }
        public ICollection<Occupancy> OccupancyHistory { get; set; }
        public ICollection<Rental> RentalHistory { get; set; }
        public ICollection<Appliance> Appliances { get; set; }
    }
}