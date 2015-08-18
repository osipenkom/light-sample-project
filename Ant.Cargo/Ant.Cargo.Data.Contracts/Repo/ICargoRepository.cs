using Ant.Cargo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ant.Cargo.Data.Contracts.Repo
{
    public interface ICargoRepository : IRepository
    {
        IEnumerable<District> GetDistricts();

        User GetUserByLogin(String login);

        void AddVehicle(Vehicle vehicle);

        void DeleteVehicles(IEnumerable<Int32> vehicleIDs);

        Vehicle GetVehicleByID(Int32 vehicleID);

        void AddDistrict(String districtName);

        District GetDistrictByID(Int32 districtID);

        IEnumerable<Vehicle> GetVehiclesByPhone(String phone);

        IEnumerable<Vehicle> GetVehiclesByDistrictID(Int32 districtID);
    }
}
