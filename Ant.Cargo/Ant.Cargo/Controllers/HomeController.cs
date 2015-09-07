using Ant.Cargo.Client.Models;
using Ant.Cargo.Services.Contracts;
using Ant.Cargo.Services.Contracts.Model;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace Ant.Cargo.Client.Controllers
{
    [Authorize]
    public class HomeController : ApiController
    {
        public HomeController(ICargoService service)
        {
            _service = service;
        }

        [HttpPost]
        public IHttpActionResult SendSmsMessageForDistricts(SmsDistrictModel model)
        {
            if (String.IsNullOrEmpty(model.Text))
            {
                return Ok("Введите текст сообщения");
            }
            if (model.Districts.Count() <= 0)
            {
                return Ok("Выберете области для отправки сообщений");
            }

            var vehicles = model.Districts.SelectMany(x => x.Vehicles);
            var phones = vehicles.Select(x=>x.PhoneNumber).Distinct();

            if (phones.Count() > 0)
            {
                var data = new SmsDto { Text = model.Text, Phones = phones.ToList() };
                _service.SendSms(data);
            }

            return Ok();
        }

        [HttpPost]
        public IHttpActionResult SendSmsMessageForVehicles(SmsVehicleModel model)
        {
            if (String.IsNullOrEmpty(model.Text))
            {
                return Ok("Введите текст сообщения");
            }
            if (model.Vehicles.Count() <= 0)
            {
                return Ok("Выберете машины для отправки сообщений");
            }

            var phones = model.Vehicles.Select(x => x.PhoneNumber).Distinct();

            if (phones.Count() > 0)
            {
                var data = new SmsDto { Text = model.Text, Phones = phones.ToList() };
                _service.SendSms(data);
            }

            return Ok();
        }

        private ICargoService _service;
    }
}
