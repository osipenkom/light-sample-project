using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ant.Cargo.Client.Models
{
    public class VehicleModel
    {
        public Int32 ID { get; set; }

        public String Model { get; set; }

        public String RegistrationNumber { get; set; }

        public String PhoneNumber { get; set; }

        public Int32 DistrictID { get; set; }

        //public DistrictModel District { get; set; }
    }
}