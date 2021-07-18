using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Threading;
using System.Timers;
using FlightsSystem.Core.DAL;
using FlightsSystem.Core.Login;
using FlightsSystem.Core.Migrations;
using Timer = System.Timers.Timer;

namespace FlightsSystem.Core.BusinessLogic
{
    // Make configuration project 
    public sealed class FlyingCenterSystem
    {
        public static readonly FlyingCenterSystem Instance = new FlyingCenterSystem();

        public LoginService LoginService { get; } = new LoginService(new AirlineDAOEF(), new CustomerDAOEF());

        static readonly List<Ticket> TicketHistory = new List<Ticket>();
        static readonly List<Flight> FlightHistory = new List<Flight>();
        private ITicketDAO _ticketDAO;
        private IFlightDAO _flightDAO;

        private FlyingCenterSystem()
        {
            InitializeComponents();

            Timer timer = new Timer(TimeSpan.FromDays(3).TotalMilliseconds);
            timer.Start();
            timer.AutoReset = true;
            timer.Elapsed += TimerOnElapsed;
        }

        private void InitializeComponents()
        {
            InitializeDAOs();
        }

        private void InitializeDAOs()
        {
            _ticketDAO = new TicketDAOEF();
            _flightDAO = new FlightDAOEF();
        }

        public TFacade GetFacade<TFacade>() where TFacade : IAnonymousUserFacade, new()
        {
            return new TFacade();
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            ArchiveTicketsAndFlights();
        }

        private void ArchiveTicketsAndFlights()
        {
            var ticketsToArchive = _ticketDAO.GetAll()
                            .Where(t => t.Flight.LandingTime < (DateTime.Now.Subtract(TimeSpan.FromHours(3))));
            var flightsToArchive = _flightDAO.GetAll()
                            .Where(f => f.LandingTime < DateTime.Now.Subtract(TimeSpan.FromHours(3)));
            TicketHistory.AddRange(ticketsToArchive);
            FlightHistory.AddRange(flightsToArchive);
            foreach (var ticket in ticketsToArchive)
                _ticketDAO.Remove(ticket);
            foreach(var flight in flightsToArchive)
                _flightDAO.Remove(flight);
        }
    }
}