using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ant.Cargo.Services.Mappers
{
    internal class MappersConfiguration
    {
        public static void CreateMaps()
        {
            DistrictMapper.CreateMaps();
            VehicleMapper.CreateMaps();
            UserMapper.CreateMaps();
        }
    }
}
