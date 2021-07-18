using System.Collections.Generic;
using FlightsSystem.Core.Login;

namespace FlightsSystem.Core.BusinessLogic
{
    public class LoggedInCustomerFacade : AnonymousUserFacade, ILoggedInCustomerFacade
    {
        public void CancelTicket(LoginToken<Customer> token, Ticket ticket)
        {
            if (token.IsTokenValid())
            {
                _ticketDAO.Remove(ticket);
            }
            else
            {
                throw new TokenNotValidException();
            }
        }

        public Ticket PurchaseTicket(LoginToken<Customer> token, Flight flight)
        {
            if (token.IsTokenValid())
            {
                if (flight.RemainingTickets > 0)
                {
                    var customer = token.User;
                    flight.RemainingTickets--;  // decrease the amount of tickets by one 
                    _flightDAO.Update(flight);  // update the given flight in the database
                    var ticket = new Ticket()
                    {
                        Customer = customer,
                        CustomerId = customer.Id,
                        Flight = flight,
                        FlightId = flight.Id
                    };
                    _ticketDAO.Add(ticket);
                    return ticket;
                }
                else
                {
                    throw new PurchaseException($"{flight} is completely booked!");
                }
            }
            else
            {
                throw new TokenNotValidException();
            }
        }

        public IList<Flight> GetAllMyFlights(LoginToken<Customer> token)
        {
            if (token.IsTokenValid())
            {
                return _flightDAO.GetFlightsByCustomer(token.User);
            }
            else
            {
                throw new TokenNotValidException();
            }
        }
    }
}