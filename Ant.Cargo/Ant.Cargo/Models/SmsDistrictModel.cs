using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ant.Cargo.Client.Models
{
    public class SmsDistrictModel : SmsModel
    {
        public IEnumerable<DistrictModel> Districts { get; set; }
    }
}