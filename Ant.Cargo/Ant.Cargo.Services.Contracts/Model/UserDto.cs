using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ant.Cargo.Services.Contracts.Model
{
    public class UserDto
    {
        public Int32 ID { get; set; }

        public String Login { get; set; }

        public String Password { get; set; }
    }
}
