using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ant.Cargo.Client.Models
{
    public class SmsVehicleModel : SmsModel
    {
        public IEnumerable<VehicleModel> Vehicles { get; set; }
    }
}