using System.Collections.Generic;
using FlightsSystem.Core.Login;

namespace FlightsSystem.Core.BusinessLogic
{
    public interface ILoggedInCustomerFacade
    {
        void CancelTicket(LoginToken<Customer> token, Ticket ticket);
        Ticket PurchaseTicket(LoginToken<Customer> token, Flight flight);
        IList<Flight> GetAllMyFlights(LoginToken<Customer> token);
    }
}