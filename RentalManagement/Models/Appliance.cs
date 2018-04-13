using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalManagement.Models
{
    public class Appliance
    {
        public Guid ID { get; set; }
        public Asset BelongsToAsset { get; set; }
        public string Name { get; set; }
        public ApplianceType Type { get; set; }
        public string Description { get; set; }
    }

    public enum ApplianceType
    {
        DishWasher
        ,Washer
        ,Dryer
        ,Microwave
        ,Stove
    }
}