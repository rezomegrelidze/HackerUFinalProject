using System;
using System.Collections.Generic;

namespace FlightsSystem.Core.DAL
{
    public interface IFlightDAO : IBasicDB<Flight>
    {
        Dictionary<Flight, int> GetAllFlightsVacancy();
        Flight GetFlightById(long id);
        IList<Flight> GetFlightsByCustomer(Customer customer);
        IList<Flight> GetFlightsByDepartureDate(DateTime departureDate);
        IList<Flight> GetFlightsByDestinationCountry(Country destinationCountry);
        IList<Flight> GetFlightsByLandingDate(DateTime landingDate);
        IList<Flight> GetFlightsByOriginCountry(Country originCountry);
    }
}