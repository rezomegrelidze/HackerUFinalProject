using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using FlightsSystem.Core.Login;

namespace FlightsSystem.Core.DAL
{
    public class FlightsSystemContext : DbContext
    {
        public FlightsSystemContext() : base("FlightsSystem")
        {
            Database.CreateIfNotExists();
        }

        public DbSet<AirlineCompany> AirlineCompanies { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Administrator> Administrators { get; set; }

    }
}