using FlightsSystem.Core;

namespace FlightsSystem.Web.Models
{
    public interface IUserRepository
    {
        UserModel GetUser(UserModel userMode);
        Administrator GetAdministrator(UserModel userModel);
        Customer GetCustomer(UserModel userModel);
        AirlineCompany GetAirline(UserModel userModel);
    }
}