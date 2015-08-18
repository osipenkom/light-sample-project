using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ant.Cargo.Services.Contracts.Model
{
    public class DistrictDto
    {
        public Int32 ID { get; set; }

        public String Name { get; set; }

        public IList<VehicleDto> Vehicles { get; set; }
    }
}
