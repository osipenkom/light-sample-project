using Ant.Cargo.Model;
using Ant.Cargo.Services.Contracts.Model;
using Ant.Cargo.Services.Mappers.Contracts;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ant.Cargo.Services.Mappers
{
    public class VehicleMapper : AbstractMapper<Vehicle, VehicleDto>, IVehicleMapper
    {
        public static void CreateMaps()
        {
            Mapper.CreateMap<Vehicle, VehicleDto>();

            Mapper.CreateMap<VehicleDto, Vehicle>();
        }
    }
}
