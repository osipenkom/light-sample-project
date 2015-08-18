using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ant.Cargo.Model
{
    public class District
    {
        public Int32 ID { get; set; }

        public String Name { get; set; }

        #region references

        public virtual IList<Vehicle> Vehicles { get; set; }

        #endregion
    }
}
