using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightsSystem.Core;
using FlightsSystem.Core.BusinessLogic;
using FlightsSystem.Core.Login;
using FlightsSystem.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace FlightsSystem.Web.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "AirlineCompany")]
    public class AirlineController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ILoggedInAirlineFacade _facade;

        public AirlineController(IUserRepository userRepository,ILoggedInAirlineFacade facade)
        {
            _userRepository = userRepository;
            _facade = facade;
        }

        [HttpGet("get_tickets")]
        public ActionResult<IEnumerable<Ticket>> GetAllTickets()
        {
            var token = GetLoginToken();
            if (token == null) return Unauthorized();
            try
            {
                return Json(_facade.GetAllTickets(token));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("get_flights")]
        public ActionResult<IEnumerable<Flight>> GetAllFlights()
        {
            var token = GetLoginToken();
            if (token == null) return Unauthorized();
            try
            {
                return Json(_facade.GetAllFlights(token));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("cancel_flight")]
        public IActionResult CancelFlight([FromBody]Flight flight)
        {
            var token = GetLoginToken();
            if (token == null) return Unauthorized();
            try
            {
                _facade.CancelFlight(token,flight);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create_flight")]
        public IActionResult CreateFlight([FromBody]Flight flight)
        {
            var token = GetLoginToken();
            if (token == null) return Unauthorized();
            try
            {
                _facade.CreateFlight(token, flight);
                return CreatedAtAction(nameof(CreateFlight), flight);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update_flight")]
        public IActionResult UpdateFlight([FromBody]Flight flight)
        {
            var token = GetLoginToken();
            if (token == null) return Unauthorized();
            try
            {
                _facade.UpdateFlight(token, flight);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("change_my_password")]
        public IActionResult ChangeMyPassword(string oldPassword, string newPassword)
        {
            var token = GetLoginToken();
            if (token == null) return Unauthorized();
            try
            {
                _facade.ChangeMyPassword(token, oldPassword, newPassword);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("modify_airline")]
        public IActionResult ModifyAirline([FromBody] AirlineCompany airline)
        {
            var token = GetLoginToken();
            if (token == null) return Unauthorized();
            try
            {
                _facade.ModifyAirlineDetails(token, airline);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        LoginToken<AirlineCompany> GetLoginToken()
        {
            UserModel userModel = JsonConvert.DeserializeObject<UserModel>(HttpContext.Session.GetString("user"));
            return new LoginToken<AirlineCompany>
            {
                User = _userRepository.GetAirline(userModel)
            };
        }
    }
}
