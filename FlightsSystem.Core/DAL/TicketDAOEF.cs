using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightsSystem.Core.DAL
{
    public class TicketDAOEF : ITicketDAO
    {
        private FlightsSystemContext db;

        public TicketDAOEF()
        {
            db = new FlightsSystemContext();
        }

        public Ticket Get(long id)
        {
            var ticket = db.Tickets.SingleOrDefault(t => t.Id == id);
            if(ticket == null) throw new InvalidOperationException("Ticket with a given [id] doesn't exist");
            return ticket;
        }

        public IList<Ticket> GetAll()
        {
            return db.Tickets.ToList();
        }

        public void Add(Ticket t)
        {
            db.Tickets.Add(t);
            db.SaveChanges();
        }

        public void Remove(Ticket t)
        {
            var ticketToRemove = db.Tickets.SingleOrDefault(ticket => ticket.Id == t.Id);
            if (ticketToRemove != null)
            {
                db.Tickets.Remove(ticketToRemove);
                db.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Can't remove an object that doesn't exist");
            }
        }

        public void Update(Ticket t)
        {
            var ticketToUpdate = db.Tickets.SingleOrDefault(ticket => ticket.Id == t.Id);
            if (ticketToUpdate != null)
            {
                ticketToUpdate.Customer = t.Customer;
                ticketToUpdate.Flight = t.Flight;
                ticketToUpdate.CustomerId = t.CustomerId;
                ticketToUpdate.FlightId = t.FlightId;
                db.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Can't update an object which is not contained in the Database");
            }
        }
    }
}