﻿using Ant.Cargo.Client.Models;
using Ant.Cargo.Services.Contracts;
using Ant.Cargo.Services.Contracts.Model;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Ant.Cargo.Client.Controllers
{
    [Authorize]
    public class VehicleController : ApiController
    {
        public VehicleController(ICargoService service)
        {
            _service = service;
        }

        [HttpPost]
        public IHttpActionResult AddVehicle(VehicleModel model)
        {
            var result = new AddVehicleResultModel { ErrorMessage = CheckVehicleModel(model) };

            if (String.IsNullOrEmpty(result.ErrorMessage))
            {
                model.PhoneNumber = "7" + model.PhoneNumber;
                var data = Mapper.Map<VehicleDto>(model);
                result.VehicleID = _service.AddVehicle(data);
            }

            return Ok(result);
        }

        [HttpPost]
        public IHttpActionResult DeleteVehicles(Int32[] vehicleIDs)
        {
            _service.DeleteVehicles(vehicleIDs);
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult GetVehiclesByDistrictID(Int32 districtID)
        {
            var result = _service.GetVehiclesByDistrictID(districtID);
            return Ok(result);
        }

        private String CheckVehicleModel(VehicleModel model)
        {
            var result = String.Empty;

            if (model.DistrictID <= 0)
            {
                result = "Укажите район";
                return result;
            }
            //if (String.IsNullOrEmpty(model.Model))
            //{
            //    result = "Укажите марку автомобиля";
            //    return result;
            //}
            //if (String.IsNullOrEmpty(model.RegistrationNumber))
            //{
            //    result = "Укажите регистрационный номер автомобиля";
            //    return result;
            //}
            if (String.IsNullOrEmpty(model.PhoneNumber))
            {
                result = "Укажите номер водителя";
                return result;
            }
            else if (_service.GetVehiclesByPhone("7" + model.PhoneNumber).Count() > 0)
            {
                result = String.Format("Машина с номером {0} уже существует.", "7" + model.PhoneNumber);
                return result;
            }

            return result;
        }

        private ICargoService _service;
    }
}
