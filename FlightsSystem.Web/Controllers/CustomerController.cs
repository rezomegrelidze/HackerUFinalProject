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
using Newtonsoft.Json;

namespace FlightsSystem.Web.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Customer")]
    public class CustomerController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ILoggedInCustomerFacade _facade;

        public CustomerController(IUserRepository userRepository,ILoggedInCustomerFacade facade)
        {
            _userRepository = userRepository;
            _facade = facade;
        }
        public IActionResult Index()
        {
            return Content("Hello, customer!!");
        }

        [HttpDelete("cancel_ticket")]
        public IActionResult CancelTicket(Ticket ticket)
        {
            var token = GetLoginToken();
            if (token == null) return Unauthorized();
            try
            {
                _facade.CancelTicket(token, ticket);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("purchase_ticket")]
        public ActionResult<Ticket> PurchaseTicket([FromBody]Flight flight)
        {
            var token = GetLoginToken();
            if (token == null) return Unauthorized();
            try
            {
                var ticket = _facade.PurchaseTicket(token, flight);
                return Json(ticket); ;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get_flights")]
        public ActionResult<IEnumerable<Flight>> GetAllMyFlights()
        {
            var token = GetLoginToken();
            if (token == null) return Unauthorized();
            try
            {
                return Json(_facade.GetAllMyFlights(token));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        LoginToken<Customer> GetLoginToken()
        {
            UserModel userModel = JsonConvert.DeserializeObject<UserModel>(HttpContext.Session.GetString("user"));
            return new LoginToken<Customer>
            {
                User = _userRepository.GetCustomer(userModel)
            };
        }
    }
}
