using System;
using System.Collections.Generic;
using System.Linq;
using FlightsSystem.Core.Helpers;
using FlightsSystem.Core.Login;

namespace FlightsSystem.Core.DAL
{
    public class AdministratorDAOEF : IAdministratorDAO
    {
        private FlightsSystemContext db;

        public AdministratorDAOEF()
        {
            db = new FlightsSystemContext();
        }

        public Administrator Get(long id)
        {
            var admin = db.Administrators.SingleOrDefault(c => c.Id == id);
            if (admin == null)
                throw new InvalidOperationException("Country with given id is not found!");
            return admin;
        }

        public IList<Administrator> GetAll()
        {
            return db.Administrators.ToList();
        }

        public void Add(Administrator t)
        {
            t.Password = PasswordHasher.HashSHA1(t.Password);
            db.Administrators.Add(t);
            db.SaveChanges();
        }

        public void Remove(Administrator t)
        {
            var adminToRemove = db.Administrators.SingleOrDefault(c => c.Id == t.Id);
            if (adminToRemove != null)
            {
                db.Administrators.Remove(adminToRemove);
                db.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Can't remove an object that doesn't exist");
            }
        }

        public void Update(Administrator t)
        {
            var adminToUpdate = db.Administrators.SingleOrDefault(c => c.Id == t.Id);
            if (adminToUpdate != null)
            {
                adminToUpdate.UserName = t.UserName;
                adminToUpdate.PhoneNumber = t.PhoneNumber;
                adminToUpdate.FirstName = t.FirstName;
                adminToUpdate.LastName = t.LastName;
                adminToUpdate.Address = t.Address;
                adminToUpdate.Password = PasswordHasher.HashSHA1(t.Password);
                db.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Object with given id not found and hence can't be updated");
            }
        }
    }
}