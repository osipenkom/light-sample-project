using Ant.Cargo.Model;
using Ant.Cargo.Services.Contracts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ant.Cargo.Services.Mappers.Contracts
{
    public interface IUserMapper : IMapper<User, UserDto>
    {
    }
}
