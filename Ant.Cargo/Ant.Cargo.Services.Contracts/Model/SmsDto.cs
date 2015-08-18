using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ant.Cargo.Services.Contracts.Model
{
    public class SmsDto
    {
        public List<String> Phones { get; set; }

        public String Text { get; set; }
    }
}
