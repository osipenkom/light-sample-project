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
    public class DistrictMapper : AbstractMapper<District, DistrictDto>, IDistrictMapper
    {
        public static void CreateMaps()
        {
            Mapper.CreateMap<District, DistrictDto>()
                .ForMember(x=>x.Vehicles, y => y.UseValue(new List<VehicleDto>()));

            Mapper.CreateMap<DistrictDto, District>();
        }
    }
}
