using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightsSystem.Core;
using FlightsSystem.Core.BusinessLogic;
using Microsoft.AspNetCore.Authorization;


namespace FlightsSystem.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ClientController : Controller
    {
        private readonly IAnonymousUserFacade _facade;

        public ClientController(IAnonymousUserFacade facade)
        {
            _facade = facade;
        }

        [HttpGet("all_flights")]
        public ActionResult<IEnumerable<Flight>> GetAllFlights()
        {
            try
            {
                return Json(_facade.GetAllFlights());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("all_airlines")]
        public ActionResult<IEnumerable<AirlineCompany>> GetAllAirlineCompanies()
        {
            try
            {
                return Json(_facade.GetAllAirlineCompanies());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("all_flights_vacancy")]
        public ActionResult<Dictionary<Flight, int>> GetAllFlightsVacancy()
        {
            try
            {
                return Json(_facade.GetAllFlightsVacancy());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get_flight_by_id/{id}")]
        public ActionResult<Flight> GetFlightById(int id)
        {
            try
            {
                return Json(_facade.GetFlightById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get_flights_by_origin_country/{countryCode}")]
        public ActionResult<IEnumerable<Flight>> GetFlightsByOriginCountry(int countryCode)
        {
            try
            {
                return Json(_facade.GetFlightsByOriginCountry(countryCode));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get_flights_by_destination_country/{countryCode}")]
        public ActionResult<IEnumerable<Flight>> GetFlightsByDestinationCountry(int countryCode)
        {
            try
            {
                return Json(_facade.GetFlightsByDestinationCountry(countryCode));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get_flights_by_departure_date")]
        public ActionResult<IEnumerable<Flight>> GetFlightsByDepartureDate(int day,int month,int year)
        {
            if (day == 0 || month == 0 || year == 0) return BadRequest("Please specify correct date");

            DateTime departureDate = new DateTime(year, month, day);
            try
            {
                return Json(_facade.GetFlightsByDepartureDate(departureDate));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get_flights_by_landing_date")]
        public ActionResult<IEnumerable<Flight>> GetFlightsByLandingDate(int day, int month, int year)
        {
            if (day == 0 || month == 0 || year == 0) return BadRequest("Please specify correct date");

            var landingDate = new DateTime(year, month, day);
            try
            {
                return Json(_facade.GetFlightsByLandingDate(landingDate));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
