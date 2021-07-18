using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using FlightsSystem.Core;
using FlightsSystem.Core.DAL;
using FlightsSystem.Core.Helpers;
using FlightsSystem.DBGenerator.Models;

namespace FlightsSystem.DBGenerator.Services
{
    // TODO: Finish unimplemented members
    internal class RandomDataGenerator
    {
        private readonly RandomDataSpec _randomDataSpec;
        private readonly ObservableCollection<string> _logList;
        private IAirlineDAO _airlineDAO;
        private ICustomerDAO _customerDAO;
        private IFlightDAO _flightDAO;
        private IAdministratorDAO _administratorDAO;
        private ITicketDAO _ticketDAO;
        private ICountryDAO _countryDAO;
        private double _operationProgress;

        internal delegate void ProgressChanged();

        public event ProgressChanged OnProgressChanged;

        public double OperationProgress
        {
            get => _operationProgress;
            set
            {
                _operationProgress = value;
                OnProgressChanged?.Invoke();
            }
        }

        public RandomDataGenerator(RandomDataSpec randomDataSpec, ObservableCollection<string> logList)
        {
            _randomDataSpec = randomDataSpec;
            _logList = logList;
            InitializeDAOs();
        }

        private void InitializeDAOs()
        {
            _airlineDAO = new AirlineDAOEF();
            _countryDAO = new CountryDAOEF();
            _flightDAO = new FlightDAOEF();
            _administratorDAO = new AdministratorDAOEF();
            _ticketDAO = new TicketDAOEF();
            _customerDAO = new CustomerDAOEF();
        }

        public async Task AddRandomDataToDatabaseAsync()
        {
            OperationProgress = 0;
            double incr = 100 / 12.0; // 12 because there are 12 lines of blocking code 

            var airlineCompanies = (await GetAirlineCompanies()).ToList();
            OperationProgress += incr;
            airlineCompanies.ForEach(airline => _airlineDAO.Add(airline));

            _logList.Add("Airline companies added successfully!");
            OperationProgress += incr;
            var countries = (await GetCountries()).ToList();
            OperationProgress += incr;
            countries.ForEach(country => _countryDAO.Add(country));

            _logList.Add("Countries added successfully!");
            OperationProgress += incr;
            var customers = (await GetCustomers()).ToList();
            OperationProgress += incr;
            customers.ForEach(customer => _customerDAO.Add(customer));

            _logList.Add("Customers added successfully!");
            OperationProgress += incr;
            var admins = (await GetAdministrators()).ToList();
            OperationProgress += incr;
            admins.ForEach(admin => _administratorDAO.Add(admin));

            _logList.Add("Administrators added successfully!");
            OperationProgress += incr;
            var flights = GetFlights(airlineCompanies, countries).ToList();
            OperationProgress += incr;
            flights.ForEach(flight => _flightDAO.Add(flight));

            _logList.Add("Flights added successfully!");
            OperationProgress += incr;
            var tickets = GetTickets(customers, flights).ToList();
            OperationProgress += incr;
            tickets.ForEach(ticket => _ticketDAO.Add(ticket));
            OperationProgress += incr;

            _logList.Add("Tickets added successfully!");

            _logList.Add("Added data successfully");
        }

        private async Task<IEnumerable<Country>> GetCountries()
        {
            var random = new Random();
            var service = new CountryService();
            return (await service.GetAllCountries()).OrderBy(_ => random.Next());
        }

        private IEnumerable<Ticket> GetTickets(List<Customer> customers,List<Flight> flights)
        {
            var rand = new Random();
            
            foreach(var customer in customers)
            {
                var ticketsPerCustomer = rand.Next(_randomDataSpec.MinTicketPerCustomer, _randomDataSpec.MaxTicketPerCustomer);
                for (int i = 0; i < ticketsPerCustomer; i++)
                {
                    var flight = flights.OrderBy(x => rand.Next()).First();
                    var ticket = new Ticket
                    {
                        Customer = customer,
                        CustomerId = customer.Id,
                        Flight = flight,
                        FlightId = flight.Id
                    };
                    yield return ticket;
                }
            }
        }

