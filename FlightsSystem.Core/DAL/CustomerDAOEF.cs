using System;
using System.Collections.Generic;
using System.Linq;
using FlightsSystem.Core.Helpers;
using FlightsSystem.Core.Login;

namespace FlightsSystem.Core.DAL
{
    public class CustomerDAOEF : ICustomerDAO
    {
        private FlightsSystemContext db;

        public CustomerDAOEF()
        {
            db = new FlightsSystemContext();
        }

        public Customer Get(long id)
        {
            var customer = db.Customers.SingleOrDefault(c => c.Id == id);

            if(customer == null) 
                throw new InvalidOperationException("Customer with given id doesn't exist!");

            return customer;
        }

        public IList<Customer> GetAll()
        {
            return db.Customers.ToList();
        }

        public void Add(Customer t)
        {
            t.Password = PasswordHasher.HashSHA1(t.Password);
            db.Customers.Add(t);
            db.SaveChanges();
        }

        public void Remove(Customer t)
        {
            var customerToRemove = db.Customers.SingleOrDefault(c => c.Id == t.Id);

            if (customerToRemove != null)
            {
                db.Customers.Remove(customerToRemove);
                db.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Can't remove an object that doesn't exist");
            }
        }

        public void Update(Customer t)
        {
            var customerToUpdate = db.Customers.SingleOrDefault(c => c.Id == t.Id);

            if (customerToUpdate != null)
            {
                customerToUpdate.UserName = t.UserName;
                customerToUpdate.Password = PasswordHasher.HashSHA1(t.Password);
                customerToUpdate.Address = t.Address;
                customerToUpdate.CreditCardNumber = t.CreditCardNumber;
                customerToUpdate.FirstName = t.LastName;
                customerToUpdate.PhoneNumber = t.PhoneNumber;
                db.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Can't update an object which is not in the Database");
            }
        }

        public Customer GetCustomerByUsername(string username)
        {
            var customer = db.Customers.SingleOrDefault(c => c.UserName == username);
            if(customer == null) 
                throw new InvalidOperationException("Customer with given username doesn't exist");

            return customer;
        }
    }
}