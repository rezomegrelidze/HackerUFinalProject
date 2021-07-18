using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightsSystem.Core.DAL
{
    public class CountryDAOEF : ICountryDAO
    {
        private FlightsSystemContext db;

        public CountryDAOEF()
        {
            db = new FlightsSystemContext();
        }

        public Country Get(long id)
        {
            var country = db.Countries.SingleOrDefault(c => c.Id == id);
            if(country == null) 
                throw new InvalidOperationException("Country with given id is not found!");
            return country;
        }

        public IList<Country> GetAll()
        {
            return db.Countries.ToList();
        }

        public void Add(Country t)
        {
            db.Countries.Add(t);
            db.SaveChanges();
        }

        public void Remove(Country t)
        {
            var countryToRemove = db.Countries.SingleOrDefault(c => c.Id == t.Id);
            if (countryToRemove != null)
            {
                db.Countries.Remove(countryToRemove);
                db.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Can't remove an object that doesn't exist");
            }
        }

        public void Update(Country t)
        {
            var countryToUpdate = db.Countries.SingleOrDefault(c => c.Id == t.Id);
            if (countryToUpdate != null)
            {
                countryToUpdate.CountryName = t.CountryName;
                db.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Object with given id not found and hence can't be updated");
            }
        }
    }
}