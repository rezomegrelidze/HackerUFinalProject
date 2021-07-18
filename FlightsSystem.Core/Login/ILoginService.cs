using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;

namespace FlightsSystem.Core.Login
{
    public interface ILoginService
    {
        bool TryAdminLogin(string userName, string password, out LoginToken<Administrator> token);
        bool TryCustomerLogin(string userName, string password, out LoginToken<Customer> token);
        bool TryAirlineLogin(string userName, string password, out LoginToken<AirlineCompany> token);
    }
}