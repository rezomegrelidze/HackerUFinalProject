using System.Collections.Generic;

namespace FlightsSystem.Core.DAL
{
    public interface IAirlineDAO : IBasicDB<AirlineCompany>
    {
        AirlineCompany GetAirlineByUsername(string username);
        IList<AirlineCompany> GetAirlinesByCountry(Country country);
    }
}