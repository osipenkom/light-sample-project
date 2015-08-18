using Ant.Cargo.Data.Contracts.Repo;
using Ant.Cargo.Data.Repositories;
using Ant.Cargo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ant.Cargo.Data.Repo
{
    public class CargoRepository : AbstractRepository, ICargoRepository
    {
        public CargoRepository(AbstractDataContext context)
            : base(context)
        {
        }

        public IEnumerable<District> GetDistricts()
        {
            return GetQuery<District>().ToList();
        }

        public User GetUserByLogin(String login)
        {
            return GetSingle<User>(x => x.Login.Equals(login));
        }

        public void AddVehicle(Vehicle vehicle)
        {
            base.Add(vehicle);
        }

        public void DeleteVehicles(IEnumerable<Int32> vehicleIDs)
        {
            foreach (var vehicleID in vehicleIDs)
            {
                var entity = GetVehicleByID(vehicleID);
                if (entity != null)
                {
                    base.Remove(entity);
                }
            }
        }

        public Vehicle GetVehicleByID(Int32 vehicleID)
        {
            return GetSingle<Vehicle>(x => x.ID == vehicleID);
        }

        public void AddDistrict(String districtName)
        {
            var district = new District { Name = districtName };
            base.Add(district);
        }

        public District GetDistrictByID(Int32 districtID)
        {
            return GetSingle<District>(x => x.ID == districtID);
        }

        public IEnumerable<Vehicle> GetVehiclesByPhone(String phone)
        {
            return GetQuery<Vehicle>(x => x.PhoneNumber.Equals(phone)).ToList();
        }

        public IEnumerable<Vehicle> GetVehiclesByDistrictID(Int32 districtID)
        {
            return GetQuery<Vehicle>(x => x.DistrictID == districtID).ToList();
        }
    }
}
