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
        public Asset()
        {
            this.OccupancyHistory = new List<Occupancy>();
            this.RentalHistory = new List<Rental>();
            this.Appliances = new List<Appliance>();
        }

        public Guid ID { get; set; }
        public bool IsOccuppied { get; set; }
        public string Name { get; set; }
        public AssetType Type { get; set; }
        public FullAddress Address { get; set; }
        public int AskingRent { get; set; }
        public virtual ICollection<Occupancy> OccupancyHistory { get; set; }
        public virtual ICollection<Rental> RentalHistory { get; set; }
        public virtual ICollection<Appliance> Appliances { get; set; }
    }

    public enum AssetType
    {
        DetachedHome
        ,Condominium
        ,TownHouse
    }
}