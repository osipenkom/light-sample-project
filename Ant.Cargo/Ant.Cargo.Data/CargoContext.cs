using Ant.Cargo.Data.Mappings;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ant.Cargo.Data
{
    public class CargoContext : AbstractDataContext
    {
        public CargoContext(ConnectionConfiguration configuration)
            : base(configuration)
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        /// <summary>
        /// Defines the Personify DbModel.
        /// </summary>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new DistrictMap());
            modelBuilder.Configurations.Add(new VehicleMap());
            modelBuilder.Configurations.Add(new UserMap());
        }
    }
}
