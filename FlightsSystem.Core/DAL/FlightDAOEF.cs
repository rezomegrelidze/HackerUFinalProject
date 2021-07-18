using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightsSystem.Core.DAL
{
    public class FlightDAOEF : IFlightDAO
    {
        private FlightsSystemContext db;

        public FlightDAOEF()
        {
            db = new FlightsSystemContext();
        }

        public Dictionary<Flight,int> GetAllFlightsVacancy()
        {
            return db.Flights.ToDictionary(f => f,f => f.RemainingTickets);
        }

        public Flight GetFlightById(long id)
        {
            var flight = db.Flights.SingleOrDefault(f => f.Id == id);
            if(flight == null)
                throw new InvalidOperationException("Flight with given id doesn't exist!");

            return flight;
        }

        public IList<Flight> GetFlightsByCustomer(Customer customer)
        {
            var tickets = db.Tickets.Where(t => t.CustomerId == customer.Id);
            return tickets.Select(x => x.Flight).ToList();
        }

        public IList<Flight> GetFlightsByDepartureDate(DateTime departureDate)
        {
            return db.Flights.Where(f => f.DepartureTime == departureDate).ToList();
        }

        public IList<Flight> GetFlightsByDestinationCountry(Country destinationCountry)
        {
            return db.Flights.Where(f => f.DestinationCountry == destinationCountry).ToList();
        }

        public IList<Flight> GetFlightsByLandingDate(DateTime landingDate)
        {
            return db.Flights.Where(f => f.LandingTime == landingDate).ToList();
        }

        public IList<Flight> GetFlightsByOriginCountry(Country originCountry)
        {
            return db.Flights.Where(f => f.OriginCountry == originCountry).ToList();
        }

        public Flight Get(long id)
        {
            return GetFlightById(id);
        }

        public IList<Flight> GetAll()
        {
            return db.Flights.ToList();
        }

        public void Add(Flight t)
        {
            db.Flights.Add(t);
            db.SaveChanges();
        }

        public void Remove(Flight t)
        {
            var flightToRemove = db.Flights.SingleOrDefault(f => f.Id == t.Id);
            if (flightToRemove != null)
            {
                db.Flights.Remove(flightToRemove);
                db.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Can't remove an object that doesn't exist");
            }
        }

        public void Update(Flight t)
        {
            var flightToUpdate = db.Flights.SingleOrDefault(f => f.Id == t.Id);
            if (flightToUpdate != null)
            {
                flightToUpdate.OriginCountry = t.OriginCountry;
                flightToUpdate.OriginCountryCode = t.OriginCountryCode;
                flightToUpdate.AirlineCompany = t.AirlineCompany;
                flightToUpdate.AirlineCompanyId = t.AirlineCompanyId;
                flightToUpdate.RemainingTickets = t.RemainingTickets;
                flightToUpdate.DestinationCountry = t.DestinationCountry;
                flightToUpdate.DestinationCountryCode = t.DestinationCountryCode;
                flightToUpdate.DepartureTime = t.DepartureTime;
                flightToUpdate.LandingTime = t.LandingTime;
                db.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Can't update an object that doesn't exist");
            }
        }
    }
}