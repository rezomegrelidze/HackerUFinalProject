using System.Collections.Generic;
using System.Linq;
using FlightsSystem.Core.Login;

namespace FlightsSystem.Core.BusinessLogic
{
    public class LoggedInAirlineFacade : AnonymousUserFacade, ILoggedInAirlineFacade
    {
        public IList<Ticket> GetAllTickets(LoginToken<AirlineCompany> token)
        {
            if (token.IsTokenValid())
            {
                return _ticketDAO.GetAll();
            }

            throw new TokenNotValidException();
        }

        public IList<Flight> GetAllFlights(LoginToken<AirlineCompany> token)
        {
            if (token.IsTokenValid())
            {
                return _flightDAO.GetAll();
            }

            throw new TokenNotValidException();
        }

        public void CancelFlight(LoginToken<AirlineCompany> token, Flight flight)
        {
            if (token.IsTokenValid())
            {
                _flightDAO.Remove(flight);
            }
            else
            {
                throw new TokenNotValidException();
            }
        }

        public void CreateFlight(LoginToken<AirlineCompany> token, Flight flight)
        {
            if (token.IsTokenValid())
            {
                _flightDAO.Add(flight);
            }
            else
            {
                throw new TokenNotValidException();
            }
        }

        public void UpdateFlight(LoginToken<AirlineCompany> token, Flight flight)
        {
            if (token.IsTokenValid())
            {
                _flightDAO.Update(flight);
            }
            else
            {
                throw new TokenNotValidException();
            }
        }

        public void ChangeMyPassword(LoginToken<AirlineCompany> token, string oldPassword, string newPassword)
        {
            var service = new LoginService(_airlineDAO,_customerDAO);
            if (token.IsTokenValid())
            {
                service.ChangeAirlinePassword(token,oldPassword,newPassword);
            }
            else
            {
                throw new TokenNotValidException();
            }
        }

        public void ModifyAirlineDetails(LoginToken<AirlineCompany> token, AirlineCompany airline)
        {
            if (token.IsTokenValid())
            {
                _airlineDAO.Update(airline);
            }
            else
            {
                throw new TokenNotValidException();
            }
        }
    }
}