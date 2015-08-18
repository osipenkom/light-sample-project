using Ant.Cargo.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ant.Cargo.Data.Mappings
{
    internal class VehicleMap : EntityTypeConfiguration<Vehicle>
    {
        public VehicleMap()
        {
            ToTable("Vehicle");

            HasKey(x => x.ID);

            HasRequired(x => x.District)
                .WithMany(x=>x.Vehicles)
                .HasForeignKey(x => x.DistrictID);
        }
    }
}
