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
    public class UserMapper : AbstractMapper<User, UserDto>, IUserMapper
    {
        public static void CreateMaps()
        {
            Mapper.CreateMap<User, UserDto>();

            Mapper.CreateMap<UserDto, User>();
        }
    }
}
