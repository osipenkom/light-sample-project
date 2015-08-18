using Ant.Cargo.Data.Contracts;
using Ant.Cargo.Data.Contracts.Repo;
using Ant.Cargo.Model;
using Ant.Cargo.Services.Contracts;
using Ant.Cargo.Services.Contracts.Model;
using Ant.Cargo.Services.Mappers.Contracts;
using Ant.Cargo.Services.Services.SmsServices;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;

namespace Ant.Cargo.Services
{
    public class CargoService : AbstractService, ICargoService
    {
        public CargoService(ICargoContextManager contextManager, IUnityContainer container)
            : base(contextManager, container)
        { }

        public IEnumerable<DistrictDto> GetDistricts()
        {
            var repository = DataContextManager.CreateRepository<ICargoRepository>();
            var mapper = MapperFactory.CreateMapper<IDistrictMapper>();
            var data = repository.GetDistricts();
            var result = mapper.MapCollectionToModel<DistrictDto>(data);
            return result;
        }

        public Boolean CheckUserCredentials (UserDto userModel)
        {
            var result = false;
            var repository = DataContextManager.CreateRepository<ICargoRepository>();
            var user = repository.GetUserByLogin(userModel.Login);

            if (user != null && user.Password.Equals(userModel.Password))
            {
                result = true;
            }

            return result;
        }

        public void AddVehicle(VehicleDto model)
        {
            var result = String.Empty;
            var repository = DataContextManager.CreateRepository<ICargoRepository>();
            var mapper = MapperFactory.CreateMapper<IVehicleMapper>();
            var data = mapper.MapFromModel(model);
            repository.AddVehicle(data);
            DataContextManager.Save();
        }

        public IEnumerable<VehicleDto> GetVehiclesByPhone(String phone)
        {
            var repository = DataContextManager.CreateRepository<ICargoRepository>();
            var mapper = MapperFactory.CreateMapper<IVehicleMapper>();
            var data = repository.GetVehiclesByPhone(phone);
            var result = mapper.MapCollectionToModel<VehicleDto>(data);
            return result;
        }

        public void DeleteVehicles(IEnumerable<Int32> vehicleIDs)
        {
            var repository = DataContextManager.CreateRepository<ICargoRepository>();
            repository.DeleteVehicles(vehicleIDs);
            DataContextManager.Save();
        }

        public void SendSms(SmsDto model)
        {
            try
            {
                SMSC smsc = new SMSC();

                var senderSMSLimit = Configuration.SendSMSLimit;
                var totalSends = model.Phones.Count() / senderSMSLimit;

                for (int i = 0; i <= totalSends; i++)
                {
                    var phonesForSend = model.Phones.Skip(i * senderSMSLimit).Take(senderSMSLimit);
                    var phonesForSendString = String.Join(",", phonesForSend);
                    if (!String.IsNullOrEmpty(phonesForSendString))
                    {
#if !DEBUG
                        smsc.send_sms(phonesForSendString, model.Text, sender: Configuration.SmsSender);
#endif
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void AddDistrict (String districtName)
        {
            var repository = DataContextManager.CreateRepository<ICargoRepository>();
            repository.AddDistrict(districtName);
            DataContextManager.Save();
        }

        public DistrictDto GetDistrictByID(Int32 districtID)
        {
            var repository = DataContextManager.CreateRepository<ICargoRepository>();
            var mapper = MapperFactory.CreateMapper<IDistrictMapper>();
            var data = repository.GetDistrictByID(districtID);
            var result = mapper.MapToModel(data);
            return result;
        }

        public IEnumerable<VehicleDto> GetVehiclesByDistrictID(Int32 districtID)
        {
            var repository = DataContextManager.CreateRepository<ICargoRepository>();
            var mapper = MapperFactory.CreateMapper<IVehicleMapper>();
            var data = repository.GetVehiclesByDistrictID(districtID);
            var result = mapper.MapCollectionToModel<VehicleDto>(data);
            return result;
        }
    }
}
