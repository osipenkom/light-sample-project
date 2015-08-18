using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ant.Cargo.Services.Contracts.Model
{
    public class VehicleDto
    {
        public Int32 ID { get; set; }

        public String Model { get; set; }

        public String RegistrationNumber { get; set; }

        public String PhoneNumber { get; set; }

        public Int32 DistrictID { get; set; }

        public DistrictDto District { get; set; }
    }
}
