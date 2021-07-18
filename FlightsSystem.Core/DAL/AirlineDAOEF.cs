using System;
using System.Collections.Generic;
using System.Linq;
using FlightsSystem.Core.Helpers;
using FlightsSystem.Core.Login;

namespace FlightsSystem.Core.DAL
{

    // Make remove airline function in stored procedure

    public class AirlineDAOEF : IAirlineDAO
    {
        private FlightsSystemContext db;

        public AirlineDAOEF()
        {
            db = new FlightsSystemContext();
        }

        public AirlineCompany Get(long id)
        {
            var company = db.AirlineCompanies.SingleOrDefault(c => c.Id == id);
            if(company == null)
                throw new InvalidOperationException("AirlineCompany with a given [id] doesn't exist");
            return company;
        }

        public IList<AirlineCompany> GetAll()
        {
            return db.AirlineCompanies.ToList();
        }

        public void Add(AirlineCompany t)
        {
            t.Password = PasswordHasher.HashSHA1(t.Password);
            db.AirlineCompanies.Add(t);
            db.SaveChanges();
        }

        public void Remove(AirlineCompany t)
        {
            var airlineCompany = db.AirlineCompanies.SingleOrDefault(company => company.Id == t.Id);
            if (airlineCompany != null)
            {
                db.AirlineCompanies.Remove(airlineCompany);
                db.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Can't remove an object that doesn't exist");
            }
        }

        public void Update(AirlineCompany t)
        {
            var companyToUpdate = db.AirlineCompanies.SingleOrDefault(company => company.Id == t.Id);
            if (companyToUpdate != null)
            {
                companyToUpdate.AirlineName = t.AirlineName;
                companyToUpdate.Country = t.Country;
                companyToUpdate.CountryCode = t.CountryCode;
                companyToUpdate.UserName = t.UserName;
                companyToUpdate.Password = PasswordHasher.HashSHA1(t.Password);
            }
            else
            {
                throw new InvalidOperationException("Can't update an object which is not contained in the Database");
            }
        }

        public AirlineCompany GetAirlineByUsername(string username)
        {
            var company = db.AirlineCompanies.SingleOrDefault(c => c.UserName == username);
            if (company == null)
            {
                throw new InvalidOperationException("Airline company with given username not found!");
            }
            return company;
        }

        public IList<AirlineCompany> GetAirlinesByCountry(Country country)
        {
            return db.AirlineCompanies.Where(c => c.Id == country.Id).ToList();
        }
    }
}