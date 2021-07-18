using System;
using System.Diagnostics;
using FlightsSystem.Core.Login;

namespace FlightsSystem.Core.BusinessLogic
{
    public class LoggedInAdministratorFacade : AnonymousUserFacade, ILoggedInAdministratorFacade
    {
        public void CreateNewAirline(LoginToken<Administrator> token, AirlineCompany airline)
        {
            if (token.IsTokenValid())
                _airlineDAO.Add(airline);
            else
            {
                throw new TokenNotValidException();
            }
        }
        public void UpdateAirlineDetails(LoginToken<Administrator> token, AirlineCompany customer)
        {
            if (token.IsTokenValid())
                _airlineDAO.Update(customer);
            else
            {
                throw new TokenNotValidException();
            }
        }

        public void RemoveAirline(LoginToken<Administrator> token, AirlineCompany airline)
        {
            if (token.IsTokenValid())
                _airlineDAO.Remove(airline);
            else
            {
                throw new TokenNotValidException();
            }
        }

        public void CreateNewCustomer(LoginToken<Administrator> token, Customer customer)
        {
            if (token.IsTokenValid())
                _customerDAO.Add(customer);
            else
            {
                throw new TokenNotValidException();
            }
        }

        public void UpdateCustomerDetails(LoginToken<Administrator> token, Customer customer)
        {
            if (token.IsTokenValid())
                _customerDAO.Update(customer);
            else
            {
                throw new TokenNotValidException();
            }
        }

        public void RemoveCustomer(LoginToken<Administrator> token, Customer customer)
        {
            if(token.IsTokenValid())
                _customerDAO.Remove(customer);
            else
            {
                throw new TokenNotValidException();
            }
        }
    }
}