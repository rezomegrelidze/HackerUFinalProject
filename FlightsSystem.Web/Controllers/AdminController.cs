using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightsSystem.Core;
using FlightsSystem.Core.BusinessLogic;
using FlightsSystem.Core.Login;
using FlightsSystem.Web.Infrastructure;
using FlightsSystem.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace FlightsSystem.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ILoggedInAdministratorFacade _facade;

        public AdminController(
            IUserRepository userRepository,ILoggedInAdministratorFacade facade)
        {
            _userRepository = userRepository;
            _facade = facade;
        }

        [HttpPost("new_airline")]
        public IActionResult NewAirline([FromBody]AirlineCompany airline)
        {
            var token = GetLoginToken();
            if (token == null) return Unauthorized();
            try
            {
                _facade.CreateNewAirline(token, airline);
                return Ok(airline);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update_airline")]
        public IActionResult UpdateAirline([FromBody] AirlineCompany airline)
        {
            var token = GetLoginToken();
            if (token == null) return Unauthorized();
            try
            {
                _facade.UpdateAirlineDetails(token, airline);
                return Ok(airline);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("remove_airline")]
        public IActionResult RemoveAirline([FromBody] AirlineCompany airline)
        {
            var token = GetLoginToken();
            if (token == null) return Unauthorized();
            try
            {
                _facade.RemoveAirline(token, airline);
                return Ok(airline);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("new_customer")]
        public IActionResult NewCustomer([FromBody] Customer customer)
        {
            var token = GetLoginToken();
            if (token == null) return Unauthorized();
            try
            {
                _facade.CreateNewCustomer(token, customer);
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update_customer")]
        public IActionResult UpdateCustomer([FromBody] Customer customer)
        {
            var token = GetLoginToken();
            if (token == null) return Unauthorized();
            try
            {
                _facade.UpdateCustomerDetails(token, customer);
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("remove_customer")]
        public IActionResult RemoveCustomer([FromBody] Customer customer)
        {
            var token = GetLoginToken();
            if (token == null) return Unauthorized();
            try
            {
                _facade.RemoveCustomer(token, customer);
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        LoginToken<Administrator> GetLoginToken()
        {
            var user = HttpContext.Session.GetJson<UserModel>("user");
            if (user == null) return null;
            return new LoginToken<Administrator>()
            {
                User = _userRepository.GetAdministrator(user)
            };
        }
    }
}
