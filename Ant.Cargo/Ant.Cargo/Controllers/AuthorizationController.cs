using Ant.Cargo.Client.Models;
using Ant.Cargo.Services.Contracts;
using Ant.Cargo.Services.Contracts.Model;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;

namespace Ant.Cargo.Client.Controllers
{
    public class AuthorizationController : ApiController
    {
        public AuthorizationController(ICargoService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public IHttpActionResult Login(LoginModel model)
        {
            var simpleValidationResult = ChecLoginModel(model);

            if (!String.IsNullOrEmpty(simpleValidationResult))
            {
                return Ok(simpleValidationResult);
            }

            var userDto = Mapper.Map<UserDto>(model);

            if (_service.CheckUserCredentials(userDto))
            {
                FormsAuthentication.SetAuthCookie(model.Login, true);
                return Ok();
            }
            else
            {
                simpleValidationResult = "Incorrect login or Password";
                return Ok(simpleValidationResult);
            }
        }

        private String ChecLoginModel(LoginModel model)
        {
            var result = String.Empty;

            if (model == null)
            {
                result = "Incorrect login or Password";
                return result;
            }

            if (String.IsNullOrEmpty(model.Login))
            {
                result = "Enter login please";
                return result;
            }
            if (String.IsNullOrEmpty(model.Password))
            {
                result = "Enter password please";
                return result;
            }
            return result;
        }

        private ICargoService _service;
    }
}
