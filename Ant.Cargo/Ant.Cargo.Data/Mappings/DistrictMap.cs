using Ant.Cargo.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ant.Cargo.Data.Mappings
{
    internal class DistrictMap : EntityTypeConfiguration<District>
    {
        public DistrictMap()
        {
            ToTable("District");

            HasKey(x => x.ID);

            //HasMany(x => x.Vehicles)
            //    .WithRequired(x => x.District);
        }
    }
}
