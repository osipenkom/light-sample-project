using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ant.Cargo.Client.Models
{
    public class DistrictModel
    {
        public Int32 ID { get; set; }

        public String Name { get; set; }

        public IList<VehicleModel> Vehicles { get; set; } 
    }
}