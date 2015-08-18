using Ant.Cargo.Client.Models;
using Ant.Cargo.Services.Contracts.Model;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ant.Cargo.Client.Mappers
{
    public class CargoModelMap
    {
        public static void CreateMaps()
        {
            Mapper.CreateMap<VehicleDto, VehicleModel>();

            Mapper.CreateMap<VehicleModel, VehicleDto>();

            Mapper.CreateMap<DistrictDto, DistrictModel>();

            Mapper.CreateMap<LoginModel, UserDto>();
        }
    }
}