        private IEnumerable<Flight> GetFlights(List<AirlineCompany> airlineCompanies,List<Country> countries)
        {
            var rand = new Random();
            var numFlights = _randomDataSpec.FlightCountPerCompany;
            foreach (var airlineCompany in airlineCompanies)
            {
                for (int i = 0; i < numFlights; i++)
                {
                    var flight = new Flight
                    {
                        AirlineCompany = airlineCompany,
                        AirlineCompanyId = airlineCompany.Id,
                        DepartureTime = Helpers.GetRandomDateTime()
                    };
                    flight.LandingTime = flight.DepartureTime.AddHours(rand.Next(24)); // add max 24 hours of random travel time
                    flight.OriginCountry = countries.OrderBy(x => rand.Next()).First();
                    flight.DestinationCountry = countries
                        .OrderBy(x => x.GetHashCode()).First(x => x.CountryName != flight.OriginCountry.CountryName);
                    flight.DestinationCountry = countries.OrderBy(x => rand.Next())
                        .First(x => x.CountryName != flight.OriginCountry.CountryName);
                    flight.OriginCountryCode = flight.OriginCountry.Id;
                    flight.DestinationCountryCode = flight.DestinationCountry.Id;
                    flight.RemainingTickets = rand.Next(300, 500);
                    yield return flight;
                }
            }
        }

        private async Task<IEnumerable<Administrator>> GetAdministrators()
        {
            var admins = new List<Administrator>();
            var randomUserService = new RandomUserService();
            for (int i = 0; i < _randomDataSpec.AdministratorCount; i++)
            {
                var user = await randomUserService.GetRandomUser();
                var admin = new Administrator()
                {
                    Address = user.Address,
                    Password = user.Password,
                    UserName = user.Username,
                    PhoneNumber = user.Phone,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };
                admins.Add(admin);
            }
            return admins;
        }

        private async Task<IEnumerable<Customer>> GetCustomers()
        {
            var customers = new List<Customer>();
            var customerCount = new Random().Next(_randomDataSpec.MinCustomerCount,_randomDataSpec.MaxCustomerCount);
            var randomUserService = new RandomUserService();
            for (int i = 0; i < customerCount; i++)
            {
                var user = await randomUserService.GetRandomUser();
                var customer = new Customer
                {
                    Address = user.Address,
                    Password = user.Password,
                    UserName = user.Username,
                    PhoneNumber = user.Phone,
                    CreditCardNumber = GetRandomCreditCardNumber(),
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };
                customers.Add(customer);
            }
            return customers;
        }

        private string GetRandomCreditCardNumber()
        {
            var random = new Random();
            return string.Join("", Enumerable.Range(1, 14).Select(x => random.Next(0, 10).ToString()));
        }

        private async Task<IEnumerable<AirlineCompany>> GetAirlineCompanies()
        {
            var rand = new Random();
            var randomUserService = new RandomUserService();
            var countryService = new CountryService();
            var countries = await countryService.GetAllCountries();
            var airlineService = new AirlineService();
            var airlineCompanyCount = _randomDataSpec.AirlineCompanyCount;
            var airlineNames = (await airlineService.GetAirlineNames())
                .OrderBy(x => rand.Next()).Take(airlineCompanyCount);
            var airlines = new List<AirlineCompany>();
            foreach (var airlineName in airlineNames)
            {
                var randomUser = await randomUserService.GetRandomUser();
                var airline = new AirlineCompany
                {
                    AirlineName = airlineName,
                    Password = randomUser.Password,
                    Country = countries.OrderBy(x => rand.Next()).First()
                };
                airline.CountryCode = airline.Country.Id;
                airlines.Add(airline);
            }

            return airlines;
        }

        public async Task ReplaceDatabaseAsync()
        {
            OperationProgress = 0;
            double incr = 100 / 6.0; // 6 because 6 blocking operations
            await Task.Run(() =>
            {
                _airlineDAO.Clear();
                OperationProgress += incr;
                _countryDAO.Clear();
                OperationProgress += incr;
                _flightDAO.Clear();
                OperationProgress += incr;
                _administratorDAO.Clear();
                OperationProgress += incr;
                _ticketDAO.Clear();
                OperationProgress += incr;
                _customerDAO.Clear();
                OperationProgress += incr;
            });
            _logList.Add("Database cleared from data!");
            await AddRandomDataToDatabaseAsync();
        }
    }
}