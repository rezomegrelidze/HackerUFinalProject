using System;
using System.Collections.Generic;
using FlightsSystem.Core.DAL;

namespace FlightsSystem.Core.BusinessLogic
{
    public class AnonymousUserFacade : FacadeBase, IAnonymousUserFacade
    {

        public AnonymousUserFacade()
        {
            _airlineDAO = new AirlineDAOEF();
            _countryDAO = new CountryDAOEF();
            _customerDAO = new CustomerDAOEF();
            _flightDAO = new FlightDAOEF();
            _ticketDAO = new TicketDAOEF();
        }

        public IList<Flight> GetAllFlights()
        {
            return _flightDAO.GetAll();
        }

        public IList<AirlineCompany> GetAllAirlineCompanies()
        {
            return _airlineDAO.GetAll();
        }

        public Dictionary<Flight, int> GetAllFlightsVacancy()
        {
            return _flightDAO.GetAllFlightsVacancy();
        }

        public Flight GetFlightById(int id)
        {
            return _flightDAO.GetFlightById(id);
        }

        public IList<Flight> GetFlightsByOriginCountry(int countryCode)
        {
            return _flightDAO.GetFlightsByOriginCountry(_countryDAO.Get(countryCode));
        }

        public IList<Flight> GetFlightsByDestinationCountry(int countryCode)
        {
            return _flightDAO.GetFlightsByDestinationCountry(_countryDAO.Get(countryCode));
        }

        public IList<Flight> GetFlightsByDepartureDate(DateTime departureDate)
        {
            return _flightDAO.GetFlightsByDepartureDate(departureDate);
        }

        public IList<Flight> GetFlightsByLandingDate(DateTime landingDate)
        {
            return _flightDAO.GetFlightsByLandingDate(landingDate);
        }
    }
}