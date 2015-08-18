using Ant.Cargo.Model;
using Ant.Cargo.Services.Contracts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ant.Cargo.Services.Contracts
{
    public interface ICargoService
    {
        IEnumerable<DistrictDto> GetDistricts();

        Boolean CheckUserCredentials(UserDto userModel);

        void AddVehicle(VehicleDto model);

        IEnumerable<VehicleDto> GetVehiclesByPhone(String phone);

        void DeleteVehicles(IEnumerable<Int32> vehicleIDs);

        void SendSms(SmsDto model);

        void AddDistrict(String districtName);

        DistrictDto GetDistrictByID(Int32 districtID);

        IEnumerable<VehicleDto> GetVehiclesByDistrictID(Int32 districtID);
    }
}
