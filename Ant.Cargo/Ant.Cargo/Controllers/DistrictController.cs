using Ant.Cargo.Client.Models;
using Ant.Cargo.Services.Contracts;
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
    public class DistrictController : ApiController
    {
        public DistrictController(ICargoService service)
        {
            _service = service;
        }

        [HttpGet]
        public IHttpActionResult GetDistricts()
        {
            var data = _service.GetDistricts();
            var result = Mapper.Map<IEnumerable<DistrictModel>>(data);
            return Ok(result);
        }

        [HttpGet]
        public IHttpActionResult AddNewDistrict(String districtName)
        {
            if (!String.IsNullOrEmpty(districtName))
            {
                _service.AddDistrict(districtName);
                return Ok();
            }
            return Ok("Enter District Name, please.");
        }

        [HttpGet]
        public IHttpActionResult GetDistrictByID(Int32 districtID)
        {
            var data = _service.GetDistrictByID(districtID);
            var result = Mapper.Map<DistrictModel>(data);
            return Ok(result);
        }

        private ICargoService _service;
    }
}